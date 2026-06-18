const path = require("node:path");
const fs = require("node:fs");
const crypto = require("node:crypto");

const connectionString = process.env.DATABASE_URL;
const usePostgres = !!connectionString;

let pool = null;
let sqliteDb = null;
let activeDb = null;

if (usePostgres) {
  console.log("Using PostgreSQL database...");
  const { Pool } = require("pg");
  pool = new Pool({
    connectionString: connectionString,
    ssl: connectionString.includes("localhost") || connectionString.includes("127.0.0.1")
      ? false
      : { rejectUnauthorized: false }
  });
  activeDb = { type: "postgres", client: pool };
} else {
  console.log("Using SQLite local database...");
  const sqlite3 = require("sqlite3").verbose();
  const dbPath = process.env.DB_PATH || path.resolve(__dirname, "../../data/peti.sqlite");
  const dir = path.dirname(dbPath);
  if (!fs.existsSync(dir)) {
    fs.mkdirSync(dir, { recursive: true });
  }
  sqliteDb = new sqlite3.Database(dbPath);
  activeDb = { type: "sqlite", client: sqliteDb, path: dbPath };
}

// Converts SQLite style '?' placeholders to PostgreSQL style '$1', '$2', etc.
function convertQuery(sql) {
  let index = 1;
  return sql.replace(/\?/g, () => `$${index++}`);
}

async function run(sql, params = []) {
  if (usePostgres) {
    const pgSql = convertQuery(sql);
    const result = await pool.query(pgSql, params);
    return result;
  } else {
    return new Promise((resolve, reject) => {
      sqliteDb.run(sql, params, function (err) {
        if (err) reject(err);
        else resolve({ changes: this.changes, lastID: this.lastID });
      });
    });
  }
}

async function get(sql, params = []) {
  if (usePostgres) {
    const pgSql = convertQuery(sql);
    const result = await pool.query(pgSql, params);
    return result.rows[0] || null;
  } else {
    return new Promise((resolve, reject) => {
      sqliteDb.get(sql, params, (err, row) => {
        if (err) reject(err);
        else resolve(row || null);
      });
    });
  }
}

async function all(sql, params = []) {
  if (usePostgres) {
    const pgSql = convertQuery(sql);
    const result = await pool.query(pgSql, params);
    return result.rows;
  } else {
    return new Promise((resolve, reject) => {
      sqliteDb.all(sql, params, (err, rows) => {
        if (err) reject(err);
        else resolve(rows || []);
      });
    });
  }
}

async function initDb() {
  // 1. Create tables in PostgreSQL / SQLite
  await run(`CREATE TABLE IF NOT EXISTS organizations (
    id TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    ruc TEXT NOT NULL UNIQUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
  )`);

  await run(`CREATE TABLE IF NOT EXISTS users (
    id TEXT PRIMARY KEY,
    organization_id TEXT NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
    name TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    role TEXT NOT NULL,
    area TEXT NOT NULL,
    module_permissions TEXT NOT NULL DEFAULT '{}',
    created_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
  )`);

  await run(`CREATE TABLE IF NOT EXISTS petis (
    organization_id TEXT PRIMARY KEY REFERENCES organizations(id) ON DELETE CASCADE,
    data TEXT NOT NULL,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
  )`);

  // 2. Perform SQLite to PostgreSQL migration only when a PostgreSQL URL is configured.
  if (usePostgres) {
    const sqliteDbPath = path.resolve(__dirname, "../../data/peti.sqlite");
    if (fs.existsSync(sqliteDbPath)) {
      console.log("Found SQLite database. Checking if migration is needed...");
      try {
        await migrateFromSqlite(sqliteDbPath);
      } catch (err) {
        console.error("Migration from SQLite failed:", err);
      }
    }
  }
}

async function migrateFromSqlite(sqliteDbPath) {
  const sqlite3 = require("sqlite3").verbose();
  const sqliteDb = new sqlite3.Database(sqliteDbPath);

  const getSqliteData = (query, params = []) => {
    return new Promise((resolve, reject) => {
      sqliteDb.all(query, params, (err, rows) => {
        if (err) reject(err);
        else resolve(rows);
      });
    });
  };

  try {
    const orgs = await getSqliteData("SELECT * FROM organizations");
    const users = await getSqliteData("SELECT * FROM users");
    const petis = await getSqliteData("SELECT * FROM petis");

    console.log(`Migrating ${orgs.length} organizations, ${users.length} users, and ${petis.length} PETIs from SQLite to PostgreSQL...`);

    // Insert organizations
    for (const org of orgs) {
      await run(
        "INSERT INTO organizations (id, name, ruc, created_at) VALUES (?, ?, ?, ?) ON CONFLICT (id) DO NOTHING",
        [org.id, org.name, org.ruc, org.created_at || new Date().toISOString()]
      );
    }

    // Insert users
    for (const u of users) {
      await run(
        "INSERT INTO users (id, organization_id, name, email, password_hash, role, area, module_permissions, created_at) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?) ON CONFLICT (id) DO NOTHING",
        [u.id, u.organization_id, u.name, u.email, u.password_hash, u.role, u.area, u.module_permissions, u.created_at || new Date().toISOString()]
      );
    }

    // Insert and migrate petis data structure
    for (const p of petis) {
      let parsed = JSON.parse(p.data);
      parsed = migratePetiDataStructure(parsed);
      await run(
        "INSERT INTO petis (organization_id, data, updated_at) VALUES (?, ?, ?) ON CONFLICT (organization_id) DO UPDATE SET data = EXCLUDED.data, updated_at = EXCLUDED.updated_at",
        [p.organization_id, JSON.stringify(parsed), p.updated_at || new Date().toISOString()]
      );
    }

    console.log("Migration successful! Renaming SQLite database file...");
    sqliteDb.close();

    // Rename sqlite file to prevent migration running again
    const migratedPath = sqliteDbPath + ".migrated";
    if (fs.existsSync(migratedPath)) {
      fs.unlinkSync(migratedPath);
    }
    fs.renameSync(sqliteDbPath, migratedPath);
    console.log(`SQLite database renamed to ${path.basename(migratedPath)}`);
  } catch (error) {
    sqliteDb.close();
    throw error;
  }
}

function migratePetiDataStructure(petiObj) {
  if (!petiObj || !petiObj.data) return petiObj;
  
  // 1. Objectives migration: flat row -> hierarchical (1 strategic -> N specifics)
  if (petiObj.data.objectives && Array.isArray(petiObj.data.objectives.rows)) {
    const oldRows = petiObj.data.objectives.rows;
    const needsMigration = oldRows.some(row => row.hasOwnProperty('specific') || row.hasOwnProperty('indicator'));
    
    if (needsMigration) {
      const newRows = [];
      const strategicMap = new Map();
      
      for (const row of oldRows) {
        const stratText = row.strategic || 'Objetivo Estratégico General';
        if (!strategicMap.has(stratText)) {
          const newStrat = {
            id: `str-${crypto.randomUUID()}`,
            strategic: stratText,
            specifics: []
          };
          strategicMap.set(stratText, newStrat);
          newRows.push(newStrat);
        }
        
        const newStrat = strategicMap.get(stratText);
        if (row.specific || row.indicator || row.owner) {
          newStrat.specifics.push({
            id: `sp-${crypto.randomUUID()}`,
            name: row.specific || 'Objetivo Específico',
            description: row.specific || '',
            indicator: row.indicator || '',
            owner: row.owner || '',
            status: 'Pendiente',
            createdAt: new Date().toISOString().split('T')[0]
          });
        }
      }
      petiObj.data.objectives.rows = newRows;
    }
  }

  // 2. Porter forces migration
  if (!petiObj.data.porter || !petiObj.data.porter.forces) {
    petiObj.data.porter = { forces: [] };
  }

  // 3. PEST categories migration
  if (petiObj.data.pest && Array.isArray(petiObj.data.pest.factors)) {
    petiObj.data.pest.answers = petiObj.data.pest.answers || {};
    const oldFactors = petiObj.data.pest.factors;
    const needsPestMigration = oldFactors.length > 0 && !oldFactors[0].hasOwnProperty('category');
    if (needsPestMigration) {
      petiObj.data.pest.factors = oldFactors.map(f => {
        const categoryMap = {
          "Político": "Político",
          "Económico": "Económico",
          "Social": "Social",
          "Tecnológico": "Tecnológico"
        };
        return {
          id: f.id,
          category: categoryMap[f.type] || "Político",
          factor: f.factor || "",
          impactType: f.impact >= 3 ? "Favorable" : "Desfavorable",
          valuation: f.impact ?? 2,
          probability: f.probability ?? 2,
          note: f.note || ""
        };
      });
    }
  } else {
    petiObj.data.pest = { answers: {}, factors: [] };
  }

  // 4. Strategy items migration
  if (!petiObj.data.strategy || !petiObj.data.strategy.items) {
    if (!petiObj.data.strategy) {
      petiObj.data.strategy = { matrices: { fo: {}, af: {}, ad: {}, od: {} }, selected: "", reflection: "", items: [] };
    } else {
      petiObj.data.strategy.items = [];
    }
  }
  normalizeStrategyMatrices(petiObj.data.strategy);

  // 5. Matriz CAME migration
  if (petiObj.data.came && petiObj.data.came.actions) {
    for (const key of ['correct', 'confront', 'maintain', 'exploit']) {
      if (Array.isArray(petiObj.data.came.actions[key])) {
        petiObj.data.came.actions[key] = petiObj.data.came.actions[key].map(act => {
          if (act.hasOwnProperty('text') && !act.hasOwnProperty('action')) {
            return {
              id: act.id,
              action: act.text,
              owner: act.owner || '',
              startDate: new Date().toISOString().split('T')[0],
              endDate: act.deadline ? new Date(act.deadline).toISOString().split('T')[0] : new Date().toISOString().split('T')[0],
              strategyId: act.strategyId || '',
              priority: 'Media',
              status: 'Pendiente',
              notes: ''
            };
          }
          return { strategyId: '', ...act };
        });
      } else {
        petiObj.data.came.actions[key] = [];
      }
    }
  }

  return petiObj;
}

function normalizeStrategyMatrices(strategy) {
  if (!strategy) return;
  const matrices = strategy.matrices || {};
  strategy.matrices = {
    fo: Array.isArray(matrices.fo) ? {} : (matrices.fo || {}),
    af: matrices.af || matrices.fa || {},
    ad: matrices.ad || matrices.da || {},
    od: matrices.od || matrices.do || {},
  };
  (strategy.items || []).forEach((item) => {
    const typeMap = { FA: "AF", DA: "AD", DO: "OD" };
    if (typeMap[item.type]) item.type = typeMap[item.type];
  });
}

module.exports = { db: activeDb, run, get, all, initDb, migratePetiDataStructure, usePostgres };

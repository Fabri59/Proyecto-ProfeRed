const { all, get } = require("../database/db");
const { createPeti } = require("../database/defaultPeti");

function parseJson(value, fallback) {
  try {
    return JSON.parse(value || "");
  } catch {
    return fallback;
  }
}

function publicUser(row) {
  return {
    id: row.id,
    organizationId: row.organization_id,
    name: row.name,
    email: row.email,
    role: row.role,
    area: row.area,
    modulePermissions: parseJson(row.module_permissions, {}),
  };
}

async function sessionPayload(userRow, token) {
  const organization = await get("SELECT id, name, ruc FROM organizations WHERE id = ?", [userRow.organization_id]);
  if (!organization) throw new Error("Organización no encontrada para el usuario.");
  const users = await all("SELECT * FROM users WHERE organization_id = ? ORDER BY created_at ASC", [userRow.organization_id]);
  const petiRow = await get("SELECT data FROM petis WHERE organization_id = ?", [userRow.organization_id]);
  const peti = petiRow?.data
    ? parseJson(petiRow.data, createPeti(organization.name, organization.ruc))
    : createPeti(organization.name, organization.ruc);
  return {
    token,
    user: publicUser(userRow),
    organization,
    users: users.map(publicUser),
    peti,
  };
}

module.exports = { publicUser, sessionPayload };

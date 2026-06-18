const sqlite3 = require('sqlite3').verbose();
const path = require('path');

const dbPath = path.resolve(__dirname, './data/peti.sqlite');
console.log('Reading database from:', dbPath);

const db = new sqlite3.Database(dbPath, (err) => {
  if (err) {
    console.error('Error opening database:', err);
    process.exit(1);
  }
});

db.all("SELECT id, name, ruc, created_at FROM organizations", [], (err, orgs) => {
  if (err) {
    console.error('Error reading organizations:', err);
  } else {
    console.log('ORGANIZATIONS:', orgs);
  }
});

db.all("SELECT id, organization_id, name, email, role, area FROM users", [], (err, users) => {
  if (err) {
    console.error('Error reading users:', err);
  } else {
    console.log('USERS:', users);
  }
});

db.all("SELECT organization_id, data, updated_at FROM petis", [], (err, petis) => {
  if (err) {
    console.error('Error reading petis:', err);
  } else {
    console.log('PETIS COUNT:', petis.length);
    petis.forEach(peti => {
      console.log('Peti Org:', peti.organization_id);
      console.log('Peti Data:', JSON.stringify(JSON.parse(peti.data), null, 2));
    });
  }
  db.close();
});

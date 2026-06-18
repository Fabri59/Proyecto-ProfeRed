const sqlite3 = require('sqlite3').verbose();
const path = require('path');

const dbPath = path.resolve(__dirname, './data/peti.sqlite');

const db = new sqlite3.Database(dbPath);

db.all("SELECT organization_id, data FROM petis", [], (err, petis) => {
  if (err) {
    console.error(err);
  } else {
    petis.forEach(peti => {
      const parsed = JSON.parse(peti.data);
      console.log('SWOT strengths count:', parsed.data.swot.strengths.length);
      console.log('SWOT opportunities count:', parsed.data.swot.opportunities.length);
      console.log('SWOT weaknesses count:', parsed.data.swot.weaknesses.length);
      console.log('SWOT threats count:', parsed.data.swot.threats.length);
      console.log('OBJECTIVES:', parsed.data.objectives);
    });
  }
  db.close();
});

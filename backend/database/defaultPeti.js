const { randomUUID } = require("node:crypto");

function uid(prefix = "id") {
  return `${prefix}-${randomUUID()}`;
}

function createPeti(companyName, ruc = "", sector = "") {
  return {
    activeModule: "info",
    approvals: {},
    assignedResponsibles: {},
    history: [],
    versions: [],
    notifications: [],
    data: {
      info: { name: companyName, ruc, sector, description: "", employees: "", manager: "", tiLead: "" },
      mission: { text: "" },
      vision: { text: "" },
      values: { items: [] },
      objectives: { uen: "", rows: [] },
      swot: { strengths: [], opportunities: [], weaknesses: [], threats: [] },
      valueChain: { answers: {}, reflections: { strengths: [], weaknesses: [], summary: "" } },
      bcg: { products: [] },
      porter: { forces: [] },
      pest: { answers: {}, factors: [] },
      strategy: { matrices: { fo: {}, af: {}, ad: {}, od: {} }, selected: "", reflection: "", items: [] },
      came: { actions: { correct: [], confront: [], maintain: [], exploit: [] } },
      executive: { promoters: "", conclusions: "" },
    },
  };
}

module.exports = { createPeti, uid };

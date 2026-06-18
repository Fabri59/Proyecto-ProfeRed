let backendReady = false;

const valueChainQuestions = [
  "La empresa tiene una política sistematizada de cero defectos en la producción de productos/servicios.",
  "La empresa emplea los medios productivos tecnológicamente más avanzados de su sector.",
  "La empresa dispone de un sistema de información y control de gestión eficiente y eficaz.",
  "Los medios técnicos y tecnológicos están preparados para competir a corto, medio y largo plazo.",
  "La empresa es un referente en su sector en I+D+i.",
  "La excelencia de los procedimientos de la empresa es una principal fuente de ventaja competitiva.",
  "La empresa dispone de página web y la usa para relacionarse con clientes y proveedores.",
  "Los productos/servicios incorporan tecnología difícil de imitar.",
  "La empresa optimiza su cadena de producción como ventaja competitiva en costes.",
  "La informatización de la empresa es una fuente clara de ventaja competitiva.",
  "Los canales de distribución son una fuente de ventajas competitivas.",
  "Los productos/servicios son diferencialmente valorados por el cliente.",
  "La empresa dispone y ejecuta un plan sistemático de marketing y ventas.",
  "La empresa tiene optimizada su gestión financiera.",
  "La empresa mejora continuamente la relación con clientes desde un plan previo.",
  "La empresa es referente en el lanzamiento de productos y servicios innovadores.",
  "Los recursos humanos son considerados el principal activo estratégico.",
  "La plantilla está motivada y conoce metas, objetivos y estrategias.",
  "La empresa trabaja conforme a una estrategia y objetivos claros.",
  "La gestión del circulante está optimizada.",
  "Está definido el posicionamiento estratégico de todos los productos.",
  "Existe una política de marca basada en reputación y relación con clientes.",
  "La cartera de clientes está altamente fidelizada.",
  "El equipo de ventas y marketing es una ventaja competitiva.",
  "El servicio al cliente es una ventaja competitiva frente a competidores.",
];

const scale = [
  "Totalmente en desacuerdo",
  "No está de acuerdo",
  "Está de acuerdo",
  "Bastante de acuerdo",
  "Totalmente de acuerdo",
];

const pestQuestions = [
  { category: "Social", text: "Los cambios en la composición étnica de los consumidores de nuestro mercado está teniendo un notable impacto." },
  { category: "Social", text: "El envejecimiento de la población tiene un importante impacto en la demanda." },
  { category: "Social", text: "Los nuevos estilos de vida y tendencias originan cambios en la oferta de nuestro sector." },
  { category: "Social", text: "El envejecimiento de la población tiene un importante impacto en la oferta del sector donde operamos." },
  { category: "Social", text: "Las variaciones en el nivel de riqueza de la población impactan considerablemente en la demanda de los productos/servicios del sector donde operamos." },
  { category: "Político", text: "La legislación fiscal afecta muy considerablemente a la economía de las empresas del sector donde operamos." },
  { category: "Político", text: "La legislación laboral afecta muy considerablemente a la operativa del sector donde actuamos." },
  { category: "Político", text: "Las subvenciones otorgadas por las Administraciones Públicas son claves en el desarrollo competitivo del mercado donde operamos." },
  { category: "Político", text: "El impacto que tiene la legislación de protección al consumidor, en la manera de producir bienes y/o servicios es muy importante." },
  { category: "Político", text: "La normativa autonómica tiene un impacto considerable en el funcionamiento del sector donde actuamos." },
  { category: "Económico", text: "Las expectativas de crecimiento económico generales afectan crucialmente al mercado donde operamos." },
  { category: "Económico", text: "La política de tipos de interés es fundamental en el desarrollo financiero del sector donde trabaja nuestra empresa." },
  { category: "Económico", text: "La globalización permite a nuestra industria gozar de importantes oportunidades en nuevos mercados." },
  { category: "Económico", text: "La situación del empleo es fundamental para el desarrollo económico de nuestra empresa y nuestro sector." },
  { category: "Económico", text: "Las expectativas del ciclo económico de nuestro sector impactan en la situación económica de sus empresas." },
  { category: "Tecnológico", text: "Las Administraciones Públicas están incentivando el esfuerzo tecnológico de las empresas de nuestro sector." },
  { category: "Tecnológico", text: "Internet, el comercio electrónico, el wireless y otras NTIC están impactando en la demanda de nuestros productos/servicios y en los de la competencia." },
  { category: "Tecnológico", text: "El empleo de NTIC's es generalizado en el sector donde trabajamos." },
  { category: "Tecnológico", text: "En nuestro sector, es de gran importancia ser pionero o referente en el empleo de aplicaciones tecnológicas." },
  { category: "Tecnológico", text: "En el sector donde operamos, para ser competitivos, es condición sine qua non innovar constantemente." },
  { category: "Medioambiental", text: "La legislación medioambiental afecta al desarrollo de nuestro sector." },
  { category: "Medioambiental", text: "Los clientes de nuestro mercado exigen que seamos socialmente responsables, en el plano medioambiental." },
  { category: "Medioambiental", text: "En nuestro sector, las políticas medioambientales son una fuente de ventajas competitivas." },
  { category: "Medioambiental", text: "La creciente preocupación social por el medio ambiente impacta notablemente en la demanda de productos/servicios ofertados en nuestro mercado." },
  { category: "Medioambiental", text: "El factor ecológico es una fuente de diferenciación clara en el sector donde opera nuestra empresa." },
];

const modules = [
  { id: "info", short: "Empresa", title: "Información de empresa", owner: "Administración", help: "Registre datos generales de la organización." },
  { id: "mission", short: "Misión", title: "Misión", owner: "Gerencia", help: "Defina la razón de ser de la empresa." },
  { id: "vision", short: "Visión", title: "Visión", owner: "Gerencia", help: "Defina el estado futuro deseado." },
  { id: "values", short: "Valores", title: "Valores", owner: "Gerencia", help: "Registre principios culturales y de comportamiento." },
  { id: "objectives", short: "Objetivos", title: "Objetivos estratégicos y UEN", owner: "Gerencia", help: "Defina UEN, objetivos estratégicos, específicos e indicadores." },
  { id: "valueChain", short: "Cadena", title: "Cadena de valor y autodiagnóstico", owner: "TI", help: "Evalúe las 25 afirmaciones del Excel y obtenga potencial de mejora." },
  { id: "bcg", short: "BCG", title: "Matriz de Crecimiento - Participación BCG", owner: "TI", help: "Clasifique productos por crecimiento y participación relativa." },
  { id: "porter", short: "Porter", title: "5 Fuerzas de Porter", owner: "Planeamiento", help: "Evalúe el perfil competitivo del microentorno." },
  { id: "pest", short: "PEST", title: "Análisis PEST", owner: "Planeamiento", help: "Evalúe factores políticos, económicos, sociales y tecnológicos." },
  { id: "swot", short: "FODA", title: "Análisis interno y externo FODA", owner: "TI", help: "Revise fortalezas, oportunidades, debilidades y amenazas generadas automáticamente." },
  { id: "strategy", short: "Estrategia", title: "Identificación de estrategias", owner: "Gerencia", help: "Cruce factores FODA para identificar la estrategia dominante." },
  { id: "came", short: "CAME", title: "Matriz CAME", owner: "Gerencia", help: "Defina acciones para corregir, afrontar, mantener y explotar." },
  { id: "executive", short: "Resumen", title: "Resumen ejecutivo", owner: "Gerencia", help: "Consolide el PETI en un reporte ejecutivo." },
];

const rolePermissions = {
  "Administrador Empresa": { admin: true, approve: true, edit: modules.map((m) => m.id) },
  Gerencia: { admin: false, approve: true, edit: ["mission", "vision", "values", "objectives", "strategy", "came", "executive"] },
  "Área TI": { admin: false, approve: false, edit: ["objectives", "swot", "valueChain", "bcg", "porter", "pest"] },
  Administración: { admin: false, approve: false, edit: ["info", "objectives", "executive"] },
  Planeamiento: { admin: false, approve: false, edit: ["porter", "pest"] },
  Consultor: { admin: false, approve: false, edit: modules.filter((m) => m.id !== "info").map((m) => m.id) },
  Finanzas: { admin: false, approve: false, edit: [] },
  Lector: { admin: false, approve: false, edit: [] },
};

let db = { organizations: [], users: [] };
let session = null;
let autosaveTimer = null;

const $ = (selector) => document.querySelector(selector);

function now() {
  return new Date().toLocaleString("es-PE", { dateStyle: "short", timeStyle: "short" });
}

function uid(prefix = "id") {
  const cryptoApi = globalThis.crypto;
  if (cryptoApi && typeof cryptoApi.randomUUID === "function") {
    return `${prefix}-${cryptoApi.randomUUID()}`;
  }
  const random = Math.random().toString(36).slice(2, 15);
  const time = Date.now().toString(36);
  return `${prefix}-${time}-${random}`;
}

function createPeti(companyName, sector = "", ruc = "") {
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
      pest: { answers: {}, impacts: {}, factors: [] },
      strategy: { matrices: { fo: {}, af: {}, ad: {}, od: {} }, selected: "", reflection: "", items: [] },
      came: { actions: { correct: [], confront: [], maintain: [], exploit: [] } },
      executive: { promoters: "", conclusions: "" },
    },
  };
}

function convertPetiDataStructure(petiObj) {
  if (!petiObj || !petiObj.data) return petiObj;

  // 1. Objectives (Strategic -> N Specifics)
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
            id: `str-${uid("obj")}`,
            strategic: stratText,
            specifics: []
          };
          strategicMap.set(stratText, newStrat);
          newRows.push(newStrat);
        }
        const newStrat = strategicMap.get(stratText);
        if (row.specific || row.indicator || row.owner) {
          newStrat.specifics.push({
            id: `sp-${uid("obj")}`,
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

  // 2. Porter forces
  if (!petiObj.data.porter || !petiObj.data.porter.forces) {
    petiObj.data.porter = { forces: [] };
  }

  // 3. PEST
  if (petiObj.data.pest && Array.isArray(petiObj.data.pest.factors)) {
    petiObj.data.pest.answers = petiObj.data.pest.answers || {};
    petiObj.data.pest.impacts = petiObj.data.pest.impacts || {};
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
    petiObj.data.pest = { answers: {}, impacts: {}, factors: [] };
  }

  // 4. Strategy
  if (!petiObj.data.strategy || !petiObj.data.strategy.items) {
    if (!petiObj.data.strategy) {
      petiObj.data.strategy = { matrices: { fo: {}, af: {}, ad: {}, od: {} }, selected: "", reflection: "", items: [] };
    } else {
      petiObj.data.strategy.items = [];
    }
  }
  normalizeStrategyMatrices(petiObj.data.strategy);

  // 5. CAME
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

function applySession(payload) {
  const migratedPeti = convertPetiDataStructure(payload.peti);
  db = {
    organizations: [{ ...payload.organization, peti: migratedPeti }],
    users: payload.users,
  };
  session = { userId: payload.user.id };
}

async function persist() {
  if (!session) return;
  try {
    await Api.savePeti(peti());
  } catch (error) {
    toast(error.message);
  }
}

function currentUser() {
  return db.users.find((user) => user.id === session?.userId);
}

function currentOrg() {
  return db.organizations.find((org) => org.id === currentUser()?.organizationId);
}

function orgUsers() {
  return db.users.filter((user) => user.organizationId === currentOrg().id);
}

function peti() {
  return currentOrg().peti;
}

function canAdmin() {
  return Boolean(rolePermissions[currentUser()?.role]?.admin);
}

function canApprove() {
  const moduleId = peti()?.activeModule;
  return Boolean(canAdmin() || (rolePermissions[currentUser()?.role]?.approve && userBelongsToModuleArea(currentUser(), moduleId)));
}

function normal(value) {
  return String(value || "").toLowerCase().normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}

function userBelongsToModuleArea(user, moduleId) {
  const area = modules.find((m) => m.id === moduleId)?.owner || "";
  const areaText = `${user.area || ""} ${user.role || ""}`;
  const target = normal(area);
  const source = normal(areaText);
  if (target === "ti") return source.includes("ti") || source.includes("tecnologia");
  return source.includes(target);
}

function canView(moduleId) {
  return Boolean(currentUser()) && !firstMissingBefore(moduleId);
}

function canEdit(moduleId) {
  const user = currentUser();
  if (!user || firstMissingBefore(moduleId)) return false;
  if (rolePermissions[user.role]?.admin) return true;
  if (user.modulePermissions?.[moduleId] === "view") return false;
  if (user.role === "Lector") return false;
  return userBelongsToModuleArea(user, moduleId);
}

function moduleIndex(moduleId) {
  return modules.findIndex((m) => m.id === moduleId);
}

function firstMissingBefore(moduleId) {
  const target = moduleIndex(moduleId);
  for (let index = 0; index < target; index += 1) {
    if (!isModuleComplete(modules[index].id).ok) return modules[index];
  }
  return null;
}

function progressCount() {
  return modules.filter((m) => isModuleComplete(m.id).ok).length;
}

function isModuleComplete(moduleId) {
  const d = peti().data[moduleId];
  const min = (arr, n) => arr.length >= n;
  if (moduleId === "info") return check(["name", "ruc", "sector", "description", "manager", "tiLead"].every((k) => String(d[k] || "").trim()), "Complete RUC, datos de empresa y responsables.");
  if (moduleId === "mission" || moduleId === "vision") return check(String(d.text || "").trim().length >= 30, "Debe tener al menos 30 caracteres.");
  if (moduleId === "values") return check(d.items.length >= 3, "Registre como mínimo 3 valores.");
  if (moduleId === "objectives") return check(d.rows.length >= 3 && d.rows.every((r) => r.strategic && r.specifics && r.specifics.length > 0 && r.specifics.every(sp => sp.name && sp.indicator)), "Registre al menos 3 objetivos estratégicos con sus respectivos específicos e indicadores.");
  if (moduleId === "swot") return check(getFodaStrengths().length >= 2 && getFodaOpportunities().length >= 2 && getFodaWeaknesses().length >= 2 && getFodaThreats().length >= 2, "Complete los módulos fuente hasta generar al menos 2 elementos por cuadrante FODA.");
  if (moduleId === "valueChain") return check(validateValueChain().ok, validateValueChain().message);
  if (moduleId === "bcg") return check(d.products.length >= 3 && d.products.every((p) => p.name && Number(p.sales) > 0 && Number(p.competitorSales) > 0), "Registre 3 productos con ventas y competidor.");
  if (moduleId === "porter") return check(d.forces.length >= 1 && d.forces.every((f) => f.name && f.score !== null && f.notes.trim()), "Registre al menos una fuerza de Porter con valoración y observaciones.");
  if (moduleId === "pest") return check(validatePest().ok, validatePest().message);
  if (moduleId === "strategy") return check(Boolean(strategyWinner()), "Complete los factores FODA para calcular la estrategia dominante.");
  if (moduleId === "came") return check(Object.values(d.actions).every((list) => list.length >= 1), "Registre al menos 1 acción por bloque de la Matriz CAME.");
  if (moduleId === "executive") return check(d.promoters && d.conclusions, "Complete promotores y conclusiones.");
  return check(false, "Módulo no reconocido.");
}

function check(ok, message) {
  return { ok, message };
}

function validateValueChain() {
  const answers = peti().data.valueChain.answers;
  for (let i = 0; i < valueChainQuestions.length; i += 1) {
    if (answers[i] === undefined || answers[i] === null || answers[i] === "") {
      return { ok: false, message: `Debe seleccionar una valoración para la afirmación ${i + 1}.` };
    }
    const value = Number(answers[i]);
    if (!Number.isInteger(value) || value < 0 || value > 4) {
      return { ok: false, message: `La afirmación ${i + 1} tiene una valoración inválida.` };
    }
  }
  return { ok: true, message: "Autodiagnóstico completo." };
}

function validatePest() {
  const answers = peti().data.pest.answers || {};
  for (let i = 0; i < pestQuestions.length; i += 1) {
    if (answers[i] === undefined || answers[i] === null || answers[i] === "") {
      return { ok: false, message: `Debe seleccionar una valoración PEST para la afirmación ${i + 1}.` };
    }
    const value = Number(answers[i]);
    if (!Number.isInteger(value) || value < 0 || value > 4) {
      return { ok: false, message: `La afirmación PEST ${i + 1} tiene una valoración inválida.` };
    }
  }
  return { ok: true, message: "Autodiagnóstico PEST completo." };
}

function valueChainStats() {
  const values = Object.values(peti().data.valueChain.answers).map(Number);
  const answered = values.length;
  if (!answered) {
    return { answered, score: 0, strength: 0, improvement: 0, interpretation: "Pendiente" };
  }
  const max = valueChainQuestions.length * 4;
  const score = values.reduce((sum, v) => sum + v, 0);
  const strength = max ? Math.round((score / max) * 100) : 0;
  const improvement = 100 - strength;
  const interpretation = strength >= 80 ? "Ventaja sólida" : strength >= 60 ? "Potencial competitivo medio" : strength >= 40 ? "Requiere mejora prioritaria" : "Riesgo interno alto";
  return { answered, score, strength, improvement, interpretation };
}

async function login(event) {
  event.preventDefault();
  const email = $("#emailInput").value.trim().toLowerCase();
  const password = $("#passwordInput").value;
  try {
    const payload = await Api.login(email, password);
    Api.setToken(payload.token);
    applySession(payload);
    $("#loginView").classList.add("hidden");
    $("#appView").classList.remove("hidden");
    peti().activeModule = peti().activeModule || "info";
    renderAll();
  } catch (error) {
    toast(error.message);
  }
}

async function registerCompany(event) {
  event.preventDefault();
  const email = $("#regEmail").value.trim().toLowerCase();
  const password = $("#regPassword").value;
  const confirm = $("#regPasswordConfirm").value;
  if (password !== confirm) {
    toast("Las contraseñas no coinciden.");
    return;
  }
  try {
    const payload = await Api.register({
      companyName: $("#regCompany").value.trim(),
      ruc: $("#regRuc").value.trim(),
      email,
      adminName: $("#regName").value.trim(),
      password,
    });
    Api.setToken(payload.token);
    applySession(payload);
    $("#loginView").classList.add("hidden");
    $("#appView").classList.remove("hidden");
    toast("Empresa creada. Usted es el Administrador Empresa.");
    renderAll();
  } catch (error) {
    toast(error.message);
  }
}

function logout() {
  session = null;
  Api.clearToken();
  $("#appView").classList.add("hidden");
  $("#loginView").classList.remove("hidden");
}

function scheduleSave(moduleId, detail = "Actualización automática") {
  $("#autosaveStatus").textContent = "Guardando...";
  clearTimeout(autosaveTimer);
  autosaveTimer = setTimeout(() => {
    logChange(moduleId, "Autoguardado", detail);
    persist();
    $("#autosaveStatus").textContent = `Guardado ${now()}`;
    renderAll();
  }, 450);
}

function logChange(moduleId, action, detail) {
  const user = currentUser();
  peti().history.unshift({ id: uid("hist"), moduleId, action, detail, user: user.name, role: user.role, area: user.area, at: now() });
  peti().versions.unshift({ id: uid("ver"), moduleId, version: peti().versions.length + 1, user: user.name, at: now(), snapshot: structuredClone(peti().data[moduleId]) });
  peti().history = peti().history.slice(0, 80);
  peti().versions = peti().versions.slice(0, 40);
}

function renderAll() {
  $("#tenantName").textContent = currentOrg().name;
  $("#userBadge").textContent = `${currentUser().name} | ${currentUser().role}`;
  $("#manageUsersBtn").classList.toggle("hidden", !canAdmin());
  renderNav();
  renderDashboard();
  renderModule(peti().activeModule);
}

function renderNav() {
  $("#moduleNav").innerHTML = modules.map((m, i) => {
    const missing = firstMissingBefore(m.id);
    const done = isModuleComplete(m.id).ok;
    return `
      <button class="nav-item ${peti().activeModule === m.id ? "active" : ""} ${missing ? "locked" : ""}" data-module="${m.id}" type="button">
        <span class="nav-index">${i + 1}</span><span>${m.short}</span><span class="nav-status">${missing ? "Bloq." : done ? "Listo" : "Pend."}</span>
      </button>`;
  }).join("");
  document.querySelectorAll(".nav-item").forEach((btn) => btn.addEventListener("click", () => navigate(btn.dataset.module)));
}

function navigate(moduleId) {
  const missing = firstMissingBefore(moduleId);
  if (missing) {
    openModal("Acceso bloqueado", `<p class="notice">No puede acceder a <strong>${label(moduleId)}</strong> hasta completar <strong>${missing.title}</strong>.</p><p>${isModuleComplete(missing.id).message}</p>`);
    return;
  }
  peti().activeModule = moduleId;
  persist();
  $(".sidebar").classList.remove("open");
  renderAll();
}

function renderDashboard() {
  const vc = valueChainStats();
  const p = peti();
  const percent = Math.round((progressCount() / modules.length) * 100);
  const porterForces = p.data.porter.forces || [];
  const risk = porterForces.length ? (porterAverage() <= 2 ? "Hostil" : "Favorable") : "Pendiente";
  
  // Calculate Objectives stats
  const strategicCount = p.data.objectives.rows?.length || 0;
  let specificCount = 0;
  let indicatorCount = 0;
  let completedCount = 0;

  (p.data.objectives.rows || []).forEach(strat => {
    (strat.specifics || []).forEach(spec => {
      specificCount++;
      if (spec.indicator && spec.indicator.trim()) {
        indicatorCount++;
      }
      if (spec.status === "Completado") {
        completedCount++;
      }
    });
  });

  const compliancePercent = specificCount > 0 ? Math.round((completedCount / specificCount) * 100) : 0;

  $("#dashboard").innerHTML = `
    <article class="card"><span>Avance PETI</span><strong>${percent}%</strong><div class="progress-bar"><span style="width:${percent}%"></span></div></article>
    <article class="card"><span>Módulos completos</span><strong>${progressCount()}/${modules.length}</strong></article>
    <article class="card"><span>Objetivos Estratégicos</span><strong>${strategicCount}</strong><small>Registrados en PETI</small></article>
    <article class="card"><span>Objetivos Específicos</span><strong>${specificCount}</strong><small>${indicatorCount} con indicador</small></article>
    <article class="card"><span>Cumplimiento Específicos</span><strong>${compliancePercent}%</strong><small>${completedCount} de ${specificCount} completados</small><div class="progress-bar"><span style="width:${compliancePercent}%"></span></div></article>
    <article class="card"><span>Cadena de valor</span><strong>${vc.strength}%</strong><small>${vc.interpretation}</small></article>
    <article class="card"><span>Potencial mejora</span><strong>${vc.improvement}%</strong></article>
    <article class="card"><span>Microentorno (Porter)</span><strong>${risk}</strong></article>
    <article class="card"><span>Última edición</span><strong>${p.history[0]?.at || "Sin historial"}</strong></article>
  `;
}

function renderModule(moduleId) {
  const module = modules.find((m) => m.id === moduleId);
  $("#moduleStep").textContent = `Paso ${moduleIndex(moduleId) + 1} de ${modules.length} | Responsable: ${module.owner}`;
  $("#moduleTitle").textContent = module.title;
  const readonly = !canEdit(moduleId);
  const views = {
    info: renderInfo,
    mission: () => renderTextModule("mission", "Misión de la empresa"),
    vision: () => renderTextModule("vision", "Visión de la empresa"),
    values: renderValues,
    objectives: renderObjectives,
    swot: renderSwot,
    valueChain: renderValueChain,
    bcg: renderBcg,
    porter: renderPorter,
    pest: renderPest,
    strategy: renderStrategy,
    came: renderCame,
    executive: renderExecutive,
  };
  $("#moduleContent").innerHTML = panelShell(moduleId, readonly, views[moduleId]());
  bindModule(moduleId, readonly);
}

function panelShell(moduleId, readonly, inner) {
  const status = isModuleComplete(moduleId);
  const responsible = assignedResponsible(moduleId);
  const assignButton = canAdmin()
    ? `<button class="secondary-btn" data-action="assign" type="button">Asignar responsable</button>`
    : "";
  return `
    <article class="panel">
      <header class="panel-header">
        <div>
          <span class="badge ${status.ok ? "done" : "warn"}">${status.ok ? "Completo" : "Pendiente"}</span>
          ${readonly ? `<span class="badge block">Solo lectura</span>` : ""}
          <p>${modules.find((m) => m.id === moduleId).help}</p>
          <p class="responsible-line"><strong>Responsable del módulo:</strong> ${responsible ? `${esc(responsible.name)} — ${esc(responsible.role)} — ${esc(responsible.area)}` : `Sin asignar (${modules.find((m) => m.id === moduleId).owner})`}</p>
          ${status.ok ? "" : `<p class="notice">${status.message}</p>`}
        </div>
        <div class="action-row">
          ${canApprove() ? `<button class="secondary-btn" data-action="approve" type="button">Aprobar versión</button>` : ""}
          ${assignButton}
          <button class="secondary-btn" data-action="history" type="button">Historial</button>
        </div>
      </header>
      ${inner}
    </article>`;
}

function assignedResponsible(moduleId) {
  const userId = peti().assignedResponsibles?.[moduleId];
  if (!userId) return null;
  return orgUsers().find((user) => user.id === userId) || null;
}

function renderInfo() {
  const d = peti().data.info;
  return `<div class="form-grid">${field("name", "Nombre de la empresa", d.name)}${field("ruc", "RUC", d.ruc)}${field("sector", "Sector", d.sector)}${field("employees", "Colaboradores", d.employees, "number")}${field("manager", "Responsable gerencial", d.manager)}${field("tiLead", "Responsable TI", d.tiLead)}${field("description", "Descripción", d.description, "textarea", "wide")}</div>`;
}

function renderTextModule(moduleId, title) {
  return `<div class="form-grid">${field("text", title, peti().data[moduleId].text, "textarea", "wide")}</div>`;
}

function renderValues() {
  return `<div class="action-row"><button class="primary-btn" data-action="add-value" type="button">Agregar valor</button></div>${table(["Valor", "Descripción", "Acciones"], peti().data.values.items.map((r) => [esc(r.name), esc(r.description), rowActions("value", r.id)]))}`;
}

function isFavorableScore(score) {
  return Number(score) > (scale.length - 1) / 2;
}

function isUnfavorableScore(score) {
  return Number(score) < (scale.length - 1) / 2;
}

function isBcgStrength(product) {
  return relativeShare(product) >= 1;
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

function strategyRelations() {
  return {
    fo: {
      code: "FO",
      name: "Fortalezas vs Oportunidades",
      typology: "Estrategia Ofensiva",
      description: "Deberá adoptar estrategias de crecimiento.",
      rows: getFodaStrengths(),
      cols: getFodaOpportunities(),
      rowLetter: "F",
      colLetter: "O",
    },
    af: {
      code: "AF",
      name: "Amenazas vs Fortalezas",
      typology: "Estrategia Defensiva",
      description: "La empresa está preparada para enfrentarse a las amenazas.",
      rows: getFodaThreats(),
      cols: getFodaStrengths(),
      rowLetter: "A",
      colLetter: "F",
    },
    ad: {
      code: "AD",
      name: "Amenazas vs Debilidades",
      typology: "Estrategia de Supervivencia",
      description: "Se enfrenta a amenazas externas sin las fortalezas necesarias para luchar con la competencia.",
      rows: getFodaThreats(),
      cols: getFodaWeaknesses(),
      rowLetter: "A",
      colLetter: "D",
    },
    od: {
      code: "OD",
      name: "Oportunidades vs Debilidades",
      typology: "Estrategia de Reorientación",
      description: "La empresa no puede aprovechar las oportunidades porque carece de preparación adecuada.",
      rows: getFodaOpportunities(),
      cols: getFodaWeaknesses(),
      rowLetter: "O",
      colLetter: "D",
    },
  };
}

function autoRelationScore(row, col) {
  const scores = [row.score, col.score].map(Number).filter((value) => Number.isFinite(value));
  if (!scores.length) return 0;
  return Math.round(scores.reduce((sum, value) => sum + value, 0) / scores.length);
}

function matrixCellValue(relationKey, row, col) {
  const matrixData = peti().data.strategy.matrices?.[relationKey] || {};
  const override = matrixData[`${row.id}-${col.id}`];
  return override === undefined || override === "" || override === null ? autoRelationScore(row, col) : Number(override);
}

function getFodaStrengths() {
  const d = peti().data;
  const vcScore = Math.round(valueChainStats().strength / 25);
  const vcStrengths = (d.valueChain.reflections.strengths || []).map(s => ({
    id: s.id,
    text: s.text,
    source: "Cadena de Valor",
    score: vcScore
  }));
  const bcgStrengths = (d.bcg.products || [])
    .filter(isBcgStrength)
    .map(p => ({
      id: `bcg-${p.id}`,
      text: `Producto ${bcgQuadrant(p)}: ${p.name}`,
      source: "BCG",
      score: Math.min(4, Math.max(1, Math.round(relativeShare(p) * 2)))
    }));
  return [...vcStrengths, ...bcgStrengths];
}

function getFodaWeaknesses() {
  const d = peti().data;
  const vcScore = Math.round(valueChainStats().improvement / 25);
  const vcWeaknesses = (d.valueChain.reflections.weaknesses || []).map(w => ({
    id: w.id,
    text: w.text,
    source: "Cadena de Valor",
    score: vcScore
  }));
  const bcgWeaknesses = (d.bcg.products || [])
    .filter(p => !isBcgStrength(p))
    .map(p => ({
      id: `bcg-${p.id}`,
      text: `Producto ${bcgQuadrant(p)}: ${p.name}`,
      source: "BCG",
      score: Math.max(1, Math.min(4, 4 - Math.round(relativeShare(p) * 2)))
    }));
  return [...vcWeaknesses, ...bcgWeaknesses];
}

function getFodaOpportunities() {
  const d = peti().data;
  const porterOps = (d.porter.forces || [])
    .filter(f => isFavorableScore(f.score) && f.notes.trim())
    .map(f => ({
      id: `porter-${f.id}`,
      text: `${f.name}: ${f.notes}`,
      source: "Porter",
      score: Number(f.score)
    }));
  const pestOps = pestQuestions
    .map((question, index) => ({ ...question, index, score: Number(d.pest.answers?.[index]) }))
    .filter((question) => d.pest.impacts?.[question.index] === "Oportunidad" && Number.isFinite(question.score))
    .map(question => ({
      id: `pest-${question.index}`,
      text: `${question.category}: ${question.text}`,
      source: "PEST",
      score: question.score
    }));
  return [...porterOps, ...pestOps];
}

function getFodaThreats() {
  const d = peti().data;
  const porterThreats = (d.porter.forces || [])
    .filter(f => isUnfavorableScore(f.score) && f.notes.trim())
    .map(f => ({
      id: `porter-${f.id}`,
      text: `${f.name}: ${f.notes}`,
      source: "Porter",
      score: 4 - Number(f.score)
    }));
  const pestThreats = pestQuestions
    .map((question, index) => ({ ...question, index, score: Number(d.pest.answers?.[index]) }))
    .filter((question) => d.pest.impacts?.[question.index] === "Amenaza" && Number.isFinite(question.score))
    .map(question => ({
      id: `pest-${question.index}`,
      text: `${question.category}: ${question.text}`,
      source: "PEST",
      score: question.score
    }));
  return [...porterThreats, ...pestThreats];
}

function renderObjectives() {
  const d = peti().data.objectives;
  const rows = d.rows || [];

  const strategicBlocks = rows.map((r) => {
    const specificsHtml = (r.specifics || []).map((sp) => {
      const statusBadge = `<span class="badge ${sp.status === "Completado" ? "done" : sp.status === "En proceso" ? "info" : "warn"}">${sp.status}</span>`;
      return `
        <tr>
          <td><strong>${esc(sp.name)}</strong><br/><small class="muted">${esc(sp.description || "")}</small></td>
          <td>${esc(sp.indicator)}</td>
          <td>${esc(sp.owner)}</td>
          <td>${statusBadge}</td>
          <td>${esc(sp.createdAt || "")}</td>
          <td>
            <div class="action-row">
              <button class="secondary-btn" data-action="edit-specific" data-id="${sp.id}" data-parent-id="${r.id}" type="button">Editar</button>
              <button class="danger-btn" data-action="delete-specific" data-id="${sp.id}" data-parent-id="${r.id}" type="button">Eliminar</button>
            </div>
          </td>
        </tr>
      `;
    }).join("");

    const specificsTable = (r.specifics && r.specifics.length > 0)
      ? `<div class="table-wrap"><table><thead><tr><th>Objetivo Específico</th><th>Indicador</th><th>Responsable</th><th>Estado</th><th>Fecha Creación</th><th>Acciones</th></tr></thead><tbody>${specificsHtml}</tbody></table></div>`
      : `<p class="notice">No se han registrado objetivos específicos para este objetivo estratégico.</p>`;

    return `
      <article class="panel" style="margin-top: 24px; border: 1px solid var(--line); border-radius: 8px; padding: 18px; background: #fff;">
        <div class="panel-header" style="display: flex; justify-content: space-between; align-items: center; border-bottom: 1px solid var(--line); padding-bottom: 8px; margin-bottom: 12px;">
          <div>
            <span class="badge" style="background: var(--primary); color: #fff; border: none;">Estratégico</span>
            <h3 style="display: inline-block; margin-left: 8px; font-size: 1.1rem; color: var(--text);">${esc(r.strategic)}</h3>
          </div>
          <div class="action-row">
            <button class="secondary-btn" data-action="edit-strategic" data-id="${r.id}" type="button">Editar</button>
            <button class="danger-btn" data-action="delete-strategic" data-id="${r.id}" type="button">Eliminar</button>
          </div>
        </div>
        
        ${specificsTable}
        
        <div style="margin-top: 12px; text-align: right;">
          <button class="primary-btn" data-action="add-specific" data-parent-id="${r.id}" type="button">+ Agregar objetivo específico</button>
        </div>
      </article>
    `;
  }).join("");

  return `
    <div class="form-grid">
      ${field("uen", "Unidades Estratégicas de Negocio", d.uen, "textarea", "wide")}
    </div>
    <div class="action-row" style="margin-top: 18px;">
      <button class="primary-btn" data-action="add-strategic" type="button">Agregar objetivo estratégico</button>
    </div>
    <div class="strategic-list">
      ${strategicBlocks || `<p class="notice" style="margin-top: 18px;">Aún no se han registrado objetivos estratégicos.</p>`}
    </div>
  `;
}

function renderSwot() {
  const strengths = getFodaStrengths();
  const weaknesses = getFodaWeaknesses();
  const opportunities = getFodaOpportunities();
  const threats = getFodaThreats();

  const renderGroup = (title, list) => {
    const listHtml = list.map(item => {
      const sourceBadge = item.source ? `<span class="badge ${item.source.toLowerCase().replaceAll(" ", "-")}">${item.source}</span>` : `<span class="badge">Automático</span>`;
      return `<div class="notification-item" style="display:flex; justify-content:space-between; align-items:center; gap: 8px; padding: 8px; border-bottom: 1px solid var(--line);">
        <div style="display:flex; align-items:center; gap: 6px;">
          ${sourceBadge} 
          <span style="font-size:0.9rem; color:var(--text);">${esc(item.text)}</span>
        </div>
        <span class="muted" style="font-size:0.8rem;">Derivado</span>
      </div>`;
    }).join("");

    return `
      <section class="panel">
        <div class="panel-header">
          <h3>${title}</h3>
          <span class="badge">Auto FODA</span>
        </div>
        <div class="notification-list" style="max-height: 350px; overflow-y: auto;">
          ${listHtml || `<p class="muted">Sin registros.</p>`}
        </div>
      </section>
    `;
  };

  return `
    <p class="notice">El FODA se genera automáticamente desde Cadena de Valor, BCG, Porter y PEST según la información registrada por la empresa.</p>
    <div class="form-grid">
      ${renderGroup("Fortalezas", strengths)}
      ${renderGroup("Oportunidades", opportunities)}
      ${renderGroup("Debilidades", weaknesses)}
      ${renderGroup("Amenazas", threats)}
    </div>
  `;
}

function renderValueChain() {
  const stats = valueChainStats();
  return `
    <section class="panel">
      <div class="panel-header"><div><h3>Autodiagnóstico de la cadena de valor interna</h3><p>Escala: 0 = Totalmente en desacuerdo, 4 = Totalmente de acuerdo.</p></div><span class="badge ${stats.strength >= 60 ? "done" : "warn"}">${stats.strength}% potencial actual</span></div>
      <div class="progress-bar"><span style="width:${stats.strength}%"></span></div>
      <p class="muted">Potencial de mejora: ${stats.improvement}%. Interpretación: ${stats.interpretation}.</p>
      <div class="table-wrap"><table><thead><tr><th>Afirmación</th><th>Valoración</th></tr></thead><tbody>${valueChainQuestions.map((q, i) => `<tr><td>${i + 1}. ${esc(q)}</td><td>${scoreOptions(`vc-${i}`, peti().data.valueChain.answers[i], i)}</td></tr>`).join("")}</tbody></table></div>
    </section>
    <section class="panel">
      <h3>Reflexiones y análisis</h3>
      <div class="form-grid">${field("summary", "Reflexión general", peti().data.valueChain.reflections.summary, "textarea", "wide")}</div>
      <div class="action-row"><button class="secondary-btn" data-action="add-vc-strength" type="button">Agregar fortaleza detectada</button><button class="secondary-btn" data-action="add-vc-weakness" type="button">Agregar debilidad detectada</button></div>
      <div class="form-grid"><section>${table(["Fortalezas", "Acciones"], peti().data.valueChain.reflections.strengths.map((r) => [esc(r.text), rowActions("vc-strength", r.id)]))}</section><section>${table(["Debilidades", "Acciones"], peti().data.valueChain.reflections.weaknesses.map((r) => [esc(r.text), rowActions("vc-weakness", r.id)]))}</section></div>
    </section>`;
}

function renderBcg() {
  const rows = peti().data.bcg.products.map((p) => ({ ...p, share: relativeShare(p), quadrant: bcgQuadrant(p) }));
  return `<div class="bcg-layout"><section><div class="action-row"><button class="primary-btn" data-action="add-product" type="button">Agregar producto</button></div>${table(["Producto", "Ventas", "Crec.", "Part. rel.", "Cuadrante", "Acciones"], rows.map((r) => [esc(r.name), money(r.sales), `${Number(r.growth || 0).toFixed(1)}%`, r.share.toFixed(2), `<span class="badge">${r.quadrant}</span>`, rowActions("product", r.id)]))}</section><section><div class="bcg-canvas-wrap"><canvas id="bcgCanvas" width="520" height="430"></canvas></div></section></div>`;
}

function renderPorter() {
  const forces = peti().data.porter.forces || [];
  const scalePorter = ["0 (Muy Hostil)", "1 (Hostil)", "2 (Neutral)", "3 (Favorable)", "4 (Muy Favorable)"];
  
  const rows = forces.map(f => {
    const radios = scalePorter.map((label, val) => `
      <label title="${label}" style="margin-right: 8px; font-weight: normal; cursor: pointer;">
        <input type="radio" name="porter-${f.id}" data-porter-id="${f.id}" value="${val}" ${f.score === val ? 'checked' : ''} />
        ${val}
      </label>
    `).join("");

    return `
      <tr>
        <td><strong>${esc(f.name)}</strong></td>
        <td>
          <div class="score-options" style="display:flex; flex-wrap:nowrap;">${radios}</div>
        </td>
        <td>
          <textarea data-porter-notes="${f.id}" placeholder="Escriba las observaciones del análisis..." style="width: 100%; min-height: 48px; padding: 6px; border: 1px solid var(--line); border-radius: 4px; font-family: inherit; font-size: 0.9rem; resize: vertical;">${esc(f.notes || "")}</textarea>
        </td>
        <td>${rowActions("porter-force", f.id)}</td>
      </tr>
    `;
  });

  return `
    <section class="panel">
      <div class="panel-header">
        <div>
          <h3>Análisis de las 5 Fuerzas de Porter</h3>
          <p class="muted">Registre las fuerzas evaluadas por la empresa, valore de 0 (hostil) a 4 (favorable) y documente el análisis.</p>
        </div>
        <button class="primary-btn" data-action="add-porter-force" type="button">Agregar fuerza</button>
      </div>
      <div class="table-wrap">
        <table>
          <thead>
            <tr>
              <th style="width: 180px;">Fuerza de Porter</th>
              <th style="width: 260px;">Valoración</th>
              <th>Observaciones / Análisis</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            ${rows.join("") || `<tr><td colspan="4">Aún no existen registros.</td></tr>`}
          </tbody>
        </table>
      </div>
      <p class="notice" style="margin-top:12px;">
        <strong>Resultado automático:</strong> Las fuerzas con valoración <strong>3 o 4</strong> se agregarán como <strong>Oportunidades</strong> en el FODA. Las fuerzas con valoración <strong>0 o 1</strong> se agregarán como <strong>Amenazas</strong>.
      </p>
    </section>
  `;
}

function renderPest() {
  const d = peti().data.pest;
  const diagnosticRows = pestQuestions.map((question, index) => [
    `<span class="badge" style="background:#d7eef4; color:#164e63; border:none;">${esc(question.category)}</span><br><strong>${index + 1}.</strong> ${esc(question.text)}`,
    scoreOptions(`pest-${index}`, d.answers?.[index], index),
    `<select data-pest-impact="${index}">
      <option value="" ${!d.impacts?.[index] ? "selected" : ""}>No clasificar</option>
      <option value="Oportunidad" ${d.impacts?.[index] === "Oportunidad" ? "selected" : ""}>Oportunidad</option>
      <option value="Amenaza" ${d.impacts?.[index] === "Amenaza" ? "selected" : ""}>Amenaza</option>
    </select>`
  ]);

  return `
    <div class="pest-layout">
      <section>
        <h3>Autodiagnóstico entorno global PEST</h3>
        ${table(["Factor del entorno", "Valoración", "FODA"], diagnosticRows)}
      </section>
      <section>
        <div class="pest-chart-wrap">
          <canvas id="pestCanvas" width="560" height="360"></canvas>
        </div>
      </section>
    </div>
    <p class="notice" style="margin-top:12px;">
      <strong>Resultado automático:</strong> Las afirmaciones marcadas como <strong>Oportunidad</strong> o <strong>Amenaza</strong> se integran al FODA con la valoración registrada en esta tabla.
    </p>
  `;
}

function renderStrategy() {
  const scores = strategyScores();
  const d = peti().data.strategy;
  const strategiesList = d.items || [];
  const relations = strategyRelations();
  
  const strategiesTable = table(
    ["Nombre", "Descripción", "Tipo", "Responsable", "Prioridad", "Puntuación", "Estado", "Acciones"],
    strategiesList.map(item => [
      `<strong>${esc(item.name)}</strong>`,
      esc(item.description),
      `<span class="badge" style="background:#eef2f7; color:var(--text); border:none;">${item.type}</span>`,
      esc(item.owner),
      esc(item.priority),
      item.score,
      `<span class="badge ${item.status === "Implementado" ? "done" : item.status === "En proceso" ? "info" : "warn"}">${item.status}</span>`,
      rowActions("strategy-item", item.id)
    ])
  );

  return `
    <div class="form-grid" style="grid-template-columns: 1fr;">
      ${Object.keys(relations).map((key) => matrixBlock(key)).join("")}
    </div>
    
    <section class="panel" style="margin-top: 18px;">
      <h3>Puntuaciones Consolidadas</h3>
      ${table(["Relaciones", "Tipología de estrategia", "Puntuación", "Descripción"], Object.entries(relations).map(([key, relation]) => [
        relation.code,
        relation.typology,
        `<strong data-strategy-score="${key}">${scores[key]}</strong>`,
        relation.description
      ]))}
    </section>

    <div class="form-grid" style="margin-top: 18px;">
      ${field("selected", "Estrategia identificada (Dominante)", d.selected || strategyWinner(), "text")}
      ${field("reflection", "Reflexión estratégica", d.reflection, "textarea", "wide")}
    </div>

    <section class="panel" style="margin-top: 24px;">
      <div class="panel-header" style="display: flex; justify-content: space-between; align-items: center; border-bottom:1px solid var(--line); padding-bottom:8px; margin-bottom:12px;">
        <h3>Estrategias Formuladas</h3>
        <button class="primary-btn" data-action="add-strategy-item" type="button">+ Formular estrategia</button>
      </div>
      ${strategiesTable}
    </section>
  `;
}

function renderCame() {
  const labels = {
    maintain: "Mantener fortalezas (M)",
    correct: "Corregir debilidades (C)",
    exploit: "Aprovechar oportunidades (E - Explotar)",
    confront: "Mitigar amenazas (A - Afrontar)"
  };

  const panels = Object.entries(labels).map(([key, title]) => {
    const actionsList = peti().data.came.actions[key] || [];

    const tableHtml = table(
      ["Acción de Cambio", "Estrategia vinculada", "Responsable", "Fecha Inicio", "Fecha Fin", "Prioridad", "Estado", "Observaciones", "Acciones"],
      actionsList.map(act => [
        `<strong>${esc(act.action)}</strong>`,
        esc(strategyNameById(act.strategyId) || "Sin vínculo"),
        esc(act.owner),
        esc(act.startDate || ""),
        esc(act.endDate || ""),
        esc(act.priority || "Media"),
        `<span class="badge ${act.status === "Completado" ? "done" : act.status === "En proceso" ? "info" : "warn"}">${act.status}</span>`,
        esc(act.notes || ""),
        rowActions("came-action", act.id, key)
      ])
    );

    return `
      <section class="panel" style="margin-top: 18px;">
        <div class="panel-header" style="display: flex; justify-content: space-between; align-items: center; border-bottom:1px solid var(--line); padding-bottom:8px; margin-bottom:12px;">
          <h3>${title}</h3>
          <button class="secondary-btn" data-action="add-came-action" data-kind="${key}" type="button">Agregar Acción</button>
        </div>
        ${tableHtml}
      </section>
    `;
  }).join("");

  return `<div class="came-workspace">${panels}</div>`;
}

function renderExecutive() {
  const d = peti().data;
  const objectives = d.objectives.rows || [];
  const specificCount = objectives.reduce((sum, objective) => sum + (objective.specifics || []).length, 0);
  const strategies = d.strategy.items || [];
  const cameCount = Object.values(d.came.actions).flat().length;
  return `
    <div class="form-grid">
      ${field("promoters", "Emprendedores / promotores", d.executive.promoters, "textarea", "wide")}
      ${field("conclusions", "Conclusiones", d.executive.conclusions, "textarea", "wide")}
    </div>
    <section class="panel">
      <h3>Resumen generado</h3>
      <p><strong>Empresa:</strong> ${esc(currentOrg().name)}</p>
      <p><strong>Misión:</strong> ${esc(d.mission.text || "Pendiente")}</p>
      <p><strong>Visión:</strong> ${esc(d.vision.text || "Pendiente")}</p>
      <p><strong>Objetivos:</strong> ${objectives.length} estratégicos y ${specificCount} específicos con indicadores independientes.</p>
      <p><strong>FODA automático:</strong> ${getFodaStrengths().length} fortalezas, ${getFodaOpportunities().length} oportunidades, ${getFodaWeaknesses().length} debilidades y ${getFodaThreats().length} amenazas.</p>
      <p><strong>Estrategia dominante:</strong> ${esc(d.strategy.selected || strategyWinner() || "Pendiente")}</p>
      <p><strong>Estrategias formuladas:</strong> ${strategies.length}</p>
      <p><strong>Acciones CAME:</strong> ${cameCount}</p>
    </section>`;
}

function bindModule(moduleId, readonly) {
  document.querySelectorAll("[data-field]").forEach((input) => {
    input.disabled = readonly;
    input.addEventListener("input", () => {
      updateField(moduleId, input.dataset.field, input.value);
      scheduleSave(moduleId, `Campo ${input.dataset.field}`);
    });
  });
  document.querySelectorAll("[data-score]").forEach((input) => {
    input.disabled = readonly;
    input.addEventListener("change", () => {
      updateScore(input.dataset.score, input.value);
      scheduleSave(moduleId, "Valoración");
    });
  });
  
  // Custom bindings for Porter
  if (moduleId === "porter") {
    document.querySelectorAll("[data-porter-id]").forEach(input => {
      input.disabled = readonly;
      input.addEventListener("change", () => {
        const forceId = input.dataset.porterId;
        const force = peti().data.porter.forces.find(f => f.id === forceId);
        if (force) {
          force.score = Number(input.value);
          scheduleSave("porter", `Valoración de ${force.name}`);
        }
      });
    });
    document.querySelectorAll("[data-porter-notes]").forEach(textarea => {
      textarea.disabled = readonly;
      textarea.addEventListener("input", () => {
        const forceId = textarea.dataset.porterNotes;
        const force = peti().data.porter.forces.find(f => f.id === forceId);
        if (force) {
          force.notes = textarea.value.trim();
          scheduleSave("porter", `Observaciones de ${force.name}`);
        }
      });
    });
  }

  // Custom bindings for dynamic strategy matrices
  if (moduleId === "strategy") {
    document.querySelectorAll("[data-matrix-key]").forEach(select => {
      select.disabled = readonly;
      select.addEventListener("change", () => {
        const matrixKey = select.dataset.matrixKey;
        const cellKey = select.dataset.cellKey;

        if (!peti().data.strategy.matrices[matrixKey]) peti().data.strategy.matrices[matrixKey] = {};
        if (select.value === "") {
          delete peti().data.strategy.matrices[matrixKey][cellKey];
        } else {
          peti().data.strategy.matrices[matrixKey][cellKey] = Number(select.value);
        }
        scheduleSave("strategy", `Puntuación de Matriz ${matrixKey.toUpperCase()}`);
        
        const scores = strategyScores();
        document.querySelectorAll("[data-strategy-score]").forEach(td => {
          const relation = td.dataset.strategyScore;
          td.textContent = scores[relation];
        });
        
        const winner = strategyWinner();
        const selectedInput = document.querySelector('[data-field="selected"]');
        if (selectedInput && !selectedInput.value.trim()) {
          selectedInput.value = winner;
        }
      });
    });
  }

  if (moduleId === "pest") {
    document.querySelectorAll("[data-pest-impact]").forEach(select => {
      select.disabled = readonly;
      select.addEventListener("change", () => {
        const index = select.dataset.pestImpact;
        if (!peti().data.pest.impacts) peti().data.pest.impacts = {};
        if (select.value) peti().data.pest.impacts[index] = select.value;
        else delete peti().data.pest.impacts[index];
        scheduleSave("pest", `Clasificación FODA PEST ${Number(index) + 1}`);
      });
    });
  }

  document.querySelectorAll("[data-action]").forEach((btn) => btn.addEventListener("click", () => handleAction(btn.dataset.action, btn.dataset, readonly, moduleId)));
  if (moduleId === "bcg") drawBcg();
  if (moduleId === "pest") drawPest();
}

function updateField(moduleId, key, value) {
  if (moduleId === "valueChain" && key === "summary") peti().data.valueChain.reflections.summary = value;
  else peti().data[moduleId][key] = value;
}

function updateScore(key, value) {
  if (key.startsWith("vc-")) peti().data.valueChain.answers[key.replace("vc-", "")] = Number(value);
  if (key.startsWith("pest-")) peti().data.pest.answers[key.replace("pest-", "")] = Number(value);
}

function handleAction(action, data, readonly, moduleId) {
  const writeActions = [
    "add-value", "add-strategic", "add-specific",
    "add-vc-strength", "add-vc-weakness", "add-product", "add-porter-force",
    "add-strategy-item", "add-came-action"
  ];
  if ((writeActions.includes(action) || action.startsWith("edit-") || action.startsWith("delete-")) && readonly) {
    return toast("Su rol no tiene permiso de edición para este módulo.");
  }
  
  if (action === "history") return openHistory(moduleId);
  if (action === "assign") return openAssign(moduleId);
  if (action === "approve") return approveModule(moduleId);
  if (action === "add-value") return openValueModal();
  
  // Objectives
  if (action === "add-strategic") return openStrategicModal();
  if (action === "add-specific") return openSpecificModal(data.parentId);
  
  if (action === "add-vc-strength") return openTextListModal("valueChain", peti().data.valueChain.reflections.strengths, "Fortaleza detectada");
  if (action === "add-vc-weakness") return openTextListModal("valueChain", peti().data.valueChain.reflections.weaknesses, "Debilidad detectada");
  if (action === "add-product") return openProductModal();
  if (action === "add-porter-force") return openPorterForceModal();
  
  // Strategy formulated
  if (action === "add-strategy-item") return openStrategyModal();
  
  // Matriz CAME
  if (action === "add-came-action") return openCameModal(data.kind);

  if (action.startsWith("edit-")) return editEntity(action.replace("edit-", ""), data.id, data.kind);
  if (action.startsWith("delete-")) return confirmDelete(action.replace("delete-", ""), data.id, data.kind);
}

function field(key, label, value, type = "text", className = "", options = []) {
  if (type === "textarea") {
    return `<label class="${className}">${label}<textarea data-field="${key}">${esc(value || "")}</textarea></label>`;
  }
  if (type === "select") {
    const opts = options.map((opt) => {
      const optionValue = typeof opt === "object" ? opt.value : opt;
      const optionLabel = typeof opt === "object" ? opt.label : opt;
      return `<option value="${esc(optionValue)}" ${optionValue === value ? "selected" : ""}>${esc(optionLabel)}</option>`;
    }).join("");
    return `<label class="${className}">${label}<select data-field="${key}">${opts}</select></label>`;
  }
  return `<label class="${className}">${label}<input data-field="${key}" type="${type}" value="${esc(value ?? "")}" /></label>`;
}

function scoreOptions(name, selected, id) {
  return `<div class="score-options">${scale.map((label, value) => `<label title="${label}"><input type="radio" name="${name}" data-score="${name}" value="${value}" ${Number(selected) === value ? "checked" : ""} />${value}</label>`).join("")}</div>`;
}

function matrixBlock(key) {
  const config = strategyRelations()[key];
  const rows = config.rows;
  const cols = config.cols;

  if (rows.length === 0 || cols.length === 0) {
    return `
      <section class="panel">
        <h3>Matriz ${config.name}</h3>
        <p class="notice">Registre factores FODA correspondientes para habilitar este cruce.</p>
      </section>
    `;
  }

  const headerHtml = `
    <tr>
      <th>FODA / Cruce</th>
      ${cols.map((col, cIndex) => `<th title="${esc(col.text)}">${config.colLetter}${cIndex + 1}</th>`).join("")}
    </tr>
  `;

  const matrixData = peti().data.strategy.matrices[key] || {};

  const bodyHtml = rows.map((row, rIndex) => {
    const cellsHtml = cols.map((col, cIndex) => {
      const cellKey = `${row.id}-${col.id}`;
      const autoValue = autoRelationScore(row, col);
      const overrideValue = matrixData[cellKey];
      const options = [`<option value="">${autoValue}</option>`]
        .concat([0, 1, 2, 3, 4].map((value) => `<option value="${value}" ${Number(overrideValue) === value ? "selected" : ""}>${value}</option>`))
        .join("");
      return `<td><select class="matrix-score-select" title="Auto: ${autoValue}. Cambie solo si desea ajustar este cruce." data-matrix-key="${key}" data-cell-key="${cellKey}">${options}</select></td>`;
    }).join("");

    return `
      <tr>
        <td title="${esc(row.text)}"><strong>${config.rowLetter}${rIndex + 1}</strong></td>
        ${cellsHtml}
      </tr>
    `;
  }).join("");

  const rowLegend = rows.map((row, rIndex) => `<li><strong>${config.rowLetter}${rIndex + 1}:</strong> ${esc(row.text)}</li>`).join("");
  const colLegend = cols.map((col, cIndex) => `<li><strong>${config.colLetter}${cIndex + 1}:</strong> ${esc(col.text)}</li>`).join("");

  return `
    <section class="panel">
      <h3>Matriz ${config.code}: ${config.name}</h3>
      <p class="muted">Los puntajes se calculan automáticamente con los datos registrados. Ajuste una celda solo si el análisis gerencial requiere corregir el cruce.</p>
      <div class="table-wrap">
        <table>
          <thead>
            ${headerHtml}
          </thead>
          <tbody>
            ${bodyHtml}
          </tbody>
        </table>
      </div>
      <div class="form-grid" style="margin-top: 12px; font-size: 0.85rem; border-top: 1px dashed var(--line); padding-top: 8px;">
        <div>
          <strong>Leyenda Filas:</strong>
          <ul style="margin: 4px 0 0 16px; padding: 0;">${rowLegend}</ul>
        </div>
        <div>
          <strong>Leyenda Columnas:</strong>
          <ul style="margin: 4px 0 0 16px; padding: 0;">${colLegend}</ul>
        </div>
      </div>
    </section>
  `;
}

function table(headers, rows) {
  if (!rows.length) return `<p class="notice">Aún no existen registros.</p>`;
  return `<div class="table-wrap"><table><thead><tr>${headers.map((h) => `<th>${h}</th>`).join("")}</tr></thead><tbody>${rows.map((r) => `<tr>${r.map((c) => `<td>${c}</td>`).join("")}</tr>`).join("")}</tbody></table></div>`;
}

function rowActions(type, id, kind = "") {
  return `<div class="action-row"><button class="secondary-btn" data-action="edit-${type}" data-id="${id}" data-kind="${kind}" type="button">Editar</button><button class="danger-btn" data-action="delete-${type}" data-id="${id}" data-kind="${kind}" type="button">Eliminar</button></div>`;
}

function openTextListModal(moduleId, list, title, existing = null) {
  openFormModal(existing ? `Editar ${title}` : `Crear ${title}`, [["text", title, existing?.text || "", "textarea"]], (values) => {
    if (existing) Object.assign(existing, values);
    else list.push({ id: uid("row"), ...values });
    scheduleSave(moduleId, title);
  });
}

function openValueModal(existing = null) {
  openFormModal(existing ? "Editar valor" : "Crear valor", [["name", "Valor", existing?.name || ""], ["description", "Descripción", existing?.description || "", "textarea"]], (values) => {
    if (existing) Object.assign(existing, values);
    else peti().data.values.items.push({ id: uid("val"), ...values });
    scheduleSave("values", "Valor organizacional");
  });
}

function openStrategicModal(existing = null) {
  openFormModal(existing ? "Editar objetivo estratégico" : "Crear objetivo estratégico", [
    ["strategic", "Objetivo estratégico", existing?.strategic || "", "textarea"]
  ], (values) => {
    if (existing) {
      existing.strategic = values.strategic;
    } else {
      peti().data.objectives.rows.push({
        id: `str-${uid("obj")}`,
        strategic: values.strategic,
        specifics: []
      });
    }
    scheduleSave("objectives", "Objetivo estratégico");
  });
}

function openSpecificModal(parentId, existing = null) {
  const parent = peti().data.objectives.rows.find((r) => r.id === parentId);
  if (!parent) return;

  openFormModal(existing ? "Editar objetivo específico" : "Crear objetivo específico", [
    ["name", "Nombre del objetivo específico", existing?.name || ""],
    ["description", "Descripción", existing?.description || "", "textarea"],
    ["indicator", "Indicador", existing?.indicator || ""],
    ["owner", "Responsable", existing?.owner || ""],
    ["status", "Estado", existing?.status || "Pendiente", "select", ["Pendiente", "En proceso", "Completado"]]
  ], (v) => {
    const values = {
      ...v,
      createdAt: existing?.createdAt || new Date().toISOString().split("T")[0]
    };
    if (existing) {
      Object.assign(existing, values);
    } else {
      if (!parent.specifics) parent.specifics = [];
      parent.specifics.push({
        id: `sp-${uid("obj")}`,
        ...values
      });
    }
    scheduleSave("objectives", "Objetivo específico");
  });
}

function openProductModal(existing = null) {
  openFormModal(existing ? "Editar producto BCG" : "Crear producto BCG", [["name", "Producto", existing?.name || ""], ["sales", "Ventas", existing?.sales || "", "number"], ["growth", "Crecimiento (%)", existing?.growth || "", "number"], ["competitorSales", "Ventas mayor competidor", existing?.competitorSales || "", "number"], ["owner", "Responsable", existing?.owner || ""]], (v) => {
    const values = { ...v, sales: Number(v.sales), growth: Number(v.growth), competitorSales: Number(v.competitorSales) };
    if (existing) Object.assign(existing, values);
    else peti().data.bcg.products.push({ id: uid("prod"), ...values });
    scheduleSave("bcg", "Producto BCG");
  });
}

function openPorterForceModal(existing = null) {
  openFormModal(existing ? "Editar fuerza de Porter" : "Registrar fuerza de Porter", [
    ["name", "Fuerza o criterio evaluado", existing?.name || ""],
    ["score", "Valoración (0-4)", existing?.score ?? 2, "number"],
    ["notes", "Observaciones / Análisis", existing?.notes || "", "textarea"]
  ], (v) => {
    const values = {
      ...v,
      score: Math.max(0, Math.min(4, Number(v.score)))
    };
    if (existing) {
      Object.assign(existing, values);
    } else {
      if (!peti().data.porter.forces) peti().data.porter.forces = [];
      peti().data.porter.forces.push({ id: uid("porter"), ...values });
    }
    scheduleSave("porter", "Fuerza de Porter");
  });
}

function openStrategyModal(existing = null) {
  openFormModal(existing ? "Editar estrategia" : "Formular estrategia", [
    ["name", "Nombre de la estrategia", existing?.name || ""],
    ["description", "Descripción", existing?.description || "", "textarea"],
    ["type", "Tipo de estrategia", existing?.type || "FO", "select", ["FO", "AF", "AD", "OD"]],
    ["owner", "Responsable", existing?.owner || ""],
    ["priority", "Prioridad", existing?.priority || "Media", "select", ["Alta", "Media", "Baja"]],
    ["score", "Puntuación (1-5)", existing?.score ?? 3, "number"],
    ["status", "Estado", existing?.status || "Pendiente", "select", ["Pendiente", "En proceso", "Implementado"]]
  ], (v) => {
    const values = {
      ...v,
      score: Number(v.score)
    };
    if (existing) {
      Object.assign(existing, values);
    } else {
      if (!peti().data.strategy.items) peti().data.strategy.items = [];
      peti().data.strategy.items.push({ id: `strg-${uid("str")}`, ...values });
    }
    scheduleSave("strategy", "Estrategia formulada");
  });
}

function openCameModal(kind, existing = null) {
  const strategyOptions = [
    { value: "", label: "Sin estrategia vinculada" },
    ...(peti().data.strategy.items || []).map((strategy) => ({
      value: strategy.id,
      label: `${strategy.type || "Estrategia"} - ${strategy.name || "Sin nombre"}`
    }))
  ];
  openFormModal(existing ? "Editar acción de cambio" : "Registrar acción de cambio", [
    ["action", "Acción de cambio", existing?.action || "", "textarea"],
    ["strategyId", "Estrategia vinculada", existing?.strategyId || "", "select", strategyOptions],
    ["owner", "Responsable", existing?.owner || ""],
    ["startDate", "Fecha de inicio", existing?.startDate || new Date().toISOString().split('T')[0], "date"],
    ["endDate", "Fecha de fin", existing?.endDate || new Date().toISOString().split('T')[0], "date"],
    ["priority", "Prioridad", existing?.priority || "Media", "select", ["Alta", "Media", "Baja"]],
    ["status", "Estado", existing?.status || "Pendiente", "select", ["Pendiente", "En proceso", "Completado"]],
    ["notes", "Observaciones", existing?.notes || "", "textarea"]
  ], (v) => {
    if (existing) {
      Object.assign(existing, v);
    } else {
      if (!peti().data.came.actions[kind]) peti().data.came.actions[kind] = [];
      peti().data.came.actions[kind].push({ id: `came-${uid("came")}`, ...v });
    }
    scheduleSave("came", "Acción de cambio");
  });
}

function openFormModal(title, fields, onSubmit) {
  openModal(title, `<form id="modalForm" class="form-grid">${fields.map(([k, l, v, t = "text", opts = []]) => field(k, l, v, t, "wide", opts)).join("")}</form>`, [
    { text: "Cancelar", className: "ghost-btn", onClick: closeModal },
    { text: "Guardar", className: "primary-btn", onClick: () => {
      const values = {};
      $("#modalForm").querySelectorAll("[data-field]").forEach((input) => { values[input.dataset.field] = input.value.trim(); });
      onSubmit(values);
      closeModal();
    } },
  ]);
}

function editEntity(type, id, kind) {
  if (type === "value") return openValueModal(peti().data.values.items.find((r) => r.id === id));
  if (type === "strategic") return openStrategicModal(peti().data.objectives.rows.find((r) => r.id === id));
  if (type === "specific") {
    const parent = peti().data.objectives.rows.find((r) => r.id === kind);
    const item = parent ? parent.specifics.find((sp) => sp.id === id) : null;
    if (parent && item) return openSpecificModal(kind, item);
  }
  if (type === "vc-strength") return openTextListModal("valueChain", peti().data.valueChain.reflections.strengths, "Fortaleza detectada", peti().data.valueChain.reflections.strengths.find((r) => r.id === id));
  if (type === "vc-weakness") return openTextListModal("valueChain", peti().data.valueChain.reflections.weaknesses, "Debilidad detectada", peti().data.valueChain.reflections.weaknesses.find((r) => r.id === id));
  if (type === "product") return openProductModal(peti().data.bcg.products.find((r) => r.id === id));
  if (type === "porter-force") return openPorterForceModal(peti().data.porter.forces.find((r) => r.id === id));
  if (type === "strategy-item") return openStrategyModal(peti().data.strategy.items.find((r) => r.id === id));
  if (type === "came-action") {
    const list = peti().data.came.actions[kind] || [];
    const item = list.find((r) => r.id === id);
    if (item) return openCameModal(kind, item);
  }
}

function confirmDelete(type, id, kind) {
  let msg = "El registro será eliminado y quedará en historial.";
  if (type === "strategic") msg = "¿Está seguro de que desea eliminar este objetivo estratégico y todos sus objetivos específicos?";
  openModal("Confirmar eliminación", `<p>${msg}</p>`, [
    { text: "Cancelar", className: "ghost-btn", onClick: closeModal },
    { text: "Eliminar", className: "danger-btn", onClick: () => { removeEntity(type, id, kind); closeModal(); } },
  ]);
}

function removeEntity(type, id, kind) {
  if (type === "strategic") {
    const idx = peti().data.objectives.rows.findIndex((r) => r.id === id);
    if (idx >= 0) peti().data.objectives.rows.splice(idx, 1);
  } else if (type === "specific") {
     const parent = peti().data.objectives.rows.find((r) => r.id === kind);
     if (parent) {
       const idx = parent.specifics.findIndex((sp) => sp.id === id);
       if (idx >= 0) parent.specifics.splice(idx, 1);
     }
  } else if (type === "strategy-item") {
    const list = peti().data.strategy.items || [];
    const idx = list.findIndex((r) => r.id === id);
    if (idx >= 0) list.splice(idx, 1);
  } else if (type === "came-action") {
    const list = peti().data.came.actions[kind] || [];
    const idx = list.findIndex((r) => r.id === id);
    if (idx >= 0) list.splice(idx, 1);
  } else {
    const sources = {
      value: peti().data.values.items,
      "vc-strength": peti().data.valueChain.reflections.strengths,
      "vc-weakness": peti().data.valueChain.reflections.weaknesses,
      product: peti().data.bcg.products,
      "porter-force": peti().data.porter.forces,
    };
    const list = sources[type];
    if (list) {
      const idx = list.findIndex((r) => r.id === id);
      if (idx >= 0) list.splice(idx, 1);
    }
  }
  scheduleSave(peti().activeModule, "Eliminación de registro");
}

function openHistory(moduleId) {
  const rows = peti().history.filter((h) => h.moduleId === moduleId);
  openModal("Historial de cambios", `<div class="history-list">${rows.map((h) => `<div class="history-item"><strong>${h.action}</strong><p>${esc(h.detail)}</p><small>${h.user} | ${h.role} | ${h.area} | ${h.at}</small></div>`).join("") || "<p class='muted'>Sin cambios registrados.</p>"}</div>`);
}

function openAssign(moduleId) {
  if (!canAdmin()) {
    toast("Solo el Administrador Empresa puede asignar responsables.");
    return;
  }
  const module = modules.find((m) => m.id === moduleId);
  const candidates = orgUsers().filter((user) => userBelongsToModuleArea(user, moduleId));
  if (!candidates.length) {
    openModal("Sin usuarios del área", `<p class="notice">No hay usuarios del área responsable <strong>${module.owner}</strong>. Cree o actualice un usuario interno de esa área antes de asignar.</p>`);
    return;
  }
  openModal("Asignar responsable", `
    <p class="notice">Solo se muestran usuarios del área responsable del módulo: <strong>${module.owner}</strong>. Esta acción no modifica permisos.</p>
    <label>Responsable
      <select id="assignUser">${candidates.map((u) => `<option value="${u.id}" ${peti().assignedResponsibles?.[moduleId] === u.id ? "selected" : ""}>${esc(u.name)} — ${esc(u.role)} — ${esc(u.area)}</option>`).join("")}</select>
    </label>
  `, [
    { text: "Cancelar", className: "ghost-btn", onClick: closeModal },
    { text: "Asignar", className: "primary-btn", onClick: () => {
      const user = orgUsers().find((u) => u.id === $("#assignUser").value);
      peti().assignedResponsibles[moduleId] = user.id;
      logChange(moduleId, "Asignación de responsable", `${user.name} — ${user.role} — ${user.area}`);
      persist();
      closeModal();
      renderAll();
      toast("Responsable asignado para seguimiento.");
    } },
  ]);
}

function approveModule(moduleId) {
  peti().approvals[moduleId] = { user: currentUser().name, at: now() };
  logChange(moduleId, "Aprobación", `Módulo aprobado por ${currentUser().name}`);
  persist();
  renderAll();
  toast("Módulo aprobado.");
}

function openUsersModal() {
  if (!canAdmin()) return toast("Solo el Administrador Empresa puede gestionar usuarios.");
  openModal("Usuarios internos y permisos", `
    <div class="action-row"><button id="createUserBtn" class="primary-btn" type="button">Crear usuario</button></div>
    ${table(["Nombre", "Correo", "Rol", "Área", "Restricciones", "Acciones"], orgUsers().map((u) => [
      esc(u.name), esc(u.email), esc(u.role), esc(u.area),
      Object.entries(u.modulePermissions || {}).map(([m, p]) => `${label(m)}: ${p === "view" ? "solo lectura" : p}`).join("<br>") || "Según rol y área",
      `<button class="secondary-btn" data-user-perms="${u.id}" type="button">Área y restricciones</button>`,
    ]))}
  `);
  $("#createUserBtn").addEventListener("click", openCreateUserModal);
  document.querySelectorAll("[data-user-perms]").forEach((b) => b.addEventListener("click", () => openUserPermissions(b.dataset.userPerms)));
}

function openCreateUserModal() {
  openModal("Crear usuario interno", `
    <form id="modalForm" class="form-grid">
      ${field("name", "Nombre", "", "text", "wide")}
      ${field("email", "Correo", "", "email", "wide")}
      ${field("password", "Contraseña temporal", "", "text", "wide")}
      <label class="wide">Rol
        <select data-field="role">
          ${Object.keys(rolePermissions).filter((role) => role !== "Administrador Empresa").map((role) => `<option value="${role}">${role}</option>`).join("")}
        </select>
      </label>
      ${field("area", "Área", "", "text", "wide")}
    </form>
  `, [
    { text: "Cancelar", className: "ghost-btn", onClick: closeModal },
    { text: "Crear usuario", className: "primary-btn", onClick: async () => {
      const values = {};
      $("#modalForm").querySelectorAll("[data-field]").forEach((input) => { values[input.dataset.field] = input.value.trim(); });
      if (!values.name || !values.email || !values.password || !values.role) return toast("Complete todos los campos obligatorios.");
      try {
        const payload = await Api.createUser(values);
        db.users = payload.users;
        closeModal();
        toast("Usuario creado dentro de la empresa.");
      } catch (error) {
        toast(error.message);
      }
    } },
  ]);
}

function openUserPermissions(userId) {
  const user = db.users.find((u) => u.id === userId && u.organizationId === currentOrg().id);
  openModal(`Área y restricciones de ${esc(user.name)}`, `
    <div class="form-grid">
      <label>Rol
        <select id="userRoleEdit">${Object.keys(rolePermissions).map((role) => `<option value="${role}" ${user.role === role ? "selected" : ""}>${role}</option>`).join("")}</select>
      </label>
      <label>Área
        <input id="userAreaEdit" value="${esc(user.area)}" />
      </label>
    </div>
    <p class="notice">Editar depende del área responsable del módulo. Estas restricciones solo pueden dejar un módulo en modo lectura; no otorgan edición fuera del área.</p>
    <div class="table-wrap"><table><thead><tr><th>Módulo</th><th>Restricción</th></tr></thead><tbody>${modules.map((m) => `<tr><td>${m.title}</td><td><select data-permission-module="${m.id}"><option value="">Según rol y área</option><option value="view" ${user.modulePermissions?.[m.id] === "view" ? "selected" : ""}>Solo lectura</option></select></td></tr>`).join("")}</tbody></table></div>
  `, [
    { text: "Cancelar", className: "ghost-btn", onClick: closeModal },
    { text: "Guardar", className: "primary-btn", onClick: async () => {
      user.role = $("#userRoleEdit").value;
      user.area = $("#userAreaEdit").value.trim();
      user.modulePermissions = {};
      document.querySelectorAll("[data-permission-module]").forEach((select) => { if (select.value) user.modulePermissions[select.dataset.permissionModule] = select.value; });
      try {
        const payload = await Api.updateUser(user.id, {
          role: user.role,
          area: user.area,
          modulePermissions: user.modulePermissions,
        });
        db.users = payload.users;
        closeModal();
        renderAll();
        toast("Área y restricciones actualizadas.");
      } catch (error) {
        toast(error.message);
      }
    } },
  ]);
}

function porterAverage() {
  const answered = (peti().data.porter.forces || []).filter((i) => i.score !== null);
  return answered.length ? answered.reduce((s, i) => s + Number(i.score), 0) / answered.length : 0;
}

function strategyScores() {
  normalizeStrategyMatrices(peti().data.strategy);
  const relations = strategyRelations();
  return Object.fromEntries(Object.entries(relations).map(([key, relation]) => {
    const total = relation.rows.reduce((sum, row) => (
      sum + relation.cols.reduce((colSum, col) => colSum + matrixCellValue(key, row, col), 0)
    ), 0);
    return [key, total];
  }));
}

function strategyWinner() {
  const scores = strategyScores();
  const entries = Object.entries(scores).sort((a, b) => b[1] - a[1]);
  if (!entries[0] || entries[0][1] === 0) return "";
  return strategyRelations()[entries[0][0]]?.typology || "";
}

function strategyNameById(id) {
  const strategy = (peti().data.strategy.items || []).find((item) => item.id === id);
  if (!strategy) return "";
  return `${strategy.type || "Estrategia"} - ${strategy.name || "Sin nombre"}`;
}

function relativeShare(p) {
  return Number(p.competitorSales || 0) ? Number(p.sales || 0) / Number(p.competitorSales) : 0;
}

function bcgQuadrant(p) {
  const highGrowth = Number(p.growth) >= 10;
  const highShare = relativeShare(p) >= 1;
  if (highGrowth && highShare) return "Estrella";
  if (highGrowth) return "Incógnita";
  if (highShare) return "Vaca";
  return "Perro";
}

function pestCategoryScores() {
  const categories = ["Social", "Político", "Económico", "Tecnológico", "Medioambiental"];
  const answers = peti().data.pest.answers || {};
  return categories.map((category) => {
    const questionIndexes = pestQuestions
      .map((question, index) => ({ ...question, index }))
      .filter((question) => question.category === category)
      .map((question) => question.index);
    const values = questionIndexes
      .map((index) => answers[index])
      .filter((value) => value !== undefined && value !== null && value !== "")
      .map(Number);
    const score = values.length
      ? values.reduce((sum, value) => sum + value, 0) / values.length
      : 0;
    return { category, score, count: values.length, total: questionIndexes.length };
  });
}

function drawPest() {
  const canvas = $("#pestCanvas");
  if (!canvas) return;
  const ctx = canvas.getContext("2d");
  const w = canvas.width;
  const h = canvas.height;
  const data = pestCategoryScores();
  ctx.clearRect(0, 0, w, h);
  ctx.fillStyle = "#ffffff";
  ctx.fillRect(0, 0, w, h);
  ctx.fillStyle = "#0f172a";
  ctx.font = "700 16px Segoe UI";
  ctx.fillText("Valoración promedio PEST", 24, 30);

  const chart = { x: 54, y: 58, w: w - 90, h: h - 112 };
  ctx.strokeStyle = "#dbe2ea";
  ctx.lineWidth = 1;
  for (let i = 0; i <= 4; i += 1) {
    const y = chart.y + chart.h - (i / 4) * chart.h;
    ctx.beginPath();
    ctx.moveTo(chart.x, y);
    ctx.lineTo(chart.x + chart.w, y);
    ctx.stroke();
    ctx.fillStyle = "#64748b";
    ctx.font = "12px Segoe UI";
    ctx.fillText(String(i), 24, y + 4);
  }

  const barGap = 22;
  const barWidth = Math.max(30, (chart.w - barGap * (data.length + 1)) / data.length);
  const colors = ["#2563eb", "#0f766e", "#b7791f", "#c2413b", "#6b7280"];
  data.forEach((item, index) => {
    const x = chart.x + barGap + index * (barWidth + barGap);
    const barHeight = (Math.min(4, item.score) / 4) * chart.h;
    const y = chart.y + chart.h - barHeight;
    ctx.fillStyle = colors[index % colors.length];
    ctx.fillRect(x, y, barWidth, barHeight);
    ctx.fillStyle = "#0f172a";
    ctx.font = "700 12px Segoe UI";
    ctx.fillText(item.score.toFixed(1), x + barWidth / 2 - 10, Math.max(y - 8, chart.y + 12));
    ctx.font = "12px Segoe UI";
    ctx.fillText(item.category.slice(0, 11), x - 4, chart.y + chart.h + 22);
    ctx.fillStyle = "#64748b";
    ctx.fillText(`${item.count}/${item.total}`, x + 4, chart.y + chart.h + 40);
  });

  if (!data.some((item) => item.count)) {
    ctx.fillStyle = "#64748b";
    ctx.font = "14px Segoe UI";
    ctx.fillText("Registre factores PEST para generar el gráfico.", 80, h / 2);
  }
}

function drawBcg() {
  const canvas = $("#bcgCanvas");
  if (!canvas) return;
  const ctx = canvas.getContext("2d");
  const w = canvas.width;
  const h = canvas.height;
  ctx.clearRect(0, 0, w, h);
  [["#fef3c7", 70, 30], ["#ecfeff", w / 2 + 20, 30], ["#fee2e2", 70, h / 2 - 15], ["#dcfce7", w / 2 + 20, h / 2 - 15]].forEach(([c, x, y]) => { ctx.fillStyle = c; ctx.fillRect(x, y, (w - 100) / 2, (h - 90) / 2); });
  ctx.strokeStyle = "#334155"; ctx.strokeRect(70, 30, w - 100, h - 90);
  ctx.beginPath(); ctx.moveTo(w / 2 + 20, 30); ctx.lineTo(w / 2 + 20, h - 60); ctx.moveTo(70, h / 2 - 15); ctx.lineTo(w - 30, h / 2 - 15); ctx.stroke();
  ctx.fillStyle = "#0f172a"; ctx.font = "700 14px Segoe UI";
  [["Incógnita", 105, 58], ["Estrella", w / 2 + 55, 58], ["Perro", 105, h / 2 + 18], ["Vaca", w / 2 + 55, h / 2 + 18]].forEach(([t, x, y]) => ctx.fillText(t, x, y));
  const products = peti().data.bcg.products;
  const maxSales = Math.max(1, ...products.map((p) => Number(p.sales || 0)));
  products.forEach((p, i) => {
    const x = 70 + (Math.min(2, relativeShare(p)) / 2) * (w - 100);
    const y = h - 60 - (Math.max(0, Math.min(30, Number(p.growth || 0))) / 30) * (h - 90);
    const r = 12 + (Number(p.sales || 0) / maxSales) * 20;
    ctx.beginPath(); ctx.fillStyle = ["#2563eb", "#0f766e", "#b7791f", "#c2413b"][i % 4]; ctx.arc(x, y, r, 0, Math.PI * 2); ctx.fill();
    ctx.fillStyle = "#111827"; ctx.font = "700 12px Segoe UI"; ctx.fillText(p.name.slice(0, 16), Math.min(x + r + 4, w - 130), y + 4);
  });
}

function openModal(title, body, actions = [{ text: "Cerrar", className: "primary-btn", onClick: closeModal }]) {
  $("#modalTitle").textContent = title;
  $("#modalBody").innerHTML = body;
  $("#modalFooter").innerHTML = "";
  actions.forEach((a) => {
    const btn = document.createElement("button");
    btn.type = "button"; btn.className = a.className; btn.textContent = a.text; btn.addEventListener("click", a.onClick);
    $("#modalFooter").appendChild(btn);
  });
  $("#modalBackdrop").classList.remove("hidden");
}

function closeModal() {
  $("#modalBackdrop").classList.add("hidden");
}

function openExportModal() {
  openModal("Exportar PETI", `
    <div class="export-options">
      <button class="export-card" id="exportPdfBtn" type="button">
        <strong>Exportar PDF</strong>
        <span>Reporte ejecutivo listo para imprimir, presentar o guardar como PDF.</span>
      </button>
      <button class="export-card" id="exportExcelBtn" type="button">
        <strong>Exportar Excel</strong>
        <span>Tablas del PETI para revisión, análisis y entregables internos.</span>
      </button>
      ${canAdmin() ? `<button class="export-card technical" id="exportJsonBtn" type="button"><strong>Exportar JSON técnico</strong><span>Respaldo estructurado para pruebas o migración.</span></button>` : ""}
    </div>
  `);
  $("#exportPdfBtn").addEventListener("click", exportPdf);
  $("#exportExcelBtn").addEventListener("click", exportExcel);
  if ($("#exportJsonBtn")) $("#exportJsonBtn").addEventListener("click", exportJson);
}

function exportPayload() {
  return { empresa: currentOrg().name, usuario: { ...currentUser(), password: undefined }, avance: `${progressCount()}/${modules.length}`, generado: now(), peti: peti().data, historial: peti().history, aprobaciones: peti().approvals };
}

function reportHtml() {
  const d = peti().data;
  const vc = valueChainStats();
  const strategyScoreData = Object.entries(strategyRelations()).map(([key, relation]) => ({
    label: relation.code,
    description: relation.typology,
    value: strategyScores()[key] || 0
  }));
  const foda = {
    strengths: getFodaStrengths(),
    opportunities: getFodaOpportunities(),
    weaknesses: getFodaWeaknesses(),
    threats: getFodaThreats()
  };
  const cameLabels = { correct: "Corregir", confront: "Afrontar", maintain: "Mantener", exploit: "Explotar" };
  const objectiveRows = (d.objectives.rows || []).flatMap((objective) => {
    const specifics = objective.specifics || [];
    if (!specifics.length) return [[esc(objective.strategic), "Pendiente", "Pendiente", "Pendiente", "Pendiente"]];
    return specifics.map((specific) => [
      esc(objective.strategic),
      esc(specific.name),
      esc(specific.indicator),
      esc(specific.owner),
      esc(specific.status || "Pendiente")
    ]);
  });
  const fodaRows = Array.from({
    length: Math.max(foda.strengths.length, foda.opportunities.length, foda.weaknesses.length, foda.threats.length, 1)
  }, (_, i) => [
    esc(foda.strengths[i]?.text || ""),
    esc(foda.opportunities[i]?.text || ""),
    esc(foda.weaknesses[i]?.text || ""),
    esc(foda.threats[i]?.text || "")
  ]);
  const strategyRows = (d.strategy.items || []).map((item) => [
    esc(item.type),
    esc(item.name),
    esc(item.description),
    esc(item.owner),
    esc(item.status || "Pendiente")
  ]);
  const pestRows = pestCategoryScores().map((item) => [
    esc(item.category),
    `${item.count}/${item.total}`,
    item.score.toFixed(2)
  ]);
  const cameRows = Object.entries(d.came.actions).flatMap(([kind, rows]) => rows.map((row) => [
    esc(cameLabels[kind] || kind),
    esc(row.action || ""),
    esc(strategyNameById(row.strategyId) || "Sin vínculo"),
    esc(row.owner || ""),
    `${esc(row.startDate || "")} - ${esc(row.endDate || "")}`,
    esc(row.status || "Pendiente")
  ]));
  return `
    <html><head><title>PETI ${esc(currentOrg().name)}</title><style>
      body{font-family:Segoe UI,Arial,sans-serif;color:#172033;margin:32px;line-height:1.45}
      h1{font-size:28px;margin:0 0 8px} h2{margin-top:26px;border-bottom:1px solid #dbe2ea;padding-bottom:6px}
      table{width:100%;border-collapse:collapse;margin:10px 0 18px} th,td{border:1px solid #dbe2ea;padding:8px;text-align:left;vertical-align:top} th{background:#eef2f7}
      .kpi{display:inline-block;border:1px solid #dbe2ea;border-radius:8px;padding:10px 14px;margin:6px 8px 6px 0}
      .bar-chart{display:grid;gap:8px;margin:10px 0 18px}.bar-row{display:grid;grid-template-columns:150px 1fr 48px;gap:10px;align-items:center}.bar-track{height:18px;background:#eef2f7;border:1px solid #dbe2ea}.bar-fill{height:100%;background:#3b82f6}
    </style></head><body>
      <h1>Plan Estratégico de TI</h1>
      <p><strong>Empresa:</strong> ${esc(currentOrg().name)} | <strong>RUC:</strong> ${esc(d.info.ruc || "")} | <strong>Generado:</strong> ${now()}</p>
      <div class="kpi"><strong>Avance:</strong> ${progressCount()}/${modules.length}</div>
      <div class="kpi"><strong>Cadena de valor:</strong> ${vc.strength}%</div>
      <div class="kpi"><strong>Potencial de mejora:</strong> ${vc.improvement}%</div>
      <h2>Empresa</h2><p>${esc(d.info.description || "")}</p>
      <h2>Misión</h2><p>${esc(d.mission.text || "Pendiente")}</p>
      <h2>Visión</h2><p>${esc(d.vision.text || "Pendiente")}</p>
      <h2>Valores</h2><ul>${d.values.items.map((v) => `<li><strong>${esc(v.name)}</strong>: ${esc(v.description)}</li>`).join("")}</ul>
      <h2>Objetivos</h2>${table(["Estratégico","Específico","Indicador","Responsable","Estado"], objectiveRows)}
      <h2>FODA automático</h2>${table(["Fortalezas","Oportunidades","Debilidades","Amenazas"], fodaRows)}
      <h2>Cadena de Valor</h2><p>${vc.interpretation}. Potencial actual ${vc.strength}%, mejora ${vc.improvement}%.</p>
      <h2>BCG</h2>${table(["Producto","Ventas","Crecimiento","Participación","Cuadrante"], d.bcg.products.map((p) => [esc(p.name), money(p.sales), `${p.growth}%`, relativeShare(p).toFixed(2), bcgQuadrant(p)]))}
      <h2>PEST</h2>${table(["Categoría","Valoraciones registradas","Valoración promedio"], pestRows)}${reportBarChart(pestCategoryScores().map((item) => ({ label: item.category, value: item.score })), 4)}
      <h2>Estrategia</h2><p>${esc(d.strategy.selected || strategyWinner() || "Pendiente")}</p>
      ${reportBarChart(strategyScoreData, Math.max(1, ...strategyScoreData.map((item) => item.value)))}
      <h2>Estrategias formuladas</h2>${table(["Tipo","Nombre","Descripción","Responsable","Estado"], strategyRows)}
      <h2>Matriz CAME</h2>${table(["Bloque","Acción","Estrategia vinculada","Responsable","Plazo","Estado"], cameRows)}
      <h2>Conclusiones</h2><p>${esc(d.executive.conclusions || "Pendiente")}</p>
    </body></html>`;
}

function reportBarChart(items, maxValue) {
  return `<div class="bar-chart">${items.map((item) => {
    const value = Number(item.value || 0);
    const width = maxValue ? Math.max(0, Math.min(100, (value / maxValue) * 100)) : 0;
    return `<div class="bar-row"><strong>${esc(item.label)}</strong><div class="bar-track"><div class="bar-fill" style="width:${width}%"></div></div><span>${value.toFixed(value % 1 ? 1 : 0)}</span></div>`;
  }).join("")}</div>`;
}

function exportPdf() {
  const win = window.open("", "_blank");
  win.document.write(reportHtml());
  win.document.close();
  win.focus();
  win.print();
}

function exportExcel() {
  const html = reportHtml();
  downloadBlob(`PETI-${currentOrg().name.replaceAll(" ", "-")}.xls`, "application/vnd.ms-excel", html);
  toast("Excel exportado.");
}

function exportJson() {
  const data = { empresa: currentOrg().name, usuario: { ...currentUser(), password: undefined }, avance: `${progressCount()}/${modules.length}`, generado: now(), peti: peti().data, historial: peti().history, aprobaciones: peti().approvals };
  downloadBlob(`PETI-${currentOrg().name.replaceAll(" ", "-")}.json`, "application/json", JSON.stringify(data, null, 2));
  toast("JSON técnico exportado.");
}

function downloadBlob(filename, type, content) {
  const url = URL.createObjectURL(new Blob([content], { type }));
  const a = document.createElement("a");
  a.href = url;
  a.download = filename;
  a.click();
  URL.revokeObjectURL(url);
}

function label(moduleId) {
  return modules.find((m) => m.id === moduleId)?.title || moduleId;
}

function money(value) {
  return Number(value || 0).toLocaleString("es-PE", { style: "currency", currency: "PEN", maximumFractionDigits: 0 });
}

function esc(value) {
  return String(value ?? "").replaceAll("&", "&amp;").replaceAll("<", "&lt;").replaceAll(">", "&gt;").replaceAll('"', "&quot;").replaceAll("'", "&#039;");
}

function toast(message) {
  const node = document.createElement("div");
  node.className = "toast-message";
  node.textContent = message;
  $("#toast").appendChild(node);
  setTimeout(() => node.remove(), 3600);
}

$("#loginForm").addEventListener("submit", login);
$("#registerForm").addEventListener("submit", registerCompany);
$("#showRegisterBtn").addEventListener("click", () => $("#registerForm").classList.toggle("hidden"));
$("#logoutBtn").addEventListener("click", logout);
$("#modalClose").addEventListener("click", closeModal);
$("#helpBtn").addEventListener("click", () => openModal("Ayuda contextual", `<p>${modules.find((m) => m.id === peti().activeModule).help}</p>`));
$("#exportBtn").addEventListener("click", openExportModal);
$("#manageUsersBtn").addEventListener("click", openUsersModal);
$("#menuBtn").addEventListener("click", () => $(".sidebar").classList.toggle("open"));

async function initApp() {
  if (!Api.token()) return;
  try {
    const payload = await Api.me();
    applySession(payload);
    $("#loginView").classList.add("hidden");
    $("#appView").classList.remove("hidden");
    renderAll();
  } catch {
    Api.clearToken();
  }
}

initApp();

const sqlite3 = require("sqlite3").verbose();
const path = require("path");
const crypto = require("crypto");
const { hashPassword } = require("./backend/middleware/security");

const dbPath = path.resolve(__dirname, "./data/peti.sqlite");
console.log("Seeding SQLite database at:", dbPath);

const db = new sqlite3.Database(dbPath);

const orgId = "org-caja-tacna";
const userId = "usr-caja-tacna-admin";

const companyName = "Caja Municipal de Ahorro y Crédito de Tacna S.A.";
const ruc = "20130098488";
const email = "wquispe@cmactacna.com.pe";
const adminName = "Ing. Wilber Quispe Pérez";
const password = "cajatacna2026";
const passwordHash = hashPassword(password);

// 25 Value Chain affirmations (answers must be 0-4)
// We will distribute the scores to reflect a solid financial standing but with clear technology opportunities.
const vcAnswers = {};
const vcScores = [
  3, // 1. Política cero defectos (Bueno)
  2, // 2. Medios productivos avanzados (Regular - Legacy Core)
  3, // 3. Sistema control gestión (Bueno)
  2, // 4. Preparados tecnológico L.P. (Regular)
  1, // 5. Referente I+D+i (Bajo)
  2, // 6. Excelencia procedimientos (Regular - Manuales)
  3, // 7. Web para clientes (Bueno)
  2, // 8. Tecnología difícil imitar (Regular)
  3, // 9. Cadena producción en costes (Bueno)
  2, // 10. Informatización ventaja (Regular)
  3, // 11. Canales distribución (Bueno - Presencia regional)
  3, // 12. Productos valorados (Bueno)
  3, // 13. Plan marketing (Bueno)
  4, // 14. Gestión financiera (Excelente - SBS)
  3, // 15. Relación clientes (Bueno)
  2, // 16. Lanzamiento productos (Regular)
  3, // 17. RRHH principal activo (Bueno)
  3, // 18. Plantilla motivada (Bueno)
  3, // 19. Estrategia y metas claras (Bueno)
  4, // 20. Gestión circulante (Excelente)
  3, // 21. Posicionamiento productos (Bueno)
  4, // 22. Reputación y marca (Excelente - Liderazgo)
  4, // 23. Cartera fidelizada (Excelente - Sur peruano)
  3, // 24. Equipo ventas (Bueno)
  4  // 25. Servicio al cliente (Excelente)
];
vcScores.forEach((score, idx) => {
  vcAnswers[idx] = score;
});

// 25 PEST questions answers (0-4) and classification
const pestAnswers = {};
const pestImpacts = {};

// Questions mapping:
// Social: 0-4
// Político: 5-9
// Económico: 10-14
// Tecnológico: 15-19
// Medioambiental: 20-24
const pestScores = [
  2, 2, 3, 2, 3, // Social (2, 2, 3, 2, 3)
  2, 3, 3, 3, 2, // Político (2, 3, 3, 3, 2)
  2, 2, 3, 3, 2, // Económico (2, 2, 3, 3, 2)
  4, 4, 4, 4, 4, // Tecnológico (Todos en 4 - Gran impacto y relevancia de NTIC)
  2, 3, 2, 3, 2  // Medioambiental (2, 3, 2, 3, 2)
];

pestScores.forEach((score, idx) => {
  pestAnswers[idx] = score;
});

// Standard PEST factor classifications for FODA:
pestImpacts[11] = "Oportunidad"; // Globalización (Económico)
pestImpacts[16] = "Oportunidad"; // Comercio electrónico / NTIC (Tecnológico)
pestImpacts[17] = "Oportunidad"; // Empleo generalizado de NTIC (Tecnológico)
pestImpacts[18] = "Oportunidad"; // Referente en aplicaciones (Tecnológico)
pestImpacts[19] = "Oportunidad"; // Innovar constantemente (Tecnológico)
pestImpacts[6]  = "Amenaza";     // Legislación laboral estricta (Político)
pestImpacts[5]  = "Amenaza";     // Legislación fiscal (Político)

// Setup unique sub-ids
const v1 = `val-${crypto.randomUUID()}`;
const v2 = `val-${crypto.randomUUID()}`;
const v3 = `val-${crypto.randomUUID()}`;
const v4 = `val-${crypto.randomUUID()}`;
const v5 = `val-${crypto.randomUUID()}`;

const obj1 = `str-${crypto.randomUUID()}`;
const obj2 = `str-${crypto.randomUUID()}`;
const obj3 = `str-${crypto.randomUUID()}`;

const sp1 = `sp-${crypto.randomUUID()}`;
const sp2 = `sp-${crypto.randomUUID()}`;
const sp3 = `sp-${crypto.randomUUID()}`;
const sp4 = `sp-${crypto.randomUUID()}`;
const sp5 = `sp-${crypto.randomUUID()}`;

const prod1 = `prod-${crypto.randomUUID()}`;
const prod2 = `prod-${crypto.randomUUID()}`;
const prod3 = `prod-${crypto.randomUUID()}`;
const prod4 = `prod-${crypto.randomUUID()}`;

const pf1 = `porter-${crypto.randomUUID()}`;
const pf2 = `porter-${crypto.randomUUID()}`;
const pf3 = `porter-${crypto.randomUUID()}`;
const pf4 = `porter-${crypto.randomUUID()}`;
const pf5 = `porter-${crypto.randomUUID()}`;

const cp1 = `cpest-${crypto.randomUUID()}`;
const cp2 = `cpest-${crypto.randomUUID()}`;
const cp3 = `cpest-${crypto.randomUUID()}`;
const cp4 = `cpest-${crypto.randomUUID()}`;

const st1 = `strg-${crypto.randomUUID()}`;
const st2 = `strg-${crypto.randomUUID()}`;
const st3 = `strg-${crypto.randomUUID()}`;
const st4 = `strg-${crypto.randomUUID()}`;

const came1 = `came-${crypto.randomUUID()}`;
const came2 = `came-${crypto.randomUUID()}`;
const came3 = `came-${crypto.randomUUID()}`;
const came4 = `came-${crypto.randomUUID()}`;

const vcStrId1 = `vcs-${crypto.randomUUID()}`;
const vcStrId2 = `vcs-${crypto.randomUUID()}`;
const vcWkId1 = `vcw-${crypto.randomUUID()}`;
const vcWkId2 = `vcw-${crypto.randomUUID()}`;

// Build the structured PETI object
const petiData = {
  activeModule: "executive",
  approvals: {
    info: { user: adminName, at: "24/06/2026" },
    mission: { user: adminName, at: "24/06/2026" },
    vision: { user: adminName, at: "24/06/2026" },
    values: { user: adminName, at: "24/06/2026" },
    objectives: { user: adminName, at: "24/06/2026" },
    valueChain: { user: adminName, at: "24/06/2026" },
    bcg: { user: adminName, at: "24/06/2026" },
    porter: { user: adminName, at: "24/06/2026" },
    pest: { user: adminName, at: "24/06/2026" },
    swot: { user: adminName, at: "24/06/2026" },
    strategy: { user: adminName, at: "24/06/2026" },
    came: { user: adminName, at: "24/06/2026" },
    executive: { user: adminName, at: "24/06/2026" }
  },
  assignedResponsibles: {},
  history: [
    { user: adminName, area: "Gerencia General", at: "24/06/2026, 18:00", module: "info", action: "Aprobación", details: "Módulo aprobado y validado para auditoría" }
  ],
  versions: [
    { id: "v1", version: "1.0", user: adminName, date: "24/06/2026", comment: "Línea base del plan estratégico aprobada por la gerencia" }
  ],
  notifications: [],
  data: {
    info: {
      name: companyName,
      ruc: ruc,
      sector: "Intermediación Financiera / Banca / Microfinanzas",
      employees: "650",
      manager: "Gerencia Mancomunada (Luis Ramos / María López)",
      tiLead: adminName,
      description: "Caja Tacna es una institución financiera líder en el sur del Perú, regulada por la SBS, dedicada a proveer servicios de ahorro, crédito e inclusión financiera a las micro y pequeñas empresas, así como a las familias de la región Tacna y a nivel nacional con altos estándares de calidad."
    },
    mission: {
      text: "Promover la inclusión financiera y el bienestar económico y social de las micro, pequeñas y medianas empresas, así como de las familias del sur del Perú y del país entero, brindando soluciones financieras y de crédito ágiles, accesibles, seguras e innovadoras mediante canales modernos y un servicio altamente humano y confiable."
    },
    vision: {
      text: "Ser la institución microfinanciera líder, digitalmente consolidada y referente en el sur del país en innovación tecnológica, reconocida por la excelencia y ciberseguridad en la atención a sus clientes y por su aporte sostenible al desarrollo socioeconómico en los próximos cinco años."
    },
    values: {
      items: [
        { id: v1, name: "Integridad", description: "Actuamos con total transparencia, ética y honestidad en cada operación y relación con nuestros clientes." },
        { id: v2, name: "Innovación", description: "Fomentamos la adopción de tecnologías digitales avanzadas para optimizar y modernizar nuestros servicios bancarios." },
        { id: v3, name: "Compromiso Social", description: "Contribuimos activamente al crecimiento y desarrollo económico de Tacna y del Perú promoviendo la inclusión financiera de las microempresas." },
        { id: v4, name: "Excelencia en el Servicio", description: "Brindamos un servicio ágil, oportuno, amable y de alta calidad técnica y humana." },
        { id: v5, name: "Seguridad", description: "Garantizamos la confidencialidad, integridad y disponibilidad de la información financiera y operativa de nuestros clientes." }
      ]
    },
    objectives: {
      uen: "1. UEN Microfinanzas y Créditos Empresariales (Créditos a Mypes, Agrocréditos y Créditos de Consumo).\n2. UEN Ahorro y Captaciones (Cuentas de ahorro tradicional, depósitos a plazo fijo y CTS).\n3. UEN Canales Digitales y Banca Alternativa (App Caja Tacna Móvil, Home Banking, Cajeros Corresponsales y Red de Oficinas).",
      rows: [
        {
          id: obj1,
          strategic: "Implementar la transformación digital del core financiero y canales de atención.",
          specifics: [
            { id: sp1, name: "Migración del Core Bancario", description: "Migrar e implementar el nuevo Core Bancario en un 100% de las oficinas y agencias de Caja Tacna.", indicator: "Porcentaje de oficinas operando bajo el nuevo Core Bancario.", owner: "Gerencia de TI y Comunicaciones", status: "En proceso", createdAt: "2026-06-24" },
            { id: sp2, name: "Crecimiento de Canales Digitales", description: "Incrementar las transacciones por canales digitales (App Móvil y Banca por Internet) en un 50% en los próximos 18 meses.", indicator: "Número mensual de transacciones procesadas por canales digitales.", owner: "Jefatura de Canales Digitales", status: "En proceso", createdAt: "2026-06-24" }
          ]
        },
        {
          id: obj2,
          strategic: "Fortalecer la ciberseguridad y la gestión de continuidad del negocio financiero.",
          specifics: [
            { id: sp3, name: "Certificación ISO 27001 e implementación de SOC", description: "Implementar un Centro de Operaciones de Seguridad (SOC) permanente y certificar la norma ISO/IEC 27001 en seguridad de la información.", indicator: "Certificado ISO 27001 obtenido y SOC 100% operativo.", owner: "Jefatura de Seguridad de la Información", status: "En proceso", createdAt: "2026-06-24" },
            { id: sp4, name: "Reducción de Incidentes", description: "Reducir el índice de incidentes y fraudes electrónicos en los canales digitales a menos del 0.05% de transacciones anuales.", indicator: "Tasa de fraudes e incidentes de seguridad reportados por mes.", owner: "Jefatura de Seguridad de la Información", status: "Pendiente", createdAt: "2026-06-24" }
          ]
        },
        {
          id: obj3,
          strategic: "Optimizar la eficiencia operativa y administrativa mediante automatización.",
          specifics: [
            { id: sp5, name: "Evaluación Crediticia Analítica", description: "Automatizar el flujo de evaluación crediticia para microcréditos mediante motores de decisión analíticos basados en Machine Learning.", indicator: "Tiempo promedio de aprobación de microcréditos (horas).", owner: "Jefatura de Procesos TI", status: "En proceso", createdAt: "2026-06-24" }
          ]
        }
      ]
    },
    valueChain: {
      answers: vcAnswers,
      reflections: {
        strengths: [
          { id: vcStrId1, text: "Sólido posicionamiento y reputación de marca regional en Tacna y el sur peruano con altos niveles de fidelidad del cliente microempresario." },
          { id: vcStrId2, text: "Red de distribución consolidada con presencia estratégica de agencias y fuerte vocación de servicio al cliente presencial." }
        ],
        weaknesses: [
          { id: vcWkId1, text: "Brecha en la infraestructura tecnológica y dependencia de un Core Bancario antiguo (legacy) que limita la velocidad de respuesta comercial." },
          { id: vcWkId2, text: "Procesos operativos y de otorgamiento de créditos aún con componentes manuales que restringen la eficiencia en costes." }
        ],
        summary: "El autodiagnóstico revela que Caja Tacna posee un gran capital de posicionamiento, reputación de marca y fidelidad de clientes en la región sur. Sin embargo, se identifican importantes brechas tecnológicas asociadas a la antigüedad de su infraestructura de software (core bancario actual y procesos manuales de evaluación). Esto limita la escalabilidad de costos y el lanzamiento ágil de nuevos productos digitales, lo cual es crítico frente a la expansión de competidores más tecnificados."
      }
    },
    bcg: {
      products: [
        { id: prod1, name: "Crédito Pyme (Micro y Pequeña Empresa)", sales: 280000000, growth: 4, competitorSales: 220000000, owner: "Gerencia de Créditos" },
        { id: prod2, name: "Banca Digital y App Caja Tacna Móvil (Servicios)", sales: 45000000, growth: 25, competitorSales: 80000000, owner: "Jefatura de Canales Digitales" },
        { id: prod3, name: "Depósito a Plazo Fijo Digital", sales: 90000000, growth: 15, competitorSales: 75000000, owner: "Gerencia de Operaciones y Finanzas" },
        { id: prod4, name: "Créditos Prendarios tradicionales", sales: 12000000, growth: -2, competitorSales: 15000000, owner: "Gerencia de Créditos" }
      ]
    },
    porter: {
      forces: [
        { id: pf1, name: "Poder de negociación de los clientes", score: 3, notes: "El sector microfinanciero tiene alta fidelización en el sur del país, aunque los clientes de consumo y medianas empresas ejercen cierta presión por mejores tasas." },
        { id: pf2, name: "Poder de negociación de los proveedores", score: 2, notes: "Caja Tacna depende de proveedores globales de tecnología de información (core banking, ciberseguridad, nube). La transición a nuevos proveedores tiene altos costos de cambio, pero hay alternativas competitivas." },
        { id: pf3, name: "Amenaza de nuevos competidores entrantes", score: 2, notes: "El ingreso de fintechs, bancos 100% digitales e instituciones financieras externas que se expanden a Tacna incrementa la rivalidad. Las regulaciones de la SBS actúan como barrera de entrada." },
        { id: pf4, name: "Amenaza de productos y servicios sustitutos", score: 2, notes: "Billeteras digitales no bancarias (Yape, Plin), cooperativas de ahorro locales y el financiamiento informal son sustitutos que capturan parte del mercado de microcrédito rápido." },
        { id: pf5, name: "Rivalidad entre competidores existentes", score: 1, notes: "Intensa competencia en Tacna y el sur del país por parte de otras CMAC (Caja Arequipa, Caja Cusco) y bancos comerciales (Mibanco, BCP) que ofrecen productos similares y tasas agresivas." }
      ]
    },
    pest: {
      answers: pestAnswers,
      impacts: pestImpacts,
      factors: [],
      customFactors: [
        { id: cp1, type: "Tecnológico", description: "Acelerada adopción de billeteras digitales (Yape, Plin) y pagos sin contacto en el comercio informal de Tacna.", rating: 4, classification: "Oportunidad" },
        { id: cp2, type: "Político", description: "Incertidumbre política nacional y regulaciones más estrictas de la SBS sobre tasas de interés máximas y provisiones bancarias.", rating: 1, classification: "Amenaza" },
        { id: cp3, type: "Económico", description: "Crecimiento del comercio transfronterizo en Tacna y dinamismo de la Zona Franca (Zofratacna).", rating: 4, classification: "Oportunidad" },
        { id: cp4, type: "Social", description: "Aumento de fraudes e ingeniería social orientada a usuarios de banca digital de la región Tacna.", rating: 1, classification: "Amenaza" }
      ]
    },
    swot: {
      strengths: [],
      opportunities: [],
      weaknesses: [],
      threats: []
    },
    strategy: {
      matrices: {
        fo: {},
        af: {},
        ad: {},
        od: {}
      },
      selected: "Estrategia Ofensiva (Crecimiento y Transformación Digital)",
      reflection: "Caja Tacna debe aprovechar su arraigo local y la fidelidad de sus clientes junto con la creciente demanda de digitalización del comercio local para consolidarse en el sector. A pesar de la rivalidad competitiva y la debilidad del core bancario heredado, la migración a sistemas modernos en la nube permitirá capturar la demanda transfronteriza y expandir la cartera digital reduciendo costos de transacción y riesgos de fraude.",
      items: [
        { id: st1, name: "Implementación de la plataforma de Microcrédito Digital Integrado", description: "Desarrollar una solución móvil ágil para evaluación y desembolso inmediato de préstamos pyme, integrando motores de decisión y pasarelas de pago locales.", type: "FO", owner: "Gerencia de Negocios", priority: "Alta", score: 5, status: "En proceso" },
        { id: st2, name: "Campaña regional de ciberseguridad preventiva y autenticación multifactor", description: "Desplegar campañas de concientización a clientes microempresarios y reforzar la seguridad transaccional con biometría móvil y tokenización digital.", type: "AF", owner: "Jefatura de Seguridad de la Información", priority: "Alta", score: 4, status: "En proceso" },
        { id: st3, name: "Migración completa del Core Bancario a arquitectura en la nube", description: "Actualizar la plataforma transaccional principal para dotar de flexibilidad, alta disponibilidad y escalabilidad al procesamiento bancario digital de Caja Tacna.", type: "OD", owner: "Gerencia de TI y Comunicaciones", priority: "Alta", score: 5, status: "En proceso" },
        { id: st4, name: "Plan integral de redundancia tecnológica y continuidad operativa", description: "Habilitar un sitio secundario de recuperación de desastres (DRP) en la nube para mitigar riesgos de caídas del sistema bancario principal.", type: "AD", owner: "Gerencia de TI y Comunicaciones", priority: "Media", score: 3, status: "Pendiente" }
      ]
    },
    came: {
      actions: {
        exploit: [
          { id: came1, action: "Desarrollo e integración de la API de Caja Tacna Móvil con pasarelas de pago y billeteras digitales (Yape/Plin).", strategyId: st1, owner: "Jefatura de Desarrollo de Software", startDate: "2026-07-01", endDate: "2026-12-31", priority: "Alta", status: "En proceso", notes: "Facilitará que los comerciantes minoristas de Tacna reciban y paguen créditos directamente mediante códigos QR." }
        ],
        confront: [
          { id: came2, action: "Implementación de un sistema de prevención de fraude basado en machine learning para canales móviles.", strategyId: st2, owner: "Jefatura de Seguridad de la Información", startDate: "2026-08-01", endDate: "2026-11-30", priority: "Alta", status: "En proceso", notes: "Analiza patrones de comportamiento sospechosos en tiempo real para bloquear transacciones inusuales." }
        ],
        correct: [
          { id: came3, action: "Migración de datos históricos de clientes hacia el nuevo core bancario y capacitación técnica de administradores de bases de datos.", strategyId: st3, owner: "Gerencia de TI y Comunicaciones", startDate: "2026-07-01", endDate: "2027-03-31", priority: "Alta", status: "En proceso", notes: "Fase crítica previa al apagón tecnológico del sistema core legacy." }
        ],
        maintain: [
          { id: came4, action: "Programa anual de mantenimiento y soporte de hardware en agencias del sur con alta demanda transaccional.", strategyId: st4, owner: "Jefatura de Soporte y Operaciones TI", startDate: "2026-09-01", endDate: "2027-08-31", priority: "Media", status: "Pendiente", notes: "Garantizar la disponibilidad física del servicio bancario en agencias tradicionales." }
        ]
      }
    },
    executive: {
      promoters: "Gerencia Mancomunada de Caja Tacna (Luis Ramos / María López) y Ing. Wilber Quispe Pérez (Responsable TI).",
      conclusions: "El Plan Estratégico de TI (PETI) 2026-2030 de Caja Tacna constituye una hoja de ruta fundamental para impulsar la competitividad de la institución. Al alinear los objetivos de negocio de inclusión y crecimiento de cartera con iniciativas de TI clave como la migración del Core Bancario legacy y el fortalecimiento del SOC de ciberseguridad, Caja Tacna estará en condiciones de ofrecer canales ágiles, seguros y escalables. La ejecución de las acciones CAME formuladas mitigará la rivalidad de competidores nacionales y blindará las transacciones digitales, asegurando la sostenibilidad financiera y el liderazgo institucional en la región sur del Perú."
    }
  }
};

db.serialize(() => {
  // Clear any existing data for isolation
  db.run("DELETE FROM users");
  db.run("DELETE FROM petis");
  db.run("DELETE FROM organizations");

  // Insert Organization
  const stmtOrg = db.prepare("INSERT INTO organizations (id, name, ruc) VALUES (?, ?, ?)");
  stmtOrg.run(orgId, companyName, ruc);
  stmtOrg.finalize();

  // Insert PETI
  const stmtPeti = db.prepare("INSERT INTO petis (organization_id, data) VALUES (?, ?)");
  stmtPeti.run(orgId, JSON.stringify(petiData));
  stmtPeti.finalize();

  // Insert Admin User
  const stmtUser = db.prepare(
    "INSERT INTO users (id, organization_id, name, email, password_hash, role, area) VALUES (?, ?, ?, ?, ?, ?, ?)"
  );
  stmtUser.run(userId, orgId, adminName, email, passwordHash, "Administrador Empresa", "Gerencia General");
  stmtUser.finalize();

  console.log("Database seeded successfully!");
  console.log("Registered organization: " + companyName + " (RUC: " + ruc + ")");
  console.log("Registered admin user: " + email + " / password: " + password);
  db.close();
});

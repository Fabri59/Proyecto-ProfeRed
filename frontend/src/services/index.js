import api from './api'

// ============ AUTENTICACIÓN ============
export const authService = {
  login: (email, password) => api.post('/auth/login', { email, password }),
  register: (nombre, email, nombreUsuario, password) =>
    api.post('/auth/register', { nombre, email, nombreUsuario, password }),
  getCurrentUser: () => {
    const usuario = localStorage.getItem('usuario')
    return usuario ? JSON.parse(usuario) : null
  },
  logout: () => {
    localStorage.removeItem('token')
    localStorage.removeItem('usuario')
  },
}

// ============ EMPRESAS ============
export const empresaService = {
  getAll: () => api.get('/empresas'),
  getById: (id) => api.get(`/empresas/${id}`),
  create: (data) => api.post('/empresas', data),
  update: (id, data) => api.put(`/empresas/${id}`, data),
  delete: (id) => api.delete(`/empresas/${id}`),
  getCount: () => api.get('/empresas/count'),
}

// ============ MISIÓN ============
export const misionService = {
  getByEmpresa: (empresaId) => api.get(`/misiones/empresa/${empresaId}`),
  create: (data) => api.post('/misiones', data),
  update: (id, data) => api.put(`/misiones/${id}`, data),
}

// ============ VISIÓN ============
export const visionService = {
  getByEmpresa: (empresaId) => api.get(`/visiones/empresa/${empresaId}`),
  create: (data) => api.post('/visiones', data),
  update: (id, data) => api.put(`/visiones/${id}`, data),
}

// ============ VALORES ============
export const valorService = {
  getByEmpresa: (empresaId) => api.get(`/valores/empresa/${empresaId}`),
  create: (data) => api.post('/valores', data),
  update: (id, data) => api.put(`/valores/${id}`, data),
  delete: (id) => api.delete(`/valores/${id}`),
}

// ============ OBJETIVOS ============
export const objetivoService = {
  getByEmpresa: (empresaId) => api.get(`/objetivos/empresa/${empresaId}`),
  getById: (id) => api.get(`/objetivos/${id}`),
  create: (data) => api.post('/objetivos', data),
  update: (id, data) => api.put(`/objetivos/${id}`, data),
  delete: (id) => api.delete(`/objetivos/${id}`),
}

// ============ FODA ============
export const fodaService = {
  getByEmpresa: (empresaId) => api.get(`/foda/empresa/${empresaId}`),
  getByTipo: (empresaId, tipo) => api.get(`/foda/empresa/${empresaId}/tipo/${tipo}`),
  create: (data) => api.post('/foda', data),
  update: (id, data) => api.put(`/foda/${id}`, data),
  delete: (id) => api.delete(`/foda/${id}`),
}

// ============ MATRIZ BCG ============
export const bcgService = {
  getByEmpresa: (empresaId) => api.get(`/bcg/empresa/${empresaId}`),
  create: (data) => api.post('/bcg', data),
  update: (id, data) => api.put(`/bcg/${id}`, data),
  delete: (id) => api.delete(`/bcg/${id}`),
}

// ============ PORTER ============
export const porterService = {
  getByEmpresa: (empresaId) => api.get(`/porter/empresa/${empresaId}`),
  create: (data) => api.post('/porter', data),
  update: (id, data) => api.put(`/porter/${id}`, data),
  delete: (id) => api.delete(`/porter/${id}`),
}

// ============ PEST ============
export const pestService = {
  getByEmpresa: (empresaId) => api.get(`/pest/empresa/${empresaId}`),
  create: (data) => api.post('/pest', data),
  update: (id, data) => api.put(`/pest/${id}`, data),
  delete: (id) => api.delete(`/pest/${id}`),
}

// ============ ESTRATEGIAS ============
export const estrategiaService = {
  getByEmpresa: (empresaId) => api.get(`/estrategias/empresa/${empresaId}`),
  create: (data) => api.post('/estrategias', data),
  update: (id, data) => api.put(`/estrategias/${id}`, data),
  delete: (id) => api.delete(`/estrategias/${id}`),
}

// ============ CAME ============
export const cameService = {
  getByEmpresa: (empresaId) => api.get(`/came/empresa/${empresaId}`),
  create: (data) => api.post('/came', data),
  update: (id, data) => api.put(`/came/${id}`, data),
  delete: (id) => api.delete(`/came/${id}`),
}

// ============ CADENA DE VALOR ============
export const cadenaValorService = {
  getByEmpresa: (empresaId) => api.get(`/cadenavalor/empresa/${empresaId}`),
  create: (data) => api.post('/cadenavalor', data),
  update: (id, data) => api.put(`/cadenavalor/${id}`, data),
  delete: (id) => api.delete(`/cadenavalor/${id}`),
}

// ============ AUTO CADENA DE VALOR ============
export const autoCadenaValorService = {
  getByEmpresa: (empresaId) => api.get(`/autocadenavalor/empresa/${empresaId}`),
  create: (data) => api.post('/autocadenavalor', data),
  update: (id, data) => api.put(`/autocadenavalor/${id}`, data),
  delete: (id) => api.delete(`/autocadenavalor/${id}`),
}

// ============ AUTO BCG ============
export const autoBcgService = {
  getByEmpresa: (empresaId) => api.get(`/autobcg/empresa/${empresaId}`),
  create: (data) => api.post('/autobcg', data),
  update: (id, data) => api.put(`/autobcg/${id}`, data),
  delete: (id) => api.delete(`/autobcg/${id}`),
}

// ============ AUTO PORTER ============
export const autoPorterService = {
  getByEmpresa: (empresaId) => api.get(`/autoporter/empresa/${empresaId}`),
  create: (data) => api.post('/autoporter', data),
  update: (id, data) => api.put(`/autoporter/${id}`, data),
  delete: (id) => api.delete(`/autoporter/${id}`),
}

// ============ RESUMEN / REPORTES ============
export const resumenService = {
  getByEmpresa: (empresaId) => api.get(`/resumen/empresa/${empresaId}`),
  generar: (empresaId) => api.post(`/resumen/empresa/${empresaId}/generar`),
}

export const reportesService = {
  exportPdf: (empresaId) => api.get(`/reportes/empresa/${empresaId}/pdf`, { responseType: 'blob' }),
  exportExcel: (empresaId) => api.get(`/reportes/empresa/${empresaId}/excel`, { responseType: 'blob' }),
}

// ============ DASHBOARD ============
export const dashboardService = {
  getGeneral: () => api.get('/dashboard/general'),
}

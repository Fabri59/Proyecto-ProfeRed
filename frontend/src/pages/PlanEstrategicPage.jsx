import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import {
  autoBcgService,
  autoCadenaValorService,
  autoPorterService,
  bcgService,
  cadenaValorService,
  cameService,
  estrategiaService,
  pestService,
  porterService,
  reportesService,
  resumenService,
  misionService,
  visionService,
  valorService,
  objetivoService,
  fodaService,
} from '../services'

const PlanEstrategicPage = () => {
  const { empresaId } = useParams()
  const [activeTab, setActiveTab] = useState('mision')
  const [data, setData] = useState({
    mision: null,
    vision: null,
    valores: [],
    objetivos: [],
    foda: [],
    cadenaValor: [],
    autoCadena: [],
    bcg: [],
    autoBcg: [],
    porter: [],
    autoPorter: [],
    pest: [],
    estrategias: [],
    came: [],
    resumen: null,
  })
  const [loading, setLoading] = useState(true)
  const [form, setForm] = useState({
    texto1: '',
    texto2: '',
    texto3: '',
    numero: '',
  })

  useEffect(() => {
    loadPlanData()
  }, [empresaId])

  const loadPlanData = async () => {
    try {
      setLoading(true)
      const [misionRes, visionRes, valoresRes, objetivosRes, fodaRes, cadenaRes, autoCadenaRes, bcgRes, autoBcgRes, porterRes, autoPorterRes, pestRes, estrategiasRes, cameRes, resumenRes] =
        await Promise.all([
          misionService.getByEmpresa(empresaId),
          visionService.getByEmpresa(empresaId),
          valorService.getByEmpresa(empresaId),
          objetivoService.getByEmpresa(empresaId),
          fodaService.getByEmpresa(empresaId),
          cadenaValorService.getByEmpresa(empresaId),
          autoCadenaValorService.getByEmpresa(empresaId),
          bcgService.getByEmpresa(empresaId),
          autoBcgService.getByEmpresa(empresaId),
          porterService.getByEmpresa(empresaId),
          autoPorterService.getByEmpresa(empresaId),
          pestService.getByEmpresa(empresaId),
          estrategiaService.getByEmpresa(empresaId),
          cameService.getByEmpresa(empresaId),
          resumenService.getByEmpresa(empresaId),
        ])

      setData({
        mision: misionRes.data.datos,
        vision: visionRes.data.datos,
        valores: valoresRes.data.datos || [],
        objetivos: objetivosRes.data.datos || [],
        foda: fodaRes.data.datos || [],
        cadenaValor: cadenaRes.data.datos || [],
        autoCadena: autoCadenaRes.data.datos || [],
        bcg: bcgRes.data.datos || [],
        autoBcg: autoBcgRes.data.datos || [],
        porter: porterRes.data.datos || [],
        autoPorter: autoPorterRes.data.datos || [],
        pest: pestRes.data.datos || [],
        estrategias: estrategiasRes.data.datos || [],
        came: cameRes.data.datos || [],
        resumen: resumenRes.data.datos,
      })
    } catch (error) {
      console.error('Error cargando plan:', error)
    } finally {
      setLoading(false)
    }
  }

  const handleExport = async (tipo) => {
    const response = tipo === 'pdf'
      ? await reportesService.exportPdf(empresaId)
      : await reportesService.exportExcel(empresaId)

    const blob = new Blob([response.data])
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `plan_${empresaId}.${tipo === 'pdf' ? 'pdf' : 'xlsx'}`
    link.click()
    window.URL.revokeObjectURL(url)
  }

  const handleGenerarResumen = async () => {
    await resumenService.generar(empresaId)
    await loadPlanData()
  }

  const clearForm = () => setForm({ texto1: '', texto2: '', texto3: '', numero: '' })

  const handleCreateSimple = async (type) => {
    const idEmpresa = Number(empresaId)
    if (type === 'cadena') {
      await cadenaValorService.create({ idEmpresa, tipoActividad: form.texto1, nombre: form.texto2, descripcion: form.texto3, responsable: 'Sistema' })
    }
    if (type === 'autoCadena') {
      await autoCadenaValorService.create({ idEmpresa, area: form.texto1, puntaje: Number(form.numero || 0), observacion: form.texto3 })
    }
    if (type === 'autoBcg') {
      await autoBcgService.create({ idEmpresa, evaluacion: form.texto1, puntaje: Number(form.numero || 0), comentario: form.texto3 })
    }
    if (type === 'autoPorter') {
      await autoPorterService.create({ idEmpresa, evaluacion: form.texto1, resultado: form.texto2, observacion: form.texto3 })
    }
    clearForm()
    await loadPlanData()
  }

  if (loading) {
    return (
      <div className="text-center p-5">
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Cargando...</span>
        </div>
      </div>
    )
  }

  return (
    <div className="plan-page">
      <h1 className="page-title">Plan Estratégico</h1>

      <div className="d-flex gap-2 mb-3">
        <button className="btn btn-outline-primary btn-sm" onClick={() => handleExport('pdf')}>Exportar PDF</button>
        <button className="btn btn-outline-success btn-sm" onClick={() => handleExport('excel')}>Exportar Excel</button>
        <button className="btn btn-primary btn-sm" onClick={handleGenerarResumen}>Generar Resumen Ejecutivo</button>
      </div>

      <ul className="nav nav-tabs" role="tablist">
        <li className="nav-item">
          <button
            className={`nav-link ${activeTab === 'mision' ? 'active' : ''}`}
            onClick={() => setActiveTab('mision')}
          >
            Misión
          </button>
        </li>
        <li className="nav-item">
          <button
            className={`nav-link ${activeTab === 'vision' ? 'active' : ''}`}
            onClick={() => setActiveTab('vision')}
          >
            Visión
          </button>
        </li>
        <li className="nav-item">
          <button
            className={`nav-link ${activeTab === 'valores' ? 'active' : ''}`}
            onClick={() => setActiveTab('valores')}
          >
            Valores
          </button>
        </li>
        <li className="nav-item">
          <button
            className={`nav-link ${activeTab === 'objetivos' ? 'active' : ''}`}
            onClick={() => setActiveTab('objetivos')}
          >
            Objetivos
          </button>
        </li>
        <li className="nav-item">
          <button
            className={`nav-link ${activeTab === 'foda' ? 'active' : ''}`}
            onClick={() => setActiveTab('foda')}
          >
            FODA
          </button>
        </li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'cadena' ? 'active' : ''}`} onClick={() => setActiveTab('cadena')}>Cadena Valor</button></li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'autoCadena' ? 'active' : ''}`} onClick={() => setActiveTab('autoCadena')}>Auto Cadena</button></li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'bcg' ? 'active' : ''}`} onClick={() => setActiveTab('bcg')}>BCG</button></li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'autoBcg' ? 'active' : ''}`} onClick={() => setActiveTab('autoBcg')}>Auto BCG</button></li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'porter' ? 'active' : ''}`} onClick={() => setActiveTab('porter')}>Porter</button></li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'autoPorter' ? 'active' : ''}`} onClick={() => setActiveTab('autoPorter')}>Auto Porter</button></li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'pest' ? 'active' : ''}`} onClick={() => setActiveTab('pest')}>PEST</button></li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'estrategias' ? 'active' : ''}`} onClick={() => setActiveTab('estrategias')}>Estrategias</button></li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'came' ? 'active' : ''}`} onClick={() => setActiveTab('came')}>CAME</button></li>
        <li className="nav-item"><button className={`nav-link ${activeTab === 'resumen' ? 'active' : ''}`} onClick={() => setActiveTab('resumen')}>Resumen</button></li>
      </ul>

      <div className="tab-content mt-4">
        {activeTab === 'mision' && (
          <div className="card">
            <div className="card-body">
              <h5>Misión Empresarial</h5>
              {data.mision ? (
                <p>{data.mision.descripcion}</p>
              ) : (
                <p className="text-muted">No hay misión registrada</p>
              )}
            </div>
          </div>
        )}

        {activeTab === 'vision' && (
          <div className="card">
            <div className="card-body">
              <h5>Visión Empresarial</h5>
              {data.vision ? (
                <p>{data.vision.descripcion}</p>
              ) : (
                <p className="text-muted">No hay visión registrada</p>
              )}
            </div>
          </div>
        )}

        {activeTab === 'valores' && (
          <div className="card">
            <div className="card-body">
              <h5>Valores Corporativos</h5>
              {data.valores.length > 0 ? (
                <div className="row">
                  {data.valores.map((valor) => (
                    <div key={valor.idValor} className="col-md-6 mb-3">
                      <div className="card card-custom">
                        <div className="card-body">
                          <h6>{valor.nombre}</h6>
                          <p className="text-muted">{valor.descripcion}</p>
                        </div>
                      </div>
                    </div>
                  ))}
                </div>
              ) : (
                <p className="text-muted">No hay valores registrados</p>
              )}
            </div>
          </div>
        )}

        {activeTab === 'objetivos' && (
          <div className="card">
            <div className="card-body">
              <h5>Objetivos Estratégicos</h5>
              {data.objetivos.length > 0 ? (
                <div className="table-responsive">
                  <table className="table">
                    <thead>
                      <tr>
                        <th>Objetivo</th>
                        <th>UEN</th>
                        <th>Meta</th>
                        <th>Responsable</th>
                      </tr>
                    </thead>
                    <tbody>
                      {data.objetivos.map((obj) => (
                        <tr key={obj.idObjetivo}>
                          <td>{obj.objetivo}</td>
                          <td>{obj.uen}</td>
                          <td>{obj.meta}</td>
                          <td>{obj.responsable}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              ) : (
                <p className="text-muted">No hay objetivos registrados</p>
              )}
            </div>
          </div>
        )}

        {activeTab === 'foda' && (
          <div>
            {['Fortaleza', 'Debilidad', 'Oportunidad', 'Amenaza'].map((tipo) => {
              const items = data.foda.filter((f) => f.tipo === tipo)
              return (
                <div key={tipo} className="card mb-3">
                  <div className="card-header">
                    <h6 className="mb-0">{tipo}s ({items.length})</h6>
                  </div>
                  <div className="card-body">
                    {items.map((item) => (
                      <div key={item.idAnalisis} className="mb-2">
                        <p>{item.descripcion}</p>
                        <small className="text-muted">
                          Ponderación: {item.ponderacion}
                        </small>
                      </div>
                    ))}
                  </div>
                </div>
              )
            })}
          </div>
        )}

        {activeTab === 'cadena' && (
          <div className="card"><div className="card-body">
            <h5>Cadena de Valor</h5>
            <div className="row g-2 mb-3">
              <div className="col-md-3"><input className="form-control" placeholder="Tipo actividad" value={form.texto1} onChange={(e) => setForm({ ...form, texto1: e.target.value })} /></div>
              <div className="col-md-3"><input className="form-control" placeholder="Nombre" value={form.texto2} onChange={(e) => setForm({ ...form, texto2: e.target.value })} /></div>
              <div className="col-md-4"><input className="form-control" placeholder="Descripción" value={form.texto3} onChange={(e) => setForm({ ...form, texto3: e.target.value })} /></div>
              <div className="col-md-2"><button className="btn btn-primary w-100" onClick={() => handleCreateSimple('cadena')}>Agregar</button></div>
            </div>
            <ul>{data.cadenaValor.map((x) => <li key={x.idCadenaValor}>{x.tipoActividad} - {x.nombre}</li>)}</ul>
          </div></div>
        )}

        {activeTab === 'autoCadena' && (
          <div className="card"><div className="card-body">
            <h5>Autodiagnóstico Cadena</h5>
            <div className="row g-2 mb-3">
              <div className="col-md-4"><input className="form-control" placeholder="Área" value={form.texto1} onChange={(e) => setForm({ ...form, texto1: e.target.value })} /></div>
              <div className="col-md-2"><input type="number" className="form-control" placeholder="Puntaje" value={form.numero} onChange={(e) => setForm({ ...form, numero: e.target.value })} /></div>
              <div className="col-md-4"><input className="form-control" placeholder="Observación" value={form.texto3} onChange={(e) => setForm({ ...form, texto3: e.target.value })} /></div>
              <div className="col-md-2"><button className="btn btn-primary w-100" onClick={() => handleCreateSimple('autoCadena')}>Agregar</button></div>
            </div>
            <ul>{data.autoCadena.map((x) => <li key={x.idAutoCadena}>{x.area} - {x.puntaje}</li>)}</ul>
          </div></div>
        )}

        {activeTab === 'bcg' && (
          <div className="card"><div className="card-body"><h5>BCG</h5><ul>{data.bcg.map((x) => <li key={x.idBCG}>{x.producto} - {x.clasificacion}</li>)}</ul></div></div>
        )}

        {activeTab === 'autoBcg' && (
          <div className="card"><div className="card-body">
            <h5>Autodiagnóstico BCG</h5>
            <div className="row g-2 mb-3">
              <div className="col-md-4"><input className="form-control" placeholder="Evaluación" value={form.texto1} onChange={(e) => setForm({ ...form, texto1: e.target.value })} /></div>
              <div className="col-md-2"><input type="number" className="form-control" placeholder="Puntaje" value={form.numero} onChange={(e) => setForm({ ...form, numero: e.target.value })} /></div>
              <div className="col-md-4"><input className="form-control" placeholder="Comentario" value={form.texto3} onChange={(e) => setForm({ ...form, texto3: e.target.value })} /></div>
              <div className="col-md-2"><button className="btn btn-primary w-100" onClick={() => handleCreateSimple('autoBcg')}>Agregar</button></div>
            </div>
            <ul>{data.autoBcg.map((x) => <li key={x.idAutoBCG}>{x.evaluacion} - {x.puntaje}</li>)}</ul>
          </div></div>
        )}

        {activeTab === 'porter' && (
          <div className="card"><div className="card-body"><h5>Porter</h5><ul>{data.porter.map((x) => <li key={x.idPorter}>{x.fuerza} - {x.puntaje}</li>)}</ul></div></div>
        )}

        {activeTab === 'autoPorter' && (
          <div className="card"><div className="card-body">
            <h5>Autodiagnóstico Porter</h5>
            <div className="row g-2 mb-3">
              <div className="col-md-4"><input className="form-control" placeholder="Evaluación" value={form.texto1} onChange={(e) => setForm({ ...form, texto1: e.target.value })} /></div>
              <div className="col-md-3"><input className="form-control" placeholder="Resultado" value={form.texto2} onChange={(e) => setForm({ ...form, texto2: e.target.value })} /></div>
              <div className="col-md-3"><input className="form-control" placeholder="Observación" value={form.texto3} onChange={(e) => setForm({ ...form, texto3: e.target.value })} /></div>
              <div className="col-md-2"><button className="btn btn-primary w-100" onClick={() => handleCreateSimple('autoPorter')}>Agregar</button></div>
            </div>
            <ul>{data.autoPorter.map((x) => <li key={x.idAutoPorter}>{x.evaluacion} - {x.resultado}</li>)}</ul>
          </div></div>
        )}

        {activeTab === 'pest' && (
          <div className="card"><div className="card-body"><h5>PEST</h5><ul>{data.pest.map((x) => <li key={x.idPEST}>{x.tipo} - {x.impacto}</li>)}</ul></div></div>
        )}

        {activeTab === 'estrategias' && (
          <div className="card"><div className="card-body"><h5>Estrategias</h5><ul>{data.estrategias.map((x) => <li key={x.idEstrategia}>{x.nombre} - {x.tipo}</li>)}</ul></div></div>
        )}

        {activeTab === 'came' && (
          <div className="card"><div className="card-body"><h5>CAME</h5><ul>{data.came.map((x) => <li key={x.idCAME}>{x.tipo} - {x.estrategia}</li>)}</ul></div></div>
        )}

        {activeTab === 'resumen' && (
          <div className="card"><div className="card-body"><h5>Resumen Ejecutivo</h5><pre className="mb-0">{data.resumen?.contenido || 'Aún no generado'}</pre></div></div>
        )}
      </div>
    </div>
  )
}

export default PlanEstrategicPage

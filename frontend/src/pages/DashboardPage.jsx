import React, { useEffect, useState } from 'react'
import ReactApexChart from 'react-apexcharts'
import { dashboardService, empresaService } from '../services'
import './DashboardPage.css'

const DashboardPage = () => {
  const [stats, setStats] = useState({
    totalEmpresas: 0,
    totalObjetivos: 0,
    totalEstrategias: 0,
    avancePromedio: 0,
  })
  const [charts, setCharts] = useState({ foda: {}, bcg: {} })
  const [empresas, setEmpresas] = useState([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    loadDashboardData()
  }, [])

  const loadDashboardData = async () => {
    try {
      setLoading(true)
      const [empresasRes, dashboardRes] = await Promise.all([
        empresaService.getAll(),
        dashboardService.getGeneral(),
      ])

      const dashboard = dashboardRes.data.datos || {}

      setEmpresas(empresasRes.data.datos || [])
      setStats({
        totalEmpresas: dashboard.totalEmpresas || empresasRes.data.datos?.length || 0,
        totalObjetivos: dashboard.totalObjetivos || 0,
        totalEstrategias: dashboard.totalEstrategias || 0,
        avancePromedio: Number(dashboard.avancePromedio || 0).toFixed(1),
      })

      setCharts({
        foda: dashboard.conteoFODA || {},
        bcg: dashboard.clasificacionBCG || {},
      })
    } catch (error) {
      console.error('Error cargando dashboard:', error)
    } finally {
      setLoading(false)
    }
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
    <div className="dashboard">
      <h1 className="page-title">Dashboard</h1>

      {/* Cards de Estadísticas */}
      <div className="row mb-4">
        <div className="col-md-6 col-lg-3">
          <div className="stat-card">
            <div className="stat-icon" style={{ backgroundColor: '#a8edea' }}>
              <i className="fas fa-building"></i>
            </div>
            <div className="stat-content">
              <h5>Empresas</h5>
              <p className="stat-number">{stats.totalEmpresas}</p>
            </div>
          </div>
        </div>

        <div className="col-md-6 col-lg-3">
          <div className="stat-card">
            <div className="stat-icon" style={{ backgroundColor: '#fed6e3' }}>
              <i className="fas fa-target"></i>
            </div>
            <div className="stat-content">
              <h5>Objetivos</h5>
              <p className="stat-number">{stats.totalObjetivos}</p>
            </div>
          </div>
        </div>

        <div className="col-md-6 col-lg-3">
          <div className="stat-card">
            <div className="stat-icon" style={{ backgroundColor: '#a29bfe' }}>
              <i className="fas fa-chart-line"></i>
            </div>
            <div className="stat-content">
              <h5>Estrategias</h5>
              <p className="stat-number">{stats.totalEstrategias}</p>
            </div>
          </div>
        </div>

        <div className="col-md-6 col-lg-3">
          <div className="stat-card">
            <div className="stat-icon" style={{ backgroundColor: '#74b9ff' }}>
              <i className="fas fa-check-circle"></i>
            </div>
            <div className="stat-content">
              <h5>Completado</h5>
              <p className="stat-number">{stats.avancePromedio}%</p>
            </div>
          </div>
        </div>
      </div>

      <div className="row mb-4">
        <div className="col-lg-6 mb-3">
          <div className="card card-custom">
            <div className="card-header bg-primary text-white">
              <h5 className="mb-0">Distribución FODA</h5>
            </div>
            <div className="card-body">
              <ReactApexChart
                type="donut"
                height={280}
                series={Object.values(charts.foda).length ? Object.values(charts.foda) : [1]}
                options={{
                  labels: Object.keys(charts.foda).length ? Object.keys(charts.foda) : ['Sin datos'],
                  legend: { position: 'bottom' },
                }}
              />
            </div>
          </div>
        </div>

        <div className="col-lg-6 mb-3">
          <div className="card card-custom">
            <div className="card-header bg-primary text-white">
              <h5 className="mb-0">Clasificación BCG</h5>
            </div>
            <div className="card-body">
              <ReactApexChart
                type="bar"
                height={280}
                series={[{ name: 'Total', data: Object.values(charts.bcg).length ? Object.values(charts.bcg) : [0] }]}
                options={{
                  xaxis: { categories: Object.keys(charts.bcg).length ? Object.keys(charts.bcg) : ['Sin datos'] },
                }}
              />
            </div>
          </div>
        </div>
      </div>

      {/* Tabla de Empresas */}
      <div className="card card-custom">
        <div className="card-header bg-primary text-white">
          <h5 className="mb-0">Empresas Registradas</h5>
        </div>
        <div className="card-body">
          {empresas.length === 0 ? (
            <div className="text-center py-5">
              <p className="text-muted">No hay empresas registradas</p>
            </div>
          ) : (
            <div className="table-responsive">
              <table className="table table-hover">
                <thead>
                  <tr>
                    <th>Nombre</th>
                    <th>RUC</th>
                    <th>Correo</th>
                    <th>Rubro</th>
                    <th>Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  {empresas.map((empresa) => (
                    <tr key={empresa.idEmpresa}>
                      <td>{empresa.nombre}</td>
                      <td>{empresa.ruc}</td>
                      <td>{empresa.correo}</td>
                      <td>{empresa.rubro}</td>
                      <td>
                        <button className="btn btn-sm btn-info">
                          <i className="fas fa-eye"></i>
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
    </div>
  )
}

export default DashboardPage

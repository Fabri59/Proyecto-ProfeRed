import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { empresaService } from '../services'
import './EmpresasPage.css'

const EmpresasPage = () => {
  const [empresas, setEmpresas] = useState([])
  const [showForm, setShowForm] = useState(false)
  const [loading, setLoading] = useState(true)
  const [formData, setFormData] = useState({
    nombre: '',
    ruc: '',
    direccion: '',
    telefono: '',
    correo: '',
    rubro: '',
    descripcion: '',
    responsable: '',
  })
  const navigate = useNavigate()

  useEffect(() => {
    loadEmpresas()
  }, [])

  const loadEmpresas = async () => {
    try {
      setLoading(true)
      const response = await empresaService.getAll()
      setEmpresas(response.data.datos || [])
    } catch (error) {
      console.error('Error cargando empresas:', error)
    } finally {
      setLoading(false)
    }
  }

  const handleInputChange = (e) => {
    const { name, value } = e.target
    setFormData({ ...formData, [name]: value })
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      await empresaService.create(formData)
      setFormData({
        nombre: '',
        ruc: '',
        direccion: '',
        telefono: '',
        correo: '',
        rubro: '',
        descripcion: '',
        responsable: '',
      })
      setShowForm(false)
      loadEmpresas()
    } catch (error) {
      console.error('Error creando empresa:', error)
    }
  }

  const handleVerPlan = (empresaId) => {
    navigate(`/plan/${empresaId}`)
  }

  return (
    <div className="empresas-page">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h1 className="page-title">Empresas</h1>
        <button
          className="btn btn-primary"
          onClick={() => setShowForm(!showForm)}
        >
          <i className="fas fa-plus"></i> Nueva Empresa
        </button>
      </div>

      {showForm && (
        <div className="card card-custom mb-4">
          <div className="card-header bg-primary text-white">
            <h5 className="mb-0">Crear Nueva Empresa</h5>
          </div>
          <div className="card-body">
            <form onSubmit={handleSubmit}>
              <div className="row">
                <div className="col-md-6 mb-3">
                  <label className="form-label">Nombre</label>
                  <input
                    type="text"
                    className="form-control"
                    name="nombre"
                    value={formData.nombre}
                    onChange={handleInputChange}
                    required
                  />
                </div>
                <div className="col-md-6 mb-3">
                  <label className="form-label">RUC</label>
                  <input
                    type="text"
                    className="form-control"
                    name="ruc"
                    value={formData.ruc}
                    onChange={handleInputChange}
                    required
                  />
                </div>
              </div>

              <div className="row">
                <div className="col-md-6 mb-3">
                  <label className="form-label">Correo</label>
                  <input
                    type="email"
                    className="form-control"
                    name="correo"
                    value={formData.correo}
                    onChange={handleInputChange}
                    required
                  />
                </div>
                <div className="col-md-6 mb-3">
                  <label className="form-label">Teléfono</label>
                  <input
                    type="tel"
                    className="form-control"
                    name="telefono"
                    value={formData.telefono}
                    onChange={handleInputChange}
                  />
                </div>
              </div>

              <div className="mb-3">
                <label className="form-label">Dirección</label>
                <input
                  type="text"
                  className="form-control"
                  name="direccion"
                  value={formData.direccion}
                  onChange={handleInputChange}
                />
              </div>

              <div className="row">
                <div className="col-md-6 mb-3">
                  <label className="form-label">Rubro</label>
                  <input
                    type="text"
                    className="form-control"
                    name="rubro"
                    value={formData.rubro}
                    onChange={handleInputChange}
                  />
                </div>
                <div className="col-md-6 mb-3">
                  <label className="form-label">Responsable</label>
                  <input
                    type="text"
                    className="form-control"
                    name="responsable"
                    value={formData.responsable}
                    onChange={handleInputChange}
                  />
                </div>
              </div>

              <div className="mb-3">
                <label className="form-label">Descripción</label>
                <textarea
                  className="form-control"
                  name="descripcion"
                  value={formData.descripcion}
                  onChange={handleInputChange}
                  rows="3"
                ></textarea>
              </div>

              <button type="submit" className="btn btn-primary">
                <i className="fas fa-save"></i> Guardar
              </button>
              <button
                type="button"
                className="btn btn-secondary ms-2"
                onClick={() => setShowForm(false)}
              >
                Cancelar
              </button>
            </form>
          </div>
        </div>
      )}

      {loading ? (
        <div className="text-center p-5">
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Cargando...</span>
          </div>
        </div>
      ) : (
        <div className="row">
          {empresas.map((empresa) => (
            <div key={empresa.idEmpresa} className="col-md-6 col-lg-4 mb-4">
              <div className="card empresa-card">
                <div className="card-body">
                  <h5 className="card-title">{empresa.nombre}</h5>
                  <p className="card-text">
                    <strong>RUC:</strong> {empresa.ruc}
                  </p>
                  <p className="card-text">
                    <strong>Rubro:</strong> {empresa.rubro}
                  </p>
                  <p className="card-text">
                    <strong>Correo:</strong> {empresa.correo}
                  </p>
                  <p className="card-text text-muted">{empresa.descripcion}</p>
                </div>
                <div className="card-footer bg-light">
                  <button
                    className="btn btn-sm btn-primary w-100"
                    onClick={() => handleVerPlan(empresa.idEmpresa)}
                  >
                    <i className="fas fa-arrow-right"></i> Ver Plan
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      {!loading && empresas.length === 0 && (
        <div className="text-center py-5">
          <p className="text-muted">No hay empresas registradas</p>
        </div>
      )}
    </div>
  )
}

export default EmpresasPage

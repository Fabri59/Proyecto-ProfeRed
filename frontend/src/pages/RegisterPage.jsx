import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { authService } from '../services'
import './LoginPage.css'

const RegisterPage = () => {
  const [form, setForm] = useState({
    nombre: '',
    email: '',
    nombreUsuario: '',
    password: '',
    confirmPassword: '',
  })
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')
  const [success, setSuccess] = useState('')
  const navigate = useNavigate()

  const handleChange = (e) => {
    const { name, value } = e.target
    setForm((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')
    setSuccess('')

    if (form.password !== form.confirmPassword) {
      setError('Las contraseñas no coinciden')
      return
    }

    try {
      setLoading(true)
      await authService.register(form.nombre, form.email, form.nombreUsuario, form.password)
      setSuccess('Usuario registrado correctamente. Ya puedes iniciar sesión.')
      setTimeout(() => navigate('/login'), 1200)
    } catch (err) {
      setError(
        err.response?.data?.mensaje ||
          err.response?.data?.Mensaje ||
          'No se pudo registrar el usuario'
      )
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="login-container">
      <div className="login-card">
        <div className="login-header">
          <h1>
            <i className="fas fa-user-plus"></i> Crear cuenta
          </h1>
          <p>Registro de usuario para el sistema</p>
        </div>

        <form onSubmit={handleSubmit}>
          {error && (
            <div className="alert alert-danger" role="alert">
              {error}
            </div>
          )}
          {success && (
            <div className="alert alert-success" role="alert">
              {success}
            </div>
          )}

          <div className="mb-3">
            <label htmlFor="nombre" className="form-label">Nombre completo</label>
            <input id="nombre" name="nombre" className="form-control" value={form.nombre} onChange={handleChange} required />
          </div>

          <div className="mb-3">
            <label htmlFor="email" className="form-label">Correo electrónico</label>
            <input type="email" id="email" name="email" className="form-control" value={form.email} onChange={handleChange} required />
          </div>

          <div className="mb-3">
            <label htmlFor="nombreUsuario" className="form-label">Nombre de usuario</label>
            <input id="nombreUsuario" name="nombreUsuario" className="form-control" value={form.nombreUsuario} onChange={handleChange} required />
          </div>

          <div className="mb-3">
            <label htmlFor="password" className="form-label">Contraseña</label>
            <input type="password" id="password" name="password" className="form-control" value={form.password} onChange={handleChange} required />
          </div>

          <div className="mb-3">
            <label htmlFor="confirmPassword" className="form-label">Confirmar contraseña</label>
            <input type="password" id="confirmPassword" name="confirmPassword" className="form-control" value={form.confirmPassword} onChange={handleChange} required />
          </div>

          <button type="submit" className="btn btn-primary w-100" disabled={loading}>
            {loading ? 'Registrando...' : 'Registrarme'}
          </button>
        </form>

        <div className="login-footer">
          <span>¿Ya tienes cuenta? </span>
          <Link to="/login">Inicia sesión</Link>
        </div>
      </div>
    </div>
  )
}

export default RegisterPage

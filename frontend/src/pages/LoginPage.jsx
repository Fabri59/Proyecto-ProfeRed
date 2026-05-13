import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { authService } from '../services'
import { useAuth } from '../context/AuthContext'
import './LoginPage.css'

const LoginPage = () => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')
  const navigate = useNavigate()
  const { login } = useAuth()

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')
    setLoading(true)

    try {
      const response = await authService.login(email, password)
      const { datos } = response.data

      // Guardar datos de autenticación
      login(
        {
          idUsuario: datos.idUsuario,
          nombre: datos.nombre,
          email: datos.email,
          rolNombre: datos.rolNombre,
        },
        datos.token
      )

      navigate('/dashboard')
    } catch (err) {
      setError(
        err.response?.data?.mensaje ||
          err.response?.data?.Mensaje ||
          'Error de autenticación'
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
            <i className="fas fa-chart-line"></i> Plan Estratégico
          </h1>
          <p>Sistema de Elaboración de Plan Estratégico de TI</p>
        </div>

        <form onSubmit={handleSubmit}>
          {error && (
            <div className="alert alert-danger" role="alert">
              {error}
            </div>
          )}

          <div className="mb-3">
            <label htmlFor="email" className="form-label">
              Correo Electrónico
            </label>
            <input
              type="email"
              className="form-control"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>

          <div className="mb-3">
            <label htmlFor="password" className="form-label">
              Contraseña
            </label>
            <input
              type="password"
              className="form-control"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>

          <button
            type="submit"
            className="btn btn-primary w-100"
            disabled={loading}
          >
            {loading ? (
              <>
                <span className="spinner-border spinner-border-sm me-2"></span>
                Cargando...
              </>
            ) : (
              'Iniciar Sesión'
            )}
          </button>
        </form>

        <div className="login-footer">
          <p>Usuarios de prueba:</p>
          <small>Admin: admin@empresa.com / 123456</small>
          <br />
          <small>Analista: analista@empresa.com / 123456</small>
          <div className="mt-3">
            <span>¿No tienes cuenta? </span>
            <Link to="/register">Regístrate aquí</Link>
          </div>
        </div>
      </div>
    </div>
  )
}

export default LoginPage

import React from 'react'
import { Outlet, Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'
import './Layout.css'

const Layout = () => {
  const { usuario, logout } = useAuth()
  const navigate = useNavigate()

  const handleLogout = () => {
    logout()
    navigate('/login')
  }

  return (
    <div className="layout">
      {/* Sidebar */}
      <nav className="sidebar">
        <div className="sidebar-header">
          <h4 className="mb-3">
            <i className="fas fa-chart-line"></i> Plan Estratégico
          </h4>
          <hr />
        </div>

        <Link to="/dashboard" className="sidebar-link">
          <i className="fas fa-home"></i> Dashboard
        </Link>
        <Link to="/empresas" className="sidebar-link">
          <i className="fas fa-building"></i> Empresas
        </Link>

        <div className="sidebar-separator mt-3"></div>

        <div className="sidebar-section">
          <p className="sidebar-section-title">Herramientas</p>
          <Link to="#" className="sidebar-link">
            <i className="fas fa-file-export"></i> Exportar
          </Link>
          <Link to="#" className="sidebar-link">
            <i className="fas fa-file-import"></i> Importar
          </Link>
        </div>
      </nav>

      {/* Main Content */}
      <div className="main-container">
        {/* TopBar */}
        <nav className="navbar navbar-expand-lg navbar-dark topbar">
          <div className="container-fluid">
            <span className="navbar-brand">Sistema de Plan Estratégico de TI</span>
            
            <div className="ms-auto d-flex align-items-center">
              {usuario && (
                <>
                  <span className="me-3">
                    <i className="fas fa-user-circle"></i> {usuario.nombre}
                  </span>
                  <button 
                    className="btn btn-sm btn-outline-light"
                    onClick={handleLogout}
                  >
                    <i className="fas fa-sign-out-alt"></i> Salir
                  </button>
                </>
              )}
            </div>
          </div>
        </nav>

        {/* Page Content */}
        <div className="page-content">
          <Outlet />
        </div>

        {/* Footer */}
        <footer className="footer">
          <p>&copy; 2026 Sistema de Plan Estratégico de TI. Todos los derechos reservados.</p>
        </footer>
      </div>
    </div>
  )
}

export default Layout

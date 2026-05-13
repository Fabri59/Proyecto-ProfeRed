import React, { createContext, useContext, useState } from 'react'

const AuthContext = createContext()

export const AuthProvider = ({ children }) => {
  const [usuario, setUsuario] = useState(
    () => {
      const stored = localStorage.getItem('usuario')
      return stored ? JSON.parse(stored) : null
    }
  )

  const login = (usuarioData, token) => {
    setUsuario(usuarioData)
    localStorage.setItem('usuario', JSON.stringify(usuarioData))
    localStorage.setItem('token', token)
  }

  const logout = () => {
    setUsuario(null)
    localStorage.removeItem('usuario')
    localStorage.removeItem('token')
  }

  return (
    <AuthContext.Provider value={{ usuario, login, logout }}>
      {children}
    </AuthContext.Provider>
  )
}

export const useAuth = () => {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error('useAuth debe ser usado dentro de AuthProvider')
  }
  return context
}

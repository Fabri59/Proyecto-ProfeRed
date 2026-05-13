# 🚀 Guía de Inicio Rápido

## Instalación Rápida (5 minutos)

### 1️⃣ Base de Datos

```powershell
# En SQL Server Management Studio:
# 1. Abrir: database/01_create_database.sql
# 2. Ejecutar (F5)
# 3. Abrir: database/02_stored_procedures.sql
# 4. Ejecutar (F5)
```

### 2️⃣ Backend

```powershell
cd backend/PlanEstrategico.API
dotnet restore
dotnet run
# Acceder a: http://localhost:5000/swagger/index.html
```

### 3️⃣ Frontend

```powershell
cd frontend
npm install
npm run dev
# Acceder a: http://localhost:5173
```

## ✅ Verificar Instalación

- [ ] SQL Server ejecutándose
- [ ] Base de datos "PlanEstrategicopTI" creada
- [ ] Backend respondiendo en http://localhost:5000
- [ ] Frontend cargando en http://localhost:5173
- [ ] Swagger API disponible

## 🔐 Primer Usuario

Para crear el primer usuario administrador, ejecutar en SQL Server:

```sql
-- Insertar rol Admin si no existe
IF NOT EXISTS(SELECT * FROM Roles WHERE Nombre = 'Administrador')
INSERT INTO Roles (Nombre, Descripcion) VALUES ('Administrador', 'Acceso total al sistema')

-- Crear usuario admin
INSERT INTO Usuarios (Nombre, Email, NombreUsuario, PasswordHash, IdRol, Activo)
VALUES (
    'Administrador',
    'admin@empresa.com',
    'admin',
    -- Hash de "123456"
    '$2a$11$eKlKzpfL7bfqWF2wPKFKVuOYwQlEWX9c8HJvEzNIKxEz9Wc2kVbOG',
    1,
    1
)
```

## 📝 Variables de Entorno

### Backend (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=PlanEstrategicopTI;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  },
  "JwtSettings": {
    "SecretKey": "tu_clave_secreta_muy_larga_cambiar_en_produccion",
    "ExpirationMinutes": 1440
  }
}
```

### Frontend (.env)
```
VITE_API_URL=http://localhost:5000/api
```

## 🧪 Testear API

### Con Swagger
1. Ir a: http://localhost:5000/swagger
2. Click en "Authorize"
3. Usar token JWT de login

### Con cURL
```bash
# Login
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@empresa.com","password":"123456"}'

# Usar el token retornado
curl -X GET http://localhost:5000/api/empresas \
  -H "Authorization: Bearer <TOKEN>"
```

## 📊 Estructura de Tablas Importante

```sql
-- Tabla de Empresas (requiere al menos una)
INSERT INTO Empresa (Nombre, RUC, Direccion, Telefono, Correo, Rubro)
VALUES ('Empresa Prueba', '20123456789', 'Calle 123', '555-1234', 'empresa@test.com', 'Tecnología')

-- Esto genera un IdEmpresa que se usa en los otros módulos
```

## 🎯 Próximos Pasos

1. **Crear empresa** desde el frontend
2. **Agregar misión y visión**
3. **Registrar análisis FODA**
4. **Crear objetivos estratégicos**
5. **Exportar reporte completo**

## ❓ Preguntas Frecuentes

**¿Puerto 5000 en uso?**
```powershell
# Cambiar en Program.cs:
app.Run("http://localhost:5001");
```

**¿No conecta a BD?**
```powershell
# Verificar:
sqlcmd -S localhost\SQLEXPRESS
# Si funciona, revisar connection string
```

**¿Token no funciona?**
```javascript
// Limpiar localStorage
localStorage.clear()
// Relogin
window.location.href = '/login'
```

## 📞 Contacto

Para más ayuda, consultar documentación en `/docs/`

---

**¡Sistema listo para usar! 🎉**

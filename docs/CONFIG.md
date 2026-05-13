# Configuración de Entorno

## Variables de Entorno

### Backend - appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=PlanEstrategicopTI;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  },
  "JwtSettings": {
    "SecretKey": "tu_clave_secreta_muy_larga_y_segura_cambiar_en_produccion_123456789",
    "Issuer": "PlanEstrategico",
    "Audience": "PlanEstrategico",
    "ExpirationMinutes": 1440
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:5173",
      "http://localhost:3000",
      "http://127.0.0.1:5173"
    ]
  }
}
```

### Backend - appsettings.Development.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Debug"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=PlanEstrategicopTI;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

### Backend - appsettings.Production.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=PRODUCTION_SERVER;Database=PlanEstrategicopTI;User Id=USUARIO;Password=CONTRASEÑA;Encrypt=true;TrustServerCertificate=false;"
  },
  "JwtSettings": {
    "SecretKey": "GENERAR_CLAVE_SEGURA_LARGA_MINIMO_64_CARACTERES_AQUI",
    "Issuer": "PlanEstrategico",
    "Audience": "PlanEstrategico",
    "ExpirationMinutes": 480
  },
  "Cors": {
    "AllowedOrigins": [
      "https://tudominio.com",
      "https://www.tudominio.com"
    ]
  }
}
```

### Frontend - .env

```
VITE_API_URL=http://localhost:5000/api
VITE_APP_NAME=Plan Estratégico
VITE_APP_VERSION=1.0.0
```

### Frontend - .env.production

```
VITE_API_URL=https://api.tudominio.com/api
VITE_APP_NAME=Plan Estratégico - Producción
VITE_APP_VERSION=1.0.0
```

## Puerto Configuración

### Backend
- **Desarrollo**: http://localhost:5000
- **Producción**: https://api.tudominio.com/

Cambiar en `Program.cs`:
```csharp
app.Run("http://localhost:5000");
```

### Frontend
- **Desarrollo**: http://localhost:5173
- **Producción**: https://tudominio.com/

Cambiar en `vite.config.js`:
```javascript
server: {
  port: 5173,
  // ...
}
```

## Base de Datos

### Desarrollo
```
Server: localhost\SQLEXPRESS
Database: PlanEstrategicopTI
Authentication: Windows (Trusted Connection)
```

### Producción
```
Server: PRODUCTION_SQL_SERVER
Database: PlanEstrategicopTI
Authentication: SQL Server Authentication
User: planestrategi_user
Password: CONTRASEÑA_FUERTE_AQUI
```

## Ejemplos de Ejecución

### Ejecutar Backend

```bash
# Desarrollo
cd backend/PlanEstrategico.API
dotnet run

# Producción
dotnet publish -c Release
dotnet PlanEstrategico.API.dll --environment Production
```

### Ejecutar Frontend

```bash
# Desarrollo
cd frontend
npm run dev

# Producción
npm run build
# Servir archivos de dist/ con un servidor web
```

---

**Recuerda:** Nunca commitear archivos `.env` con credenciales reales en Git.

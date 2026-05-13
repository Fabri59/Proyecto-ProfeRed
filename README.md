# Sistema de Elaboración de Plan Estratégico de TI

Sistema web completo para la gestión integral de planes estratégicos empresariales, reemplazando completamente el uso de Excel. La solución incluye módulos para análisis FODA, matrices BCG, análisis Porter, PEST, CAME y más.

## 📋 Características Principales

 - **Autenticación JWT** con 3 roles (Administrador, Analista, Usuario)
 - **17 módulos estratégicos** funcionales
 - **Base de datos relacional** con SQL Server
 - **API RESTful** completamente funcional
 - **Dashboard** con estadísticas y gráficos
 - **Exportación** a PDF y Excel
 - **Interfaz responsiva** moderna
 - **Auditoría y logs** de cambios
 - **Validaciones** backend y frontend

## 🏗️ Arquitectura

```
Proyecto/
├── backend/                    # ASP.NET Core Web API
│   └── PlanEstrategico.API/
│       ├── Controllers/        # Web API Controllers
│       ├── Services/           # Business Logic
│       ├── Repositories/       # Data Access
│       ├── Models/             # Domain Models
│       ├── DTOs/               # Data Transfer Objects
│       ├── Data/               # Entity Framework DbContext
│       └── Utilities/          # Helpers (JWT, Hashing)
├── frontend/                   # React + Vite
│   ├── src/
│   │   ├── components/         # Componentes reutilizables
│   │   ├── pages/              # Páginas principales
│   │   ├── services/           # API Integration
│   │   ├── context/            # Estado global (Auth)
│   │   └── styles/             # CSS
│   └── public/
├── database/                   # Scripts SQL
│   ├── 01_create_database.sql
│   └── 02_stored_procedures.sql
└── docs/                       # Documentación
```

## 🛠️ Tecnologías Utilizadas

### Backend
- **ASP.NET Core 8.0**
- **Entity Framework Core 8.0**
- **SQL Server**
- **JWT Bearer Authentication**
- **AutoMapper**
- **iTextSharp** (PDF)
- **ClosedXML** (Excel)
- **BCrypt** (Password Hashing)

### Frontend
- **React 18.2**
- **Vite 5.0**
- **React Router DOM**
- **Axios**
- **Bootstrap 5**
- **ApexCharts**
- **jsPDF y html2canvas**
- **XLSX**

## 📦 Módulos del Sistema

1. **Información General** - Datos de empresa
2. **Misión** - Propósito empresarial
3. **Visión** - Visión de futuro
4. **Valores** - Valores corporativos
5. **Objetivos Estratégicos** - Metas empresariales
6. **Análisis FODA** - Fortalezas, Debilidades, Oportunidades, Amenazas
7. **Cadena de Valor** - Actividades primarias y de apoyo
8. **Auto Cadena de Valor** - Evaluación de áreas
9. **Matriz BCG** - Clasificación de productos
10. **Auto BCG** - Evaluación BCG
11. **Análisis Porter** - 5 Fuerzas competitivas
12. **Auto Porter** - Evaluación de fuerzas
13. **Análisis PEST** - Factores políticos, económicos, sociales, tecnológicos
14. **Identificación Estratégica** - Estrategias clave
15. **Matriz CAME** - Estrategias de corrección
16. **Resumen Ejecutivo** - Resumen automático
17. **Reporte Final** - Exportación completa

## 🚀 Instalación y Configuración

### Requisitos Previos

- .NET 8.0 SDK ([Descargar](https://dotnet.microsoft.com/download/dotnet/8.0))
- SQL Server 2019 o superior ([Descargar](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))
- Node.js 18+ ([Descargar](https://nodejs.org/))
- Visual Studio Code o Visual Studio 2022

### Paso 1: Preparar la Base de Datos

1. Abrir **SQL Server Management Studio**
2. Conectarse al servidor local (generalmente: `localhost\\SQLEXPRESS`)
3. Abrir el archivo `database/01_create_database.sql`
4. Ejecutar el script completo
5. Abrir el archivo `database/02_stored_procedures.sql`
6. Ejecutar para crear los procedimientos almacenados

### Paso 2: Configurar el Backend

```bash
cd backend/PlanEstrategico.API

# Restaurar dependencias
dotnet restore

# Aplicar migraciones (opcional, el Program.cs lo hace automáticamente)
dotnet ef database update

# Ejecutar en desarrollo
dotnet run

# El API estará disponible en: http://localhost:5000
```

**Configuración en `appsettings.json`:**
- Verificar conexión a base de datos
- Cambiar JWT SecretKey en producción
- Configurar CORS permitidos

### Paso 3: Instalar Frontend

```bash
cd frontend

# Instalar dependencias
npm install

# Ejecutar en desarrollo
npm run dev

# El Frontend estará disponible en: http://localhost:5173
```

**Crear archivo `.env` (opcional):**
```
VITE_API_URL=http://localhost:5000/api
```

## 👤 Credenciales de Prueba

El sistema se inicializa con datos de ejemplo. Los roles se asignan automáticamente:

### Crear usuario Admin (Primera vez)
1. Ejecutar el script SQL con INSERT de usuario admin
2. O crear mediante registro y cambiar rol manualmente

## 🔐 Seguridad

- ✅ Contraseñas con hash BCrypt
- ✅ Tokens JWT con expiración configurable
- ✅ Validación de roles y permisos
- ✅ CORS configurado
- ✅ Auditoría de cambios
- ✅ Logging centralizado

## 📊 API Endpoints Principales

### Autenticación
```
POST   /api/auth/login              - Iniciar sesión
POST   /api/auth/register           - Registrarse
GET    /api/usuarios/{id}           - Obtener usuario
```

### Empresas
```
GET    /api/empresas                - Listar empresas
GET    /api/empresas/{id}           - Obtener empresa
POST   /api/empresas                - Crear empresa
PUT    /api/empresas/{id}           - Actualizar empresa
DELETE /api/empresas/{id}           - Eliminar empresa
```

### Módulos Estratégicos
```
GET    /api/misiones/empresa/{id}   - Obtener misión
POST   /api/misiones                - Crear misión
PUT    /api/misiones/{id}           - Actualizar misión

GET    /api/foda/empresa/{id}       - Obtener FODA
GET    /api/foda/empresa/{id}/tipo/{tipo} - Filtrar por tipo
POST   /api/foda                    - Crear análisis
...
```

## 💾 Estructura de Base de Datos

### Tablas principales
- `Usuarios` - Usuarios del sistema
- `Roles` - Roles de acceso
- `Empresa` - Información de empresas
- `Mision`, `Vision`, `Valores` - Información estratégica
- `ObjetivosEstrategicos` - Objetivos
- `AnalisisFODA` - Análisis FODA
- `MatrizBCG` - Matriz BCG
- `AnalisisPorter` - Análisis Porter
- `AnalisisPEST` - Análisis PEST
- `EstrategiaIdentificacion` - Estrategias
- `MatrizCAME` - Matriz CAME
- `Auditoria` - Registro de cambios
- `Logs` - Logs del sistema

## 📱 Funcionalidades Frontend

### Dashboard
- Estadísticas generales
- Últimas empresas
- Gráficos de progreso

### Gestión de Empresas
- CRUD completo
- Lista con filtros
- Vista detallada

### Plan Estratégico
- Tabs para cada módulo
- Formularios dinámicos
- Validación en tiempo real

## 🔧 Desarrollo

### Agregar nuevo módulo

1. **Crear Model** en `Models/`
2. **Crear DTO** en `DTOs/`
3. **Crear Service** en `Services/`
4. **Crear Controller** en `Controllers/`
5. **Registrar Service** en `Program.cs`
6. **Crear React Page** en `frontend/src/pages/`
7. **Agregar route** en `App.jsx`

### Ejemplo: Nuevo módulo "Indicadores"

```csharp
// Model
public class Indicador {
    public int IdIndicador { get; set; }
    public int IdEmpresa { get; set; }
    public string Nombre { get; set; }
    public decimal Valor { get; set; }
    public datetime Fecha { get; set; }
}

// Service
public interface IIndicadorService {
    Task<IEnumerable<IndicadorDto>> GetByEmpresaAsync(int empresaId);
    Task<IndicadorDto> CreateAsync(IndicadorCreateDto dto);
}

// Controller
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IndicadoresController : ControllerBase {
    // Implementación...
}
```

## 📈 Generación de Reportes

### Exportar a PDF
```javascript
// Frontend
import jsPDF from 'jspdf'

const exportToPDF = async () => {
  const doc = new jsPDF()
  // Agregar contenido
  doc.save('plan_estrategico.pdf')
}
```

### Exportar a Excel
```javascript
import XLSX from 'xlsx'

const exportToExcel = (data) => {
  const worksheet = XLSX.utils.json_to_sheet(data)
  const workbook = XLSX.utils.book_new()
  XLSX.utils.book_append_sheet(workbook, worksheet, "Sheet1")
  XLSX.writeFile(workbook, "plan_estrategico.xlsx")
}
```

## 🐛 Solución de Problemas

### Error de conexión a BD
```
Verificar:
- SQL Server ejecutándose
- Connection string en appsettings.json
- Credenciales de SQL Server
- Instancia de SQL Server existente
```

### Error de CORS
```
Solución en appsettings.json:
"Cors": {
  "AllowedOrigins": ["http://localhost:5173"]
}
```

### Token expirado
El token se renueva automáticamente. Si persiste:
```javascript
localStorage.removeItem('token')
localStorage.removeItem('usuario')
// Redirigir a login
```

## 📚 Documentación Adicional

- [Guía de API](./docs/API.md)
- [Manual de Usuario](./docs/MANUAL.md)
- [Estructura de Base de Datos](./docs/DATABASE.md)

## 🤝 Contribución

Para contribuir al proyecto:
1. Fork el repositorio
2. Crear rama (`git checkout -b feature/AmazingFeature`)
3. Commit cambios (`git commit -m 'Add AmazingFeature'`)
4. Push a rama (`git push origin feature/AmazingFeature`)
5. Crear Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver archivo `LICENSE` para más detalles.

## 👥 Autores

Desarrollado como sistema completo de gestión estratégica empresarial.

## 📞 Soporte

Para reportar bugs o solicitar features, crear un issue en el repositorio.

---

**Versión:** 1.0.0
**Última actualización:** Mayo 2026
**Estado:** Producción

Sistema de Elaboración de Plan Estratégico de TI
Sistema web completo para la gestión integral de planes estratégicos empresariales, reemplazando completamente el uso de Excel. La solución incluye módulos para análisis FODA, matrices BCG, análisis Porter, PEST, CAME y más.

📋 Características Principales
✅ Autenticación JWT con 3 roles (Administrador, Analista, Usuario) ✅ 17 módulos estratégicos funcionales ✅ Base de datos relacional con SQL Server ✅ API RESTful completamente funcional ✅ Dashboard con estadísticas y gráficos ✅ Exportación a PDF y Excel ✅ Interfaz responsiva moderna ✅ Auditoría y logs de cambios ✅ Validaciones backend y frontend

🏗️ Arquitectura
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
🛠️ Tecnologías Utilizadas
Backend
ASP.NET Core 8.0
Entity Framework Core 8.0
SQL Server
JWT Bearer Authentication
AutoMapper
iTextSharp (PDF)
ClosedXML (Excel)
BCrypt (Password Hashing)
Frontend
React 18.2
Vite 5.0
React Router DOM
Axios
Bootstrap 5
ApexCharts
jsPDF y html2canvas
XLSX
📦 Módulos del Sistema
Información General - Datos de empresa
Misión - Propósito empresarial
Visión - Visión de futuro
Valores - Valores corporativos
Objetivos Estratégicos - Metas empresariales
Análisis FODA - Fortalezas, Debilidades, Oportunidades, Amenazas
Cadena de Valor - Actividades primarias y de apoyo
Auto Cadena de Valor - Evaluación de áreas
Matriz BCG - Clasificación de productos
Auto BCG - Evaluación BCG
Análisis Porter - 5 Fuerzas competitivas
Auto Porter - Evaluación de fuerzas
Análisis PEST - Factores políticos, económicos, sociales, tecnológicos
Identificación Estratégica - Estrategias clave
Matriz CAME - Estrategias de corrección
Resumen Ejecutivo - Resumen automático
Reporte Final - Exportación completa
🚀 Instalación y Configuración
Requisitos Previos
.NET 8.0 SDK (Descargar)
SQL Server 2019 o superior (Descargar)
Node.js 18+ (Descargar)
Visual Studio Code o Visual Studio 2022
Paso 1: Preparar la Base de Datos
Abrir SQL Server Management Studio
Conectarse al servidor local (generalmente: localhost\\SQLEXPRESS)
Abrir el archivo database/01_create_database.sql
Ejecutar el script completo
Abrir el archivo database/02_stored_procedures.sql
Ejecutar para crear los procedimientos almacenados
Paso 2: Configurar el Backend
cd backend/PlanEstrategico.API

# Restaurar dependencias
dotnet restore

# Aplicar migraciones (opcional, el Program.cs lo hace automáticamente)
dotnet ef database update

# Ejecutar en desarrollo
dotnet run

# El API estará disponible en: http://localhost:5000
Configuración en appsettings.json:

Verificar conexión a base de datos
Cambiar JWT SecretKey en producción
Configurar CORS permitidos
Paso 3: Instalar Frontend
cd frontend

# Instalar dependencias
npm install

# Ejecutar en desarrollo
npm run dev

# El Frontend estará disponible en: http://localhost:5173
Crear archivo .env (opcional):

VITE_API_URL=http://localhost:5000/api
👤 Credenciales de Prueba
El sistema se inicializa con datos de ejemplo. Los roles se asignan automáticamente:

Crear usuario Admin (Primera vez)
Ejecutar el script SQL con INSERT de usuario admin
O crear mediante registro y cambiar rol manualmente
🔐 Seguridad
✅ Contraseñas con hash BCrypt
✅ Tokens JWT con expiración configurable
✅ Validación de roles y permisos
✅ CORS configurado
✅ Auditoría de cambios
✅ Logging centralizado
📊 API Endpoints Principales
Autenticación
POST   /api/auth/login              - Iniciar sesión
POST   /api/auth/register           - Registrarse
GET    /api/usuarios/{id}           - Obtener usuario
Empresas
GET    /api/empresas                - Listar empresas
GET    /api/empresas/{id}           - Obtener empresa
POST   /api/empresas                - Crear empresa
PUT    /api/empresas/{id}           - Actualizar empresa
DELETE /api/empresas/{id}           - Eliminar empresa
Módulos Estratégicos
GET    /api/misiones/empresa/{id}   - Obtener misión
POST   /api/misiones                - Crear misión
PUT    /api/misiones/{id}           - Actualizar misión

GET    /api/foda/empresa/{id}       - Obtener FODA
GET    /api/foda/empresa/{id}/tipo/{tipo} - Filtrar por tipo
POST   /api/foda                    - Crear análisis
...
💾 Estructura de Base de Datos
Tablas principales
Usuarios - Usuarios del sistema
Roles - Roles de acceso
Empresa - Información de empresas
Mision, Vision, Valores - Información estratégica
ObjetivosEstrategicos - Objetivos
AnalisisFODA - Análisis FODA
MatrizBCG - Matriz BCG
AnalisisPorter - Análisis Porter
AnalisisPEST - Análisis PEST
EstrategiaIdentificacion - Estrategias
MatrizCAME - Matriz CAME
Auditoria - Registro de cambios
Logs - Logs del sistema
📱 Funcionalidades Frontend
Dashboard
Estadísticas generales
Últimas empresas
Gráficos de progreso
Gestión de Empresas
CRUD completo
Lista con filtros
Vista detallada
Plan Estratégico
Tabs para cada módulo
Formularios dinámicos
Validación en tiempo real
🔧 Desarrollo
Agregar nuevo módulo
Crear Model en Models/
Crear DTO en DTOs/
Crear Service en Services/
Crear Controller en Controllers/
Registrar Service en Program.cs
Crear React Page en frontend/src/pages/
Agregar route en App.jsx
Ejemplo: Nuevo módulo "Indicadores"
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
📈 Generación de Reportes
Exportar a PDF
// Frontend
import jsPDF from 'jspdf'

const exportToPDF = async () => {
  const doc = new jsPDF()
  // Agregar contenido
  doc.save('plan_estrategico.pdf')
}
Exportar a Excel
import XLSX from 'xlsx'

const exportToExcel = (data) => {
  const worksheet = XLSX.utils.json_to_sheet(data)
  const workbook = XLSX.utils.book_new()
  XLSX.utils.book_append_sheet(workbook, worksheet, "Sheet1")
  XLSX.writeFile(workbook, "plan_estrategico.xlsx")
}
🐛 Solución de Problemas
Error de conexión a BD
Verificar:
- SQL Server ejecutándose
- Connection string en appsettings.json
- Credenciales de SQL Server
- Instancia de SQL Server existente
Error de CORS
Solución en appsettings.json:
"Cors": {
  "AllowedOrigins": ["http://localhost:5173"]
}
Token expirado
El token se renueva automáticamente. Si persiste:

localStorage.removeItem('token')
localStorage.removeItem('usuario')
// Redirigir a login
📚 Documentación Adicional
Guía de API
Manual de Usuario
Estructura de Base de Datos
🤝 Contribución
Para contribuir al proyecto:

Fork el repositorio
Crear rama (git checkout -b feature/AmazingFeature)
Commit cambios (git commit -m 'Add AmazingFeature')
Push a rama (git push origin feature/AmazingFeature)
Crear Pull Request
📄 Licencia
Este proyecto está bajo la Licencia MIT. Ver archivo LICENSE para más detalles.

👥 Autores
Desarrollado como sistema completo de gestión estratégica empresarial.

📞 Soporte
Para reportar bugs o solicitar features, crear un issue en el repositorio.

Versión: 1.0.0 Última actualización: Mayo 2026 Estado: Producción

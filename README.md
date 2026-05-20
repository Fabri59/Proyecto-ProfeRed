# 📊 SISTEMA DE GESTIÓN DE PLAN ESTRATÉGICO DE TI

Un sistema web profesional construido con **ASP.NET Core MVC (.NET 8)** para elaborar, administrar, analizar y generar un Plan Estratégico de Tecnologías de Información completamente digital.

---

## 🚀 Características principales

✅ **Dashboard interactivo** con gráficos en tiempo real  
✅ **Gestión de empresas** (CRUD completo)  
✅ **Autenticación y roles** (Administrador, Analista)  
✅ **Base de datos local con SQLite** (sin instalaciones complejas)  
✅ **Interfaz moderna y responsiva** con Bootstrap 5  
✅ **Datos sincronizados** usando Entity Framework Core  
✅ **DataTables integradas** para consultas avanzadas  

---

## 📋 Contenido

- [Instalación rápida](#instalación-rápida)
- [Usuarios y contraseñas](#usuarios-y-contraseñas)
- [Cómo funciona el sistema](#cómo-funciona-el-sistema)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Tecnologías utilizadas](#tecnologías-utilizadas)
- [Próximos módulos](#próximos-módulos)
- [Soporte](#soporte)

---

## ⚡ Instalación rápida

### Requisitos previos
- **.NET 8 SDK** (descargado e instalado)
- **Git** (opcional)
- **Visual Studio Code** o cualquier editor de texto

### Pasos

1. **Clona o navega al directorio del proyecto:**
   ```bash
   cd c:\xampp\locale\en\LC_MESSAGES\htdocs\Proyecto-Red
   ```

2. **Restaura los paquetes:**
   ```bash
   dotnet restore
   ```

3. **Ejecuta las migraciones:**
   ```bash
   & "$env:USERPROFILE\.dotnet\tools\dotnet-ef.exe" database update --project . --startup-project .
   ```

4. **Inicia la aplicación:**
   ```bash
   dotnet run
   ```

5. **Abre tu navegador:**
   ```
   http://localhost:5000
   ```

---

## 👥 Usuarios y contraseñas

### Usuario Administrador (creado automáticamente)

| Campo | Valor |
|-------|-------|
| **Email** | `admin@empresa.com` |
| **Contraseña** | `Admin123$` |
| **Rol** | Administrador |
| **Acceso** | Total al sistema |

### Crear nuevos usuarios

1. Haz clic en **"¿No tienes cuenta?"** en la pantalla de login
2. Completa el formulario de registro:
   - **Nombre completo:** Tu nombre
   - **Email:** Tu correo (ej: analista@empresa.com)
   - **Contraseña:** Mínimo 8 caracteres, con mayúscula, minúscula y número
   - **Confirmar contraseña:** Repite la contraseña

3. Los nuevos usuarios se registran automáticamente con el rol **"Analista"**
4. Solo el **Administrador** puede cambiar roles

---

## 🔧 Cómo funciona el sistema

### Flujo de acceso

```
┌─────────────────┐
│  Navegador      │
│ localhost:5000  │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│  Página Login   │ ◄─ Sin autenticación (acceso público)
│  / Register     │
└────────┬────────┘
         │ Credenciales válidas
         ▼
┌─────────────────────────────┐
│  Dashboard principal        │ ◄─ Requiere autenticación
│  - Gráficos                 │
│  - Métricas clave           │
│  - Estadísticas             │
└────────┬────────────────────┘
         │
         ▼
┌──────────────────────────────────┐
│  Módulos del sistema             │
│  ✓ Empresa (CRUD)                │
│  ○ Plan (próximamente)           │
│  ○ FODA, PEST, Porter (próximo)  │
└──────────────────────────────────┘
```

### Componentes principales

#### 1. **Autenticación (Login/Registro)**
- Página pública sin requerir sesión
- Contraseñas hasheadas con BCrypt
- Sesión persistente con cookies seguras
- Validación de email único

#### 2. **Dashboard**
- **Métricas KPI:** Cantidad de planes, objetivos, estrategias
- **Gráfico de avance:** Donut chart con progreso general
- **Indicadores:** Badges con información importante
- **Actualización en tiempo real** desde la base de datos

#### 3. **Gestión de Empresas (CRUD)**
- **Crear:** Registra una nueva empresa
- **Leer:** Visualiza lista de empresas en tabla dinámica
- **Actualizar:** Edita información existente
- **Eliminar:** Borra empresa del sistema

#### 4. **Interfaz responsiva**
- **Sidebar colapsable** para navegación lateral
- **NavBar superior** con información de usuario
- **Tarjetas (Cards)** con diseño moderno
- **Tablas DataTables** con búsqueda y ordenamiento
- **Botones iconizados** con Font Awesome

---

## 📁 Estructura del proyecto

```
Proyecto-Red/
├── Controllers/           # Controladores MVC
│   ├── HomeController.cs
│   ├── DashboardController.cs
│   ├── AccountController.cs
│   └── EmpresaController.cs
│
├── Models/               # Entidades de dominio
│   ├── ApplicationUser.cs
│   ├── Empresa.cs
│   ├── Mision.cs
│   ├── Vision.cs
│   └── PlanEntities.cs   # Entidades adicionales
│
├── Views/               # Vistas Razor (.cshtml)
│   ├── Shared/
│   │   ├── _Layout.cshtml      # Layout principal
│   │   ├── _ValidationScripts
│   │   └── Error.cshtml
│   ├── Dashboard/
│   │   └── Index.cshtml
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   └── Empresa/
│       ├── Index.cshtml
│       ├── Create.cshtml
│       ├── Edit.cshtml
│       ├── Details.cshtml
│       └── Delete.cshtml
│
├── Services/            # Lógica de negocio
│   ├── DashboardService.cs
│   └── EmpresaService.cs
│
├── Repositories/        # Acceso a datos
│   ├── Repository.cs
│   └── UnitOfWork.cs
│
├── Interfaces/          # Contratos
│   ├── IRepository.cs
│   ├── IUnitOfWork.cs
│   ├── IEmpresaService.cs
│   └── IDashboardService.cs
│
├── Data/               # Base de datos
│   └── ApplicationDbContext.cs
│
├── Helpers/            # Utilidades
│   └── DbInitializer.cs
│
├── ViewModels/         # Modelos para vistas
│   ├── DashboardViewModel.cs
│   ├── AccountViewModels.cs
│
├── Migrations/         # EF Core migrations
│   ├── [InitialCreate].cs
│   └── ApplicationDbContextModelSnapshot.cs
│
├── wwwroot/           # Archivos estáticos
│   ├── css/
│   ├── js/
│   └── images/
│
├── appsettings.json   # Configuración
├── Program.cs         # Punto de entrada
├── Proyecto-Red.csproj # Archivo del proyecto
└── README.md          # Este archivo
```

---

## 🛠️ Tecnologías utilizadas

| Tecnología | Versión | Propósito |
|-----------|---------|----------|
| **ASP.NET Core** | 8.0 | Framework web |
| **Entity Framework Core** | 8.0 | ORM para acceso a datos |
| **SQLite** | Última | Base de datos local |
| **Bootstrap** | 5.3.2 | Framework CSS |
| **Chart.js** | 4.4 | Gráficos interactivos |
| **DataTables** | 1.14.5 | Tablas avanzadas |
| **Font Awesome** | 6.5.1 | Iconos |
| **jQuery** | 3.7 | Utilidades JS |

---

## 📚 Configuración de base de datos

### SQLite (Configuración actual)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=PlanEstrategicoTI.db"
  }
}
```

**Ventajas:**
- ✅ No requiere servidor externo
- ✅ Fácil de portabilizar
- ✅ Perfecto para desarrollo

### PostgreSQL (Disponible para producción)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=PlanEstrategicoTI;Username=postgres;Password=123456"
  }
}
```

**Para cambiar a PostgreSQL:**
1. Instala PostgreSQL en tu sistema
2. Crea la base de datos: `CREATE DATABASE PlanEstrategicoTI;`
3. Modifica `appsettings.json` con la cadena de conexión
4. El código ya detecta automáticamente si es PostgreSQL o SQLite

---

## 🔐 Seguridad

✅ **Contraseñas hasheadas** con Identity Framework  
✅ **Autenticación con cookies** seguras  
✅ **CSRF protection** en formularios  
✅ **Autorización por roles** en controladores  
✅ **Validación de datos** en cliente y servidor  

---

## 📅 Próximos módulos

- [ ] **Plan de presentación** (CRUD)
- [ ] **Misión y Visión** (CRUD)
- [ ] **Objetivos estratégicos** (CRUD con progreso)
- [ ] **Matriz FODA** (CRUD + Visualización)
- [ ] **Análisis PEST** (CRUD)
- [ ] **Análisis de Porter** (CRUD)
- [ ] **Matriz BCG** (CRUD + Gráfico)
- [ ] **Cadena de Valor** (CRUD)
- [ ] **KPIs e indicadores** (CRUD)
- [ ] **Plan de acción** (CRUD)
- [ ] **Presupuesto** (CRUD + Cálculos)
- [ ] **Cronograma** (CRUD + Gantt)
- [ ] **Generador de reportes** (PDF/Excel)

---

## 🐛 Solución de problemas

### La aplicación no inicia
```bash
# Limpia la solución
dotnet clean

# Restaura paquetes
dotnet restore

# Intenta nuevamente
dotnet run
```

### Error de base de datos
```bash
# Elimina el archivo de BD
rm PlanEstrategicoTI.db

# Vuelve a crear las migraciones
& "$env:USERPROFILE\.dotnet\tools\dotnet-ef.exe" database update
```

### No puedo iniciar sesión
- Verifica que uses `admin@empresa.com` / `Admin123$`
- Las contraseñas son case-sensitive
- Si registraste un usuario, comprueba que escribiste el email correctamente

---

## 📞 Soporte

Para reportar problemas o sugerencias:
1. Revisa esta documentación
2. Consulta la carpeta `Migrations` para histórico de cambios
3. Verifica los logs en la consola al ejecutar

---

## 📝 Licencia

Este proyecto es de uso libre para propósitos educativos y empresariales.

---

## ✨ Autor

Desarrollado con ASP.NET Core 8 y Bootstrap 5
Diseño moderno y responsivo
Ideal para gestión de planes estratégicos TI

**Versión:** 1.0.0  
**Última actualización:** Mayo 2026  
**Estado:** Producción (Base lista, módulos en desarrollo)

---

## 🎯 Objetivos del sistema

Este sistema busca **reemplazar completamente** un formato Excel tradicional con una plataforma web moderna que ofrezca:

✅ Accesibilidad desde cualquier navegador  
✅ Seguridad y control de acceso  
✅ Escalabilidad para múltiples usuarios  
✅ Visualizaciones en tiempo real  
✅ Automatización de cálculos  
✅ Generación de reportes profesionales  

---

**¡Bienvenido al Sistema de Gestión de Plan Estratégico de TI!** 🚀

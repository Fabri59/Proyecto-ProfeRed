USE [PlanEstrategicopTI];
GO

-- === TABLAS Y OBJETOS (extraído de 01_create_database.sql) ===

-- === TABLA DE USUARIOS Y SEGURIDAD ===
CREATE TABLE IF NOT EXISTS [dbo].[Usuarios]
(
    [IdUsuario] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(150) NOT NULL,
    [Email] NVARCHAR(150) NOT NULL UNIQUE,
    [NombreUsuario] NVARCHAR(50) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(500) NOT NULL,
    [IdRol] INT NOT NULL,
    [Activo] BIT NOT NULL DEFAULT 1,
    [FechaCreacion] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [FechaModificacion] DATETIME DEFAULT NULL,
    [UltimoAcceso] DATETIME DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS [dbo].[Roles]
(
    [IdRol] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(50) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(250) NOT NULL
);

CREATE TABLE IF NOT EXISTS [dbo].[Permisos]
(
    [IdPermiso] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(250) NOT NULL
);

CREATE TABLE IF NOT EXISTS [dbo].[RolPermisos]
(
    [IdRolPermiso] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdRol] INT NOT NULL,
    [IdPermiso] INT NOT NULL,
    FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles]([IdRol]),
    FOREIGN KEY ([IdPermiso]) REFERENCES [dbo].[Permisos]([IdPermiso]),
    UNIQUE([IdRol], [IdPermiso])
);

CREATE TABLE IF NOT EXISTS [dbo].[Auditoria]
(
    [IdAuditoria] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdUsuario] INT NOT NULL,
    [Tabla] NVARCHAR(100) NOT NULL,
    [Operacion] NVARCHAR(50) NOT NULL,
    [ValorAnterior] NVARCHAR(MAX) NULL,
    [ValorNuevo] NVARCHAR(MAX) NULL,
    [Fecha] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [DireccionIP] NVARCHAR(50) NULL,
    FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios]([IdUsuario])
);

CREATE TABLE IF NOT EXISTS [dbo].[Logs]
(
    [IdLog] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nivel] NVARCHAR(50) NOT NULL,
    [Mensaje] NVARCHAR(MAX) NOT NULL,
    [Excepcion] NVARCHAR(MAX) NULL,
    [Fecha] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [Usuario] NVARCHAR(150) NULL,
    [Modulo] NVARCHAR(100) NULL
);

CREATE TABLE IF NOT EXISTS [dbo].[Empresa]
(
    [IdEmpresa] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(250) NOT NULL,
    [RUC] NVARCHAR(20) NOT NULL UNIQUE,
    [Direccion] NVARCHAR(500) NOT NULL,
    [Telefono] NVARCHAR(20) NOT NULL,
    [Correo] NVARCHAR(150) NOT NULL,
    [Rubro] NVARCHAR(150) NOT NULL,
    [Descripcion] NVARCHAR(MAX) NULL,
    [Responsable] NVARCHAR(150) NULL,
    [Logo] VARBINARY(MAX) NULL,
    [LogoNombre] NVARCHAR(250) NULL,
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [FechaModificacion] DATETIME NULL,
    [Activo] BIT NOT NULL DEFAULT 1
);

-- (rest of tables...)
-- For brevity, use the provided full scripts in 01_create_database.sql and 02_stored_procedures.sql if needed.

GO

-- === PROCEDIMIENTOS (ejecutar 02_stored_procedures.sql) ===
-- Si prefieres ejecutar el script completo de procedimientos, usa 02_stored_procedures.sql

PRINT '03_apply_schema.sql creado. Revise y ejecute 02_stored_procedures.sql si es necesario.';

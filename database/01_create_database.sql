-- === SCRIPT DE CREACIÓN DE BASE DE DATOS ===
-- Sistema de Elaboración de Plan Estratégico de TI
-- Fecha: 2026-05-13

-- Crear base de datos
CREATE DATABASE [PlanEstrategicopTI]
CONTAINMENT = NONE
ON PRIMARY
(
    NAME = N'PlanEstrategicopTI',
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PlanEstrategicopTI.mdf',
    SIZE = 8192KB,
    FILEGROWTH = 65536KB
)
LOG ON
(
    NAME = N'PlanEstrategicopTI_log',
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PlanEstrategicopTI_log.ldf',
    SIZE = 8192KB,
    FILEGROWTH = 65536KB
);

GO

USE [PlanEstrategicopTI];

GO

-- === TABLA DE USUARIOS Y SEGURIDAD ===
CREATE TABLE [dbo].[Usuarios]
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

CREATE TABLE [dbo].[Roles]
(
    [IdRol] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(50) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(250) NOT NULL
);

CREATE TABLE [dbo].[Permisos]
(
    [IdPermiso] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(250) NOT NULL
);

CREATE TABLE [dbo].[RolPermisos]
(
    [IdRolPermiso] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdRol] INT NOT NULL,
    [IdPermiso] INT NOT NULL,
    FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles]([IdRol]),
    FOREIGN KEY ([IdPermiso]) REFERENCES [dbo].[Permisos]([IdPermiso]),
    UNIQUE([IdRol], [IdPermiso])
);

-- === TABLA DE AUDITORÍA ===
CREATE TABLE [dbo].[Auditoria]
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

-- === TABLA DE LOGS ===
CREATE TABLE [dbo].[Logs]
(
    [IdLog] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nivel] NVARCHAR(50) NOT NULL,
    [Mensaje] NVARCHAR(MAX) NOT NULL,
    [Excepcion] NVARCHAR(MAX) NULL,
    [Fecha] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [Usuario] NVARCHAR(150) NULL,
    [Modulo] NVARCHAR(100) NULL
);

GO

-- === MÓDULO 1: INFORMACIÓN GENERAL (EMPRESA) ===
CREATE TABLE [dbo].[Empresa]
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

GO

-- === MÓDULO 2: MISIÓN ===
CREATE TABLE [dbo].[Mision]
(
    [IdMision] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Descripcion] NVARCHAR(MAX) NOT NULL,
    [Responsable] NVARCHAR(150) NULL,
    [FechaActualizacion] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 3: VISIÓN ===
CREATE TABLE [dbo].[Vision]
(
    [IdVision] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Descripcion] NVARCHAR(MAX) NOT NULL,
    [Responsable] NVARCHAR(150) NULL,
    [Fecha] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 4: VALORES ===
CREATE TABLE [dbo].[Valores]
(
    [IdValor] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Nombre] NVARCHAR(150) NOT NULL,
    [Descripcion] NVARCHAR(MAX) NOT NULL,
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 5: OBJETIVOS ESTRATÉGICOS Y UEN ===
CREATE TABLE [dbo].[ObjetivosEstrategicos]
(
    [IdObjetivo] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Objetivo] NVARCHAR(MAX) NOT NULL,
    [UEN] NVARCHAR(250) NOT NULL,
    [Indicador] NVARCHAR(250) NOT NULL,
    [Meta] NVARCHAR(250) NOT NULL,
    [Responsable] NVARCHAR(150) NULL,
    [Estado] NVARCHAR(50) DEFAULT 'En progreso',
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [FechaModificacion] DATETIME NULL,
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 6: ANÁLISIS INTERNO Y EXTERNO (FODA) ===
CREATE TABLE [dbo].[AnalisisFODA]
(
    [IdAnalisis] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Tipo] NVARCHAR(20) NOT NULL, -- Fortaleza, Debilidad, Oportunidad, Amenaza
    [Descripcion] NVARCHAR(MAX) NOT NULL,
    [Ponderacion] DECIMAL(5,2) DEFAULT 0,
    [Estado] NVARCHAR(50) DEFAULT 'Activo',
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 7: CADENA DE VALOR ===
CREATE TABLE [dbo].[CadenaValor]
(
    [IdCadenaValor] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [TipoActividad] NVARCHAR(50) NOT NULL, -- Primaria, Apoyo
    [Nombre] NVARCHAR(250) NOT NULL,
    [Descripcion] NVARCHAR(MAX) NULL,
    [Responsable] NVARCHAR(150) NULL,
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 8: AUTODIAGNÓSTICO CADENA DE VALOR ===
CREATE TABLE [dbo].[AutoCadenaValor]
(
    [IdAutoCadena] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Area] NVARCHAR(250) NOT NULL,
    [Puntaje] DECIMAL(5,2) DEFAULT 0,
    [Observacion] NVARCHAR(MAX) NULL,
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 9: MATRIZ BCG ===
CREATE TABLE [dbo].[MatrizBCG]
(
    [IdBCG] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Producto] NVARCHAR(250) NOT NULL,
    [Participacion] DECIMAL(5,2) NOT NULL, -- Porcentaje de participación
    [Crecimiento] DECIMAL(5,2) NOT NULL, -- Porcentaje de crecimiento
    [Clasificacion] NVARCHAR(50) NOT NULL, -- Estrella, Vaca, Interrogante, Perro
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 10: AUTODIAGNÓSTICO BCG ===
CREATE TABLE [dbo].[AutoBCG]
(
    [IdAutoBCG] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Evaluacion] NVARCHAR(250) NOT NULL,
    [Puntaje] DECIMAL(5,2) DEFAULT 0,
    [Comentario] NVARCHAR(MAX) NULL,
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 11: ANÁLISIS PORTER ===
CREATE TABLE [dbo].[AnalisisPorter]
(
    [IdPorter] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Fuerza] NVARCHAR(150) NOT NULL, -- Rivalidad, Nuevos competidores, Poder clientes, Poder proveedores, Sustitutos
    [Descripcion] NVARCHAR(MAX) NOT NULL,
    [Puntaje] DECIMAL(5,2) DEFAULT 0,
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 12: AUTODIAGNÓSTICO PORTER ===
CREATE TABLE [dbo].[AutoPorter]
(
    [IdAutoPorter] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Evaluacion] NVARCHAR(250) NOT NULL,
    [Resultado] NVARCHAR(MAX) NOT NULL,
    [Observacion] NVARCHAR(MAX) NULL,
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 13: ANÁLISIS PEST ===
CREATE TABLE [dbo].[AnalisisPEST]
(
    [IdPEST] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Tipo] NVARCHAR(50) NOT NULL, -- Político, Económico, Social, Tecnológico
    [Descripcion] NVARCHAR(MAX) NOT NULL,
    [Impacto] NVARCHAR(50) DEFAULT 'Medio', -- Bajo, Medio, Alto
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 14: IDENTIFICACIÓN ESTRATÉGICA ===
CREATE TABLE [dbo].[EstrategiaIdentificacion]
(
    [IdEstrategia] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Nombre] NVARCHAR(250) NOT NULL,
    [Tipo] NVARCHAR(100) NOT NULL, -- Tipo de estrategia
    [Prioridad] NVARCHAR(50) NOT NULL, -- Alta, Media, Baja
    [Responsable] NVARCHAR(150) NULL,
    [Estado] NVARCHAR(50) DEFAULT 'Pendiente',
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 15: MATRIZ CAME ===
CREATE TABLE [dbo].[MatrizCAME]
(
    [IdCAME] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Tipo] NVARCHAR(50) NOT NULL, -- Corregir, Afrontar, Mantener, Explotar
    [Estrategia] NVARCHAR(MAX) NOT NULL,
    [Responsable] NVARCHAR(150) NULL,
    [Estado] NVARCHAR(50) DEFAULT 'Activo',
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[Empresa]([IdEmpresa])
);

GO

-- === MÓDULO 16: RESUMEN EJECUTIVO ===
CREATE TABLE [dbo].[ResumenEjecutivo]
(
    [IdResumen] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Contenido] NVARCHAR(MAX) NOT NULL,
    [FechaGeneracion] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [Activo] BIT NOT NULL DEFAULT 1
);

GO

-- === MÓDULO 17: REPORTE FINAL ===
CREATE TABLE [dbo].[ReporteFinal]
(
    [IdReporte] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdEmpresa] INT NOT NULL,
    [Contenido] NVARCHAR(MAX) NOT NULL,
    [Formato] NVARCHAR(50) NOT NULL, -- PDF, Excel, HTML
    [FechaGeneracion] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [Archivo] VARBINARY(MAX) NULL,
    [NombreArchivo] NVARCHAR(250) NULL
);

GO

-- === AGREGAR CLAVES FORÁNEAS ===
ALTER TABLE [dbo].[Usuarios]
ADD FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles]([IdRol]);

GO

-- === CREAR ÍNDICES ===
CREATE INDEX [IX_Usuarios_Email] ON [dbo].[Usuarios]([Email]);
CREATE INDEX [IX_Usuarios_NombreUsuario] ON [dbo].[Usuarios]([NombreUsuario]);
CREATE INDEX [IX_Usuarios_IdRol] ON [dbo].[Usuarios]([IdRol]);

CREATE INDEX [IX_Auditoria_IdUsuario] ON [dbo].[Auditoria]([IdUsuario]);
CREATE INDEX [IX_Auditoria_Tabla] ON [dbo].[Auditoria]([Tabla]);
CREATE INDEX [IX_Auditoria_Fecha] ON [dbo].[Auditoria]([Fecha]);

CREATE INDEX [IX_Empresa_RUC] ON [dbo].[Empresa]([RUC]);

CREATE INDEX [IX_Mision_IdEmpresa] ON [dbo].[Mision]([IdEmpresa]);
CREATE INDEX [IX_Vision_IdEmpresa] ON [dbo].[Vision]([IdEmpresa]);
CREATE INDEX [IX_Valores_IdEmpresa] ON [dbo].[Valores]([IdEmpresa]);
CREATE INDEX [IX_ObjetivosEstrategicos_IdEmpresa] ON [dbo].[ObjetivosEstrategicos]([IdEmpresa]);
CREATE INDEX [IX_AnalisisFODA_IdEmpresa] ON [dbo].[AnalisisFODA]([IdEmpresa]);
CREATE INDEX [IX_AnalisisFODA_Tipo] ON [dbo].[AnalisisFODA]([Tipo]);
CREATE INDEX [IX_CadenaValor_IdEmpresa] ON [dbo].[CadenaValor]([IdEmpresa]);
CREATE INDEX [IX_AutoCadenaValor_IdEmpresa] ON [dbo].[AutoCadenaValor]([IdEmpresa]);
CREATE INDEX [IX_MatrizBCG_IdEmpresa] ON [dbo].[MatrizBCG]([IdEmpresa]);
CREATE INDEX [IX_AutoBCG_IdEmpresa] ON [dbo].[AutoBCG]([IdEmpresa]);
CREATE INDEX [IX_AnalisisPorter_IdEmpresa] ON [dbo].[AnalisisPorter]([IdEmpresa]);
CREATE INDEX [IX_AutoPorter_IdEmpresa] ON [dbo].[AutoPorter]([IdEmpresa]);
CREATE INDEX [IX_AnalisisPEST_IdEmpresa] ON [dbo].[AnalisisPEST]([IdEmpresa]);
CREATE INDEX [IX_EstrategiaIdentificacion_IdEmpresa] ON [dbo].[EstrategiaIdentificacion]([IdEmpresa]);
CREATE INDEX [IX_MatrizCAME_IdEmpresa] ON [dbo].[MatrizCAME]([IdEmpresa]);
CREATE INDEX [IX_ResumenEjecutivo_IdEmpresa] ON [dbo].[ResumenEjecutivo]([IdEmpresa]);
CREATE INDEX [IX_ReporteFinal_IdEmpresa] ON [dbo].[ReporteFinal]([IdEmpresa]);

GO

-- === INSERTAR ROLES INICIALES ===
INSERT INTO [dbo].[Roles] ([Nombre], [Descripcion])
VALUES
    ('Administrador', 'Acceso total al sistema'),
    ('Analista', 'Puede crear y editar estrategias'),
    ('Usuario', 'Solo lectura de documentos');

GO

-- === INSERTAR PERMISOS INICIALES ===
INSERT INTO [dbo].[Permisos] ([Nombre], [Descripcion])
VALUES
    ('Ver.Empresa', 'Ver información de empresa'),
    ('Editar.Empresa', 'Editar información de empresa'),
    ('Crear.Mision', 'Crear misión'),
    ('Editar.Mision', 'Editar misión'),
    ('Ver.Dashboard', 'Ver dashboard'),
    ('Exportar.PDF', 'Exportar a PDF'),
    ('Exportar.Excel', 'Exportar a Excel'),
    ('Ver.Auditoria', 'Ver registro de auditoría'),
    ('Gestion.Usuarios', 'Gestionar usuarios'),
    ('Ver.Reportes', 'Ver reportes');

GO

-- === ASIGNAR PERMISOS A ROLES ===
-- Admin: todos los permisos
INSERT INTO [dbo].[RolPermisos] ([IdRol], [IdPermiso])
SELECT 1, [IdPermiso] FROM [dbo].[Permisos];

-- Analista: permisos de lectura y edición
INSERT INTO [dbo].[RolPermisos] ([IdRol], [IdPermiso])
SELECT 2, [IdPermiso] FROM [dbo].[Permisos]
WHERE [Nombre] IN ('Ver.Empresa', 'Editar.Empresa', 'Crear.Mision', 'Editar.Mision', 'Ver.Dashboard', 'Exportar.PDF', 'Exportar.Excel', 'Ver.Reportes');

-- Usuario: solo lectura
INSERT INTO [dbo].[RolPermisos] ([IdRol], [IdPermiso])
SELECT 3, [IdPermiso] FROM [dbo].[Permisos]
WHERE [Nombre] IN ('Ver.Empresa', 'Ver.Dashboard', 'Ver.Reportes');

GO

PRINT 'Base de datos creada exitosamente';

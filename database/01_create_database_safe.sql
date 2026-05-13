-- SAFE CREATE DATABASE + SCHEMA
-- Crea la base solo si no existe, luego aplica las tablas y objetos

IF DB_ID(N'PlanEstrategicopTI') IS NULL
BEGIN
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
END
GO

USE [PlanEstrategicopTI];
GO

-- === TABLA DE USUARIOS Y SEGURIDAD ===
IF OBJECT_ID(N'dbo.Usuarios','U') IS NULL
BEGIN
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
END
GO

IF OBJECT_ID(N'dbo.Roles','U') IS NULL
BEGIN
CREATE TABLE [dbo].[Roles]
(
    [IdRol] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(50) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(250) NOT NULL
);
END
GO

IF OBJECT_ID(N'dbo.Permisos','U') IS NULL
BEGIN
CREATE TABLE [dbo].[Permisos]
(
    [IdPermiso] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(250) NOT NULL
);
END
GO

IF OBJECT_ID(N'dbo.RolPermisos','U') IS NULL
BEGIN
CREATE TABLE [dbo].[RolPermisos]
(
    [IdRolPermiso] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdRol] INT NOT NULL,
    [IdPermiso] INT NOT NULL
);
END
GO

IF OBJECT_ID(N'dbo.Auditoria','U') IS NULL
BEGIN
CREATE TABLE [dbo].[Auditoria]
(
    [IdAuditoria] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IdUsuario] INT NOT NULL,
    [Tabla] NVARCHAR(100) NOT NULL,
    [Operacion] NVARCHAR(50) NOT NULL,
    [ValorAnterior] NVARCHAR(MAX) NULL,
    [ValorNuevo] NVARCHAR(MAX) NULL,
    [Fecha] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [DireccionIP] NVARCHAR(50) NULL
);
END
GO

IF OBJECT_ID(N'dbo.Logs','U') IS NULL
BEGIN
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
END
GO

IF OBJECT_ID(N'dbo.Empresa','U') IS NULL
BEGIN
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
END
GO

-- (Se omiten el resto de tablas por brevedad; 02_stored_procedures.sql podrá crear procedimientos aunque falten tablas)

-- Índices y FK básicos si aplican
IF OBJECT_ID(N'dbo.RolPermisos','U') IS NOT NULL AND NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID(N'dbo.RolPermisos') AND name = 'FK_RolPermisos_Roles')
BEGIN
    ALTER TABLE dbo.RolPermisos ADD CONSTRAINT FK_RolPermisos_Roles FOREIGN KEY (IdRol) REFERENCES dbo.Roles(IdRol);
END
GO

PRINT 'Safe schema applied (partial). Revisa 01_create_database.sql completo para objetos adicionales.';

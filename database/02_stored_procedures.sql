-- === PROCEDIMIENTOS ALMACENADOS Y VISTAS ===
-- Sistema de Elaboración de Plan Estratégico de TI

USE [PlanEstrategicopTI];

GO

-- === PROCEDIMIENTOS ALMACENADOS DE AUDITORÍA ===
CREATE PROCEDURE [sp_InsertarAuditoria]
    @IdUsuario INT,
    @Tabla NVARCHAR(100),
    @Operacion NVARCHAR(50),
    @ValorAnterior NVARCHAR(MAX) = NULL,
    @ValorNuevo NVARCHAR(MAX) = NULL,
    @DireccionIP NVARCHAR(50) = NULL
AS
BEGIN
    INSERT INTO [dbo].[Auditoria]
    ([IdUsuario], [Tabla], [Operacion], [ValorAnterior], [ValorNuevo], [DireccionIP])
    VALUES
    (@IdUsuario, @Tabla, @Operacion, @ValorAnterior, @ValorNuevo, @DireccionIP);
END;

GO

-- === PROCEDIMIENTOS ALMACENADOS DE USUARIOS ===
CREATE PROCEDURE [sp_CrearUsuario]
    @Nombre NVARCHAR(150),
    @Email NVARCHAR(150),
    @NombreUsuario NVARCHAR(50),
    @PasswordHash NVARCHAR(500),
    @IdRol INT
AS
BEGIN
    INSERT INTO [dbo].[Usuarios]
    ([Nombre], [Email], [NombreUsuario], [PasswordHash], [IdRol])
    VALUES
    (@Nombre, @Email, @NombreUsuario, @PasswordHash, @IdRol);
    
    SELECT SCOPE_IDENTITY() AS IdUsuario;
END;

GO

CREATE PROCEDURE [sp_ObtenerUsuarioPorEmail]
    @Email NVARCHAR(150)
AS
BEGIN
    SELECT 
        [IdUsuario],
        [Nombre],
        [Email],
        [NombreUsuario],
        [PasswordHash],
        [IdRol],
        [Activo],
        [FechaCreacion],
        [UltimoAcceso]
    FROM [dbo].[Usuarios]
    WHERE [Email] = @Email;
END;

GO

CREATE PROCEDURE [sp_ActualizarUltimoAcceso]
    @IdUsuario INT
AS
BEGIN
    UPDATE [dbo].[Usuarios]
    SET [UltimoAcceso] = GETUTCDATE()
    WHERE [IdUsuario] = @IdUsuario;
END;

GO

CREATE PROCEDURE [sp_ObtenerPermisosUsuario]
    @IdUsuario INT
AS
BEGIN
    SELECT p.[IdPermiso], p.[Nombre], p.[Descripcion]
    FROM [dbo].[Permisos] p
    INNER JOIN [dbo].[RolPermisos] rp ON p.[IdPermiso] = rp.[IdPermiso]
    INNER JOIN [dbo].[Usuarios] u ON rp.[IdRol] = u.[IdRol]
    WHERE u.[IdUsuario] = @IdUsuario;
END;

GO

-- === PROCEDIMIENTOS ALMACENADOS DE EMPRESA ===
CREATE PROCEDURE [sp_CrearEmpresa]
    @Nombre NVARCHAR(250),
    @RUC NVARCHAR(20),
    @Direccion NVARCHAR(500),
    @Telefono NVARCHAR(20),
    @Correo NVARCHAR(150),
    @Rubro NVARCHAR(150),
    @Descripcion NVARCHAR(MAX) = NULL,
    @Responsable NVARCHAR(150) = NULL
AS
BEGIN
    INSERT INTO [dbo].[Empresa]
    ([Nombre], [RUC], [Direccion], [Telefono], [Correo], [Rubro], [Descripcion], [Responsable])
    VALUES
    (@Nombre, @RUC, @Direccion, @Telefono, @Correo, @Rubro, @Descripcion, @Responsable);
    
    SELECT SCOPE_IDENTITY() AS IdEmpresa;
END;

GO

CREATE PROCEDURE [sp_ObtenerEmpresaPorId]
    @IdEmpresa INT
AS
BEGIN
    SELECT * FROM [dbo].[Empresa] WHERE [IdEmpresa] = @IdEmpresa;
END;

GO

CREATE PROCEDURE [sp_ActualizarEmpresa]
    @IdEmpresa INT,
    @Nombre NVARCHAR(250),
    @RUC NVARCHAR(20),
    @Direccion NVARCHAR(500),
    @Telefono NVARCHAR(20),
    @Correo NVARCHAR(150),
    @Rubro NVARCHAR(150),
    @Descripcion NVARCHAR(MAX) = NULL,
    @Responsable NVARCHAR(150) = NULL
AS
BEGIN
    UPDATE [dbo].[Empresa]
    SET
        [Nombre] = @Nombre,
        [RUC] = @RUC,
        [Direccion] = @Direccion,
        [Telefono] = @Telefono,
        [Correo] = @Correo,
        [Rubro] = @Rubro,
        [Descripcion] = @Descripcion,
        [Responsable] = @Responsable,
        [FechaModificacion] = GETUTCDATE()
    WHERE [IdEmpresa] = @IdEmpresa;
END;

GO

-- === PROCEDIMIENTOS ALMACENADOS DE OBJETIVOS ===
CREATE PROCEDURE [sp_ObtenerObjetivosPorEmpresa]
    @IdEmpresa INT
AS
BEGIN
    SELECT * FROM [dbo].[ObjetivosEstrategicos]
    WHERE [IdEmpresa] = @IdEmpresa
    ORDER BY [FechaRegistro] DESC;
END;

GO

-- === PROCEDIMIENTOS ALMACENADOS DE FODA ===
CREATE PROCEDURE [sp_ObtenerFODAPorEmpresa]
    @IdEmpresa INT
AS
BEGIN
    SELECT 
        [IdAnalisis],
        [Tipo],
        [Descripcion],
        [Ponderacion],
        [Estado],
        [FechaRegistro]
    FROM [dbo].[AnalisisFODA]
    WHERE [IdEmpresa] = @IdEmpresa
    ORDER BY [Tipo], [Ponderacion] DESC;
END;

GO

CREATE PROCEDURE [sp_ObtenerFODAPorTipo]
    @IdEmpresa INT,
    @Tipo NVARCHAR(20)
AS
BEGIN
    SELECT * FROM [dbo].[AnalisisFODA]
    WHERE [IdEmpresa] = @IdEmpresa AND [Tipo] = @Tipo;
END;

GO

-- === VISTAS PARA REPORTES ===
CREATE VIEW [vw_EmpresaCompleta]
AS
SELECT 
    e.[IdEmpresa],
    e.[Nombre],
    e.[RUC],
    e.[Correo],
    e.[Telefono],
    (SELECT COUNT(*) FROM [dbo].[ObjetivosEstrategicos] WHERE [IdEmpresa] = e.[IdEmpresa]) AS TotalObjetivos,
    (SELECT COUNT(*) FROM [dbo].[AnalisisFODA] WHERE [IdEmpresa] = e.[IdEmpresa]) AS TotalFODA,
    (SELECT COUNT(*) FROM [dbo].[MatrizBCG] WHERE [IdEmpresa] = e.[IdEmpresa]) AS TotalProductosBCG,
    (SELECT COUNT(*) FROM [dbo].[EstrategiaIdentificacion] WHERE [IdEmpresa] = e.[IdEmpresa]) AS TotalEstrategias,
    e.[FechaRegistro]
FROM [dbo].[Empresa] e
WHERE e.[Activo] = 1;

GO

CREATE VIEW [vw_ResumenFODA]
AS
SELECT 
    [IdEmpresa],
    SUM(CASE WHEN [Tipo] = 'Fortaleza' THEN 1 ELSE 0 END) AS Fortalezas,
    SUM(CASE WHEN [Tipo] = 'Debilidad' THEN 1 ELSE 0 END) AS Debilidades,
    SUM(CASE WHEN [Tipo] = 'Oportunidad' THEN 1 ELSE 0 END) AS Oportunidades,
    SUM(CASE WHEN [Tipo] = 'Amenaza' THEN 1 ELSE 0 END) AS Amenazas
FROM [dbo].[AnalisisFODA]
GROUP BY [IdEmpresa];

GO

CREATE VIEW [vw_ClasificacionBCG]
AS
SELECT 
    [IdEmpresa],
    SUM(CASE WHEN [Clasificacion] = 'Estrella' THEN 1 ELSE 0 END) AS Estrellas,
    SUM(CASE WHEN [Clasificacion] = 'Vaca' THEN 1 ELSE 0 END) AS Vacas,
    SUM(CASE WHEN [Clasificacion] = 'Interrogante' THEN 1 ELSE 0 END) AS Interrogantes,
    SUM(CASE WHEN [Clasificacion] = 'Perro' THEN 1 ELSE 0 END) AS Perros
FROM [dbo].[MatrizBCG]
GROUP BY [IdEmpresa];

GO

CREATE VIEW [vw_RegistroActividades]
AS
SELECT 
    'Empresa' AS Tipo,
    [Nombre] AS Descripcion,
    [FechaRegistro] AS Fecha,
    1 AS IdEmpresa
FROM [dbo].[Empresa]
UNION ALL
SELECT 'Misión', [Descripcion], [FechaActualizacion], [IdEmpresa] FROM [dbo].[Mision]
UNION ALL
SELECT 'Visión', [Descripcion], [Fecha], [IdEmpresa] FROM [dbo].[Vision]
UNION ALL
SELECT 'Objetivo', [Objetivo], [FechaRegistro], [IdEmpresa] FROM [dbo].[ObjetivosEstrategicos]
UNION ALL
SELECT 'FODA', [Descripcion], [FechaRegistro], [IdEmpresa] FROM [dbo].[AnalisisFODA];

GO

PRINT 'Procedimientos almacenados y vistas creados exitosamente';

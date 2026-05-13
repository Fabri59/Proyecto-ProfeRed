namespace PlanEstrategico.API.DTOs
{
    // =============== AUTENTICACIÓN ===============
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string RolNombre { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public class RegistroRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public int IdRol { get; set; }
        public string RolNombre { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    // =============== EMPRESA ===============
    public class EmpresaCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string RUC { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rubro { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Responsable { get; set; }
    }

    public class EmpresaDto
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string RUC { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rubro { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Responsable { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }

    // =============== MISIÓN ===============
    public class MisionCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Responsable { get; set; }
    }

    public class MisionDto
    {
        public int IdMision { get; set; }
        public int IdEmpresa { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }

    // =============== VISIÓN ===============
    public class VisionCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Responsable { get; set; }
    }

    public class VisionDto
    {
        public int IdVision { get; set; }
        public int IdEmpresa { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public DateTime Fecha { get; set; }
    }

    // =============== VALORES ===============
    public class ValorCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    public class ValorDto
    {
        public int IdValor { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }

    // =============== OBJETIVOS ESTRATÉGICOS ===============
    public class ObjetivoEstrategicoCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Objetivo { get; set; } = string.Empty;
        public string UEN { get; set; } = string.Empty;
        public string Indicador { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public string Estado { get; set; } = "En progreso";
    }

    public class ObjetivoEstrategicoDto
    {
        public int IdObjetivo { get; set; }
        public int IdEmpresa { get; set; }
        public string Objetivo { get; set; } = string.Empty;
        public string UEN { get; set; } = string.Empty;
        public string Indicador { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }

    // =============== FODA ===============
    public class AnalisisFODACreateDto
    {
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Ponderacion { get; set; }
        public string Estado { get; set; } = "Activo";
    }

    public class AnalisisFODADto
    {
        public int IdAnalisis { get; set; }
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Ponderacion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }

    // =============== CADENA DE VALOR ===============
    public class CadenaValorCreateDto
    {
        public int IdEmpresa { get; set; }
        public string TipoActividad { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Responsable { get; set; }
    }

    public class CadenaValorDto
    {
        public int IdCadenaValor { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoActividad { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Responsable { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    // =============== AUTO CADENA DE VALOR ===============
    public class AutoCadenaValorCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Area { get; set; } = string.Empty;
        public decimal Puntaje { get; set; }
        public string? Observacion { get; set; }
    }

    public class AutoCadenaValorDto
    {
        public int IdAutoCadena { get; set; }
        public int IdEmpresa { get; set; }
        public string Area { get; set; } = string.Empty;
        public decimal Puntaje { get; set; }
        public string? Observacion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    // =============== MATRIZ BCG ===============
    public class MatrizBCGCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Producto { get; set; } = string.Empty;
        public decimal Participacion { get; set; }
        public decimal Crecimiento { get; set; }
        public string Clasificacion { get; set; } = string.Empty;
    }

    public class MatrizBCGDto
    {
        public int IdBCG { get; set; }
        public int IdEmpresa { get; set; }
        public string Producto { get; set; } = string.Empty;
        public decimal Participacion { get; set; }
        public decimal Crecimiento { get; set; }
        public string Clasificacion { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }

    // =============== AUTO BCG ===============
    public class AutoBCGCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Evaluacion { get; set; } = string.Empty;
        public decimal Puntaje { get; set; }
        public string? Comentario { get; set; }
    }

    public class AutoBCGDto
    {
        public int IdAutoBCG { get; set; }
        public int IdEmpresa { get; set; }
        public string Evaluacion { get; set; } = string.Empty;
        public decimal Puntaje { get; set; }
        public string? Comentario { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    // =============== ANÁLISIS PORTER ===============
    public class AnalisisPorterCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Fuerza { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Puntaje { get; set; }
    }

    public class AnalisisPorterDto
    {
        public int IdPorter { get; set; }
        public int IdEmpresa { get; set; }
        public string Fuerza { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Puntaje { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    // =============== AUTO PORTER ===============
    public class AutoPorterCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Evaluacion { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
        public string? Observacion { get; set; }
    }

    public class AutoPorterDto
    {
        public int IdAutoPorter { get; set; }
        public int IdEmpresa { get; set; }
        public string Evaluacion { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
        public string? Observacion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    // =============== ANÁLISIS PEST ===============
    public class AnalisisPESTCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Impacto { get; set; } = "Medio";
    }

    public class AnalisisPESTDto
    {
        public int IdPEST { get; set; }
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Impacto { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }

    // =============== ESTRATEGIA IDENTIFICACIÓN ===============
    public class EstrategiaIdentificacionCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
        public string? Responsable { get; set; }
    }

    public class EstrategiaIdentificacionDto
    {
        public int IdEstrategia { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }

    // =============== MATRIZ CAME ===============
    public class MatrizCAMECreateDto
    {
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Estrategia { get; set; } = string.Empty;
        public string? Responsable { get; set; }
    }

    public class MatrizCAMEDto
    {
        public int IdCAME { get; set; }
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Estrategia { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }

    // =============== RESUMEN EJECUTIVO ===============
    public class ResumenEjecutivoDto
    {
        public int IdResumen { get; set; }
        public int IdEmpresa { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; }
    }

    // =============== REPORTE FINAL ===============
    public class ReporteFinalDto
    {
        public int IdReporte { get; set; }
        public int IdEmpresa { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public string Formato { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; }
    }

    // =============== RESPUESTAS GENÉRICAS ===============
    public class ApiResponse<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Datos { get; set; }
    }

    public class ApiErrorResponse
    {
        public bool Exito { get; set; } = false;
        public string Mensaje { get; set; } = string.Empty;
        public List<string>? Errores { get; set; }
    }

    // =============== ESTADÍSTICAS / DASHBOARD ===============
    public class DashboardDto
    {
        public int TotalEmpresas { get; set; }
        public int TotalEstrategias { get; set; }
        public int TotalObjetivos { get; set; }
        public Dictionary<string, int> ConteoFODA { get; set; } = new();
        public Dictionary<string, int> ClasificacionBCG { get; set; } = new();
        public List<AnalisisPorterDto> AnalisisPorter { get; set; } = new();
        public decimal AvancePromedio { get; set; }
    }
}

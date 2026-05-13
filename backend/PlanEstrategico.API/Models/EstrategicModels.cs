namespace PlanEstrategico.API.Models
{
    /// <summary>
    /// Entidad de Empresa (Módulo 1)
    /// </summary>
    public class Empresa
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
        public byte[]? Logo { get; set; }
        public string? LogoNombre { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; }
        public bool Activo { get; set; } = true;

        // Navegación
        public virtual ICollection<Mision> Misiones { get; set; } = new List<Mision>();
        public virtual ICollection<Vision> Visiones { get; set; } = new List<Vision>();
        public virtual ICollection<Valor> Valores { get; set; } = new List<Valor>();
        public virtual ICollection<ObjetivoEstrategico> ObjetivosEstrategicos { get; set; } = new List<ObjetivoEstrategico>();
        public virtual ICollection<AnalisisFODA> AnalisisFODAs { get; set; } = new List<AnalisisFODA>();
        public virtual ICollection<CadenaValor> CadenasValor { get; set; } = new List<CadenaValor>();
        public virtual ICollection<AutoCadenaValor> AutoCadenasValor { get; set; } = new List<AutoCadenaValor>();
        public virtual ICollection<MatrizBCG> MatricesBCG { get; set; } = new List<MatrizBCG>();
        public virtual ICollection<AutoBCG> AutoBCGs { get; set; } = new List<AutoBCG>();
        public virtual ICollection<AnalisisPorter> AnalisisPorters { get; set; } = new List<AnalisisPorter>();
        public virtual ICollection<AutoPorter> AutoPorters { get; set; } = new List<AutoPorter>();
        public virtual ICollection<AnalisisPEST> AnalisisPESTs { get; set; } = new List<AnalisisPEST>();
        public virtual ICollection<EstrategiaIdentificacion> EstrategiasIdentificacion { get; set; } = new List<EstrategiaIdentificacion>();
        public virtual ICollection<MatrizCAME> MatrizesCAME { get; set; } = new List<MatrizCAME>();
        public virtual ICollection<ResumenEjecutivo> ResumenesEjecutivos { get; set; } = new List<ResumenEjecutivo>();
        public virtual ICollection<ReporteFinal> ReportesFinal { get; set; } = new List<ReporteFinal>();
    }

    /// <summary>
    /// Entidad de Misión (Módulo 2)
    /// </summary>
    public class Mision
    {
        public int IdMision { get; set; }
        public int IdEmpresa { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Visión (Módulo 3)
    /// </summary>
    public class Vision
    {
        public int IdVision { get; set; }
        public int IdEmpresa { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Valores (Módulo 4)
    /// </summary>
    public class Valor
    {
        public int IdValor { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Objetivo Estratégico (Módulo 5)
    /// </summary>
    public class ObjetivoEstrategico
    {
        public int IdObjetivo { get; set; }
        public int IdEmpresa { get; set; }
        public string Objetivo { get; set; } = string.Empty;
        public string UEN { get; set; } = string.Empty;
        public string Indicador { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public string Estado { get; set; } = "En progreso";
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; }

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Análisis FODA (Módulo 6)
    /// </summary>
    public class AnalisisFODA
    {
        public int IdAnalisis { get; set; }
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; } = string.Empty; // Fortaleza, Debilidad, Oportunidad, Amenaza
        public string Descripcion { get; set; } = string.Empty;
        public decimal Ponderacion { get; set; } = 0;
        public string Estado { get; set; } = "Activo";
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Cadena de Valor (Módulo 7)
    /// </summary>
    public class CadenaValor
    {
        public int IdCadenaValor { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoActividad { get; set; } = string.Empty; // Primaria, Apoyo
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Responsable { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Auto Cadena de Valor (Módulo 8)
    /// </summary>
    public class AutoCadenaValor
    {
        public int IdAutoCadena { get; set; }
        public int IdEmpresa { get; set; }
        public string Area { get; set; } = string.Empty;
        public decimal Puntaje { get; set; } = 0;
        public string? Observacion { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Matriz BCG (Módulo 9)
    /// </summary>
    public class MatrizBCG
    {
        public int IdBCG { get; set; }
        public int IdEmpresa { get; set; }
        public string Producto { get; set; } = string.Empty;
        public decimal Participacion { get; set; } = 0;
        public decimal Crecimiento { get; set; } = 0;
        public string Clasificacion { get; set; } = string.Empty; // Estrella, Vaca, Interrogante, Perro
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Auto BCG (Módulo 10)
    /// </summary>
    public class AutoBCG
    {
        public int IdAutoBCG { get; set; }
        public int IdEmpresa { get; set; }
        public string Evaluacion { get; set; } = string.Empty;
        public decimal Puntaje { get; set; } = 0;
        public string? Comentario { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Análisis Porter (Módulo 11)
    /// </summary>
    public class AnalisisPorter
    {
        public int IdPorter { get; set; }
        public int IdEmpresa { get; set; }
        public string Fuerza { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Puntaje { get; set; } = 0;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Auto Porter (Módulo 12)
    /// </summary>
    public class AutoPorter
    {
        public int IdAutoPorter { get; set; }
        public int IdEmpresa { get; set; }
        public string Evaluacion { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
        public string? Observacion { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Análisis PEST (Módulo 13)
    /// </summary>
    public class AnalisisPEST
    {
        public int IdPEST { get; set; }
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; } = string.Empty; // Político, Económico, Social, Tecnológico
        public string Descripcion { get; set; } = string.Empty;
        public string Impacto { get; set; } = "Medio"; // Bajo, Medio, Alto
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Estrategia Identificación (Módulo 14)
    /// </summary>
    public class EstrategiaIdentificacion
    {
        public int IdEstrategia { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty; // Alta, Media, Baja
        public string? Responsable { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Matriz CAME (Módulo 15)
    /// </summary>
    public class MatrizCAME
    {
        public int IdCAME { get; set; }
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; } = string.Empty; // Corregir, Afrontar, Mantener, Explotar
        public string Estrategia { get; set; } = string.Empty;
        public string? Responsable { get; set; }
        public string Estado { get; set; } = "Activo";
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Resumen Ejecutivo (Módulo 16)
    /// </summary>
    public class ResumenEjecutivo
    {
        public int IdResumen { get; set; }
        public int IdEmpresa { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public bool Activo { get; set; } = true;

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }

    /// <summary>
    /// Entidad de Reporte Final (Módulo 17)
    /// </summary>
    public class ReporteFinal
    {
        public int IdReporte { get; set; }
        public int IdEmpresa { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public string Formato { get; set; } = string.Empty; // PDF, Excel, HTML
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public byte[]? Archivo { get; set; }
        public string? NombreArchivo { get; set; }

        // Navegación
        public virtual Empresa? Empresa { get; set; }
    }
}

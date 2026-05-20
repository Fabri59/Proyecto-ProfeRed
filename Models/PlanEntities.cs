using System.ComponentModel.DataAnnotations;

namespace Proyecto_Red.Models
{
    public class ValorInstitucional
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [MaxLength(500)]
        public string? Importancia { get; set; }
    }

    public class ObjetivoEstrategico
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Descripcion { get; set; }

        [MaxLength(200)]
        public string? IndicadorKPI { get; set; }

        [MaxLength(100)]
        public string? Meta { get; set; }

        [MaxLength(150)]
        public string? Responsable { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int PorcentajeAvance { get; set; }

        [MaxLength(50)]
        public string Estado { get; set; } = "Pendiente";
    }

    public class UEN
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Descripcion { get; set; }

        [MaxLength(150)]
        public string? Responsable { get; set; }

        [MaxLength(500)]
        public string? RecursosAsignados { get; set; }
    }

    public class AnalisisInterno
    {
        public int Id { get; set; }
        [MaxLength(1000)] public string? Fortalezas { get; set; }
        [MaxLength(1000)] public string? Debilidades { get; set; }
        [MaxLength(1000)] public string? RecursosTecnologicos { get; set; }
        [MaxLength(1000)] public string? InfraestructuraTI { get; set; }
        [MaxLength(1000)] public string? PersonalTI { get; set; }
        [MaxLength(1000)] public string? SistemasExistentes { get; set; }
    }

    public class AnalisisExterno
    {
        public int Id { get; set; }
        [MaxLength(1000)] public string? Oportunidades { get; set; }
        [MaxLength(1000)] public string? Amenazas { get; set; }
        [MaxLength(1000)] public string? Competencia { get; set; }
        [MaxLength(1000)] public string? Mercado { get; set; }
        [MaxLength(1000)] public string? TendenciasTecnologicas { get; set; }
    }

    public class MatrizFoda
    {
        public int Id { get; set; }
        [MaxLength(1000)] public string? Fortalezas { get; set; }
        [MaxLength(1000)] public string? Oportunidades { get; set; }
        [MaxLength(1000)] public string? Debilidades { get; set; }
        [MaxLength(1000)] public string? Amenazas { get; set; }
        [MaxLength(1000)] public string? EstrategiaFO { get; set; }
        [MaxLength(1000)] public string? EstrategiaFA { get; set; }
        [MaxLength(1000)] public string? EstrategiaDO { get; set; }
        [MaxLength(1000)] public string? EstrategiaDA { get; set; }
    }

    public class AnalisisPEST
    {
        public int Id { get; set; }
        [MaxLength(1000)] public string? Politicos { get; set; }
        [MaxLength(1000)] public string? Economicos { get; set; }
        [MaxLength(1000)] public string? Sociales { get; set; }
        [MaxLength(1000)] public string? Tecnologicos { get; set; }
    }

    public class AnalisisPorter
    {
        public int Id { get; set; }
        [MaxLength(1000)] public string? RivalidadCompetencia { get; set; }
        [MaxLength(1000)] public string? AmenazaNuevosCompetidores { get; set; }
        [MaxLength(1000)] public string? AmenazaSustitutos { get; set; }
        [MaxLength(1000)] public string? PoderClientes { get; set; }
        [MaxLength(1000)] public string? PoderProveedores { get; set; }
    }

    public class CadenaValor
    {
        public int Id { get; set; }
        [MaxLength(500)] public string? LogisticaInterna { get; set; }
        [MaxLength(500)] public string? Operaciones { get; set; }
        [MaxLength(500)] public string? LogisticaExterna { get; set; }
        [MaxLength(500)] public string? Marketing { get; set; }
        [MaxLength(500)] public string? Servicios { get; set; }
        [MaxLength(500)] public string? Infraestructura { get; set; }
        [MaxLength(500)] public string? RecursosHumanos { get; set; }
        [MaxLength(500)] public string? DesarrolloTecnologico { get; set; }
        [MaxLength(500)] public string? Abastecimiento { get; set; }
    }

    public class MatrizBCG
    {
        public int Id { get; set; }
        [MaxLength(150)] public string? ProductoProceso { get; set; }
        public decimal? CrecimientoMercado { get; set; }
        public decimal? ParticipacionMercado { get; set; }
        [MaxLength(50)] public string? Clasificacion { get; set; }
    }

    public class MatrizCAME
    {
        public int Id { get; set; }
        [MaxLength(1000)] public string? Corregir { get; set; }
        [MaxLength(1000)] public string? Afrontar { get; set; }
        [MaxLength(1000)] public string? Mantener { get; set; }
        [MaxLength(1000)] public string? Explotar { get; set; }
    }

    public class Estrategia
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(1000)] public string? Descripcion { get; set; }
        [MaxLength(150)] public string? Responsable { get; set; }
        [MaxLength(50)] public string? Prioridad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        [MaxLength(50)] public string? Tipo { get; set; }
    }

    public class IndicadorKPI
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(1000)] public string? Descripcion { get; set; }
        [MaxLength(500)] public string? Formula { get; set; }
        [MaxLength(100)] public string? Meta { get; set; }
        [MaxLength(100)] public string? ResultadoActual { get; set; }
        public int PorcentajeCumplimiento { get; set; }
        [MaxLength(50)] public string? Semaforo { get; set; }
    }

    public class PlanAccion
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Actividad { get; set; } = string.Empty;
        [MaxLength(150)] public string? Responsable { get; set; }
        [MaxLength(500)] public string? Recursos { get; set; }
        public decimal? Presupuesto { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        [MaxLength(100)] public string? Estado { get; set; }
        [MaxLength(1000)] public string? Observaciones { get; set; }
    }

    public class Presupuesto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Recurso { get; set; } = string.Empty;
        [MaxLength(500)] public string? Descripcion { get; set; }
        public decimal CostoUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoTotal => CostoUnitario * Cantidad;
    }

    public class Cronograma
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Actividad { get; set; } = string.Empty;
        [MaxLength(150)] public string? Responsable { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        [MaxLength(100)] public string? Estado { get; set; }
    }
}

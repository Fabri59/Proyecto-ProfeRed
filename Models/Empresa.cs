using System.ComponentModel.DataAnnotations;

namespace Proyecto_Red.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string RazonSocial { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string RUC { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Sector { get; set; }

        [MaxLength(200)]
        public string? Direccion { get; set; }

        [MaxLength(50)]
        public string? Telefono { get; set; }

        [EmailAddress]
        [MaxLength(150)]
        public string? Correo { get; set; }

        [Url]
        [MaxLength(200)]
        public string? PaginaWeb { get; set; }

        [MaxLength(1000)]
        public string? DescripcionGeneral { get; set; }

        [MaxLength(150)]
        public string? Representante { get; set; }

        [MaxLength(100)]
        public string? PeriodoEstrategico { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}

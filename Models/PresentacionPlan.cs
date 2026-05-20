using System.ComponentModel.DataAnnotations;

namespace Proyecto_Red.Models
{
    public class PresentacionPlan
    {
        public int Id { get; set; }

        [MaxLength(2000)]
        public string? Introduccion { get; set; }

        [MaxLength(2000)]
        public string? DescripcionPlan { get; set; }

        [MaxLength(1000)]
        public string? Finalidad { get; set; }

        [MaxLength(1000)]
        public string? Alcance { get; set; }

        [MaxLength(1500)]
        public string? Importancia { get; set; }
    }
}

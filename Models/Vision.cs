using System.ComponentModel.DataAnnotations;

namespace Proyecto_Red.Models
{
    public class Vision
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}

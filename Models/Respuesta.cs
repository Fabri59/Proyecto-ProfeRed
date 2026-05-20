using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Red.Models
{
    public class Respuesta
    {
        [Key]
        public int IdRespuesta { get; set; }

        [Required]
        public int IdPregunta { get; set; }

        [ForeignKey("IdPregunta")]
        public Pregunta Pregunta { get; set; }

        [Required]
        public string IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public ApplicationUser Usuario { get; set; }

        [Required]
        [Range(0, 4)]
        public int ValorSeleccionado { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Red.Models
{
    public class Pregunta
    {
        [Key]
        public int IdPregunta { get; set; }

        [Required]
        public string Modulo { get; set; }

        [Required]
        public string PreguntaTexto { get; set; }

        public bool Estado { get; set; } = true;

        // Navigation property
        public ICollection<Respuesta> Respuestas { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Red.Models
{
    public class Resultado
    {
        [Key]
        public int IdResultado { get; set; }

        [Required]
        public string IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public ApplicationUser Usuario { get; set; }

        [Required]
        public int PuntajeTotal { get; set; }

        [Required]
        public double Promedio { get; set; }

        [Required]
        public string Nivel { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
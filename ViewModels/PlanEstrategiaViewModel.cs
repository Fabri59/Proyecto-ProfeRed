using Proyecto_Red.Models;

namespace Proyecto_Red.ViewModels
{
    public class PlanEstrategiaViewModel
    {
        public IEnumerable<Mision> Misiones { get; set; } = Enumerable.Empty<Mision>();
        public IEnumerable<Vision> Visiones { get; set; } = Enumerable.Empty<Vision>();
        public IEnumerable<ValorInstitucional> Valores { get; set; } = Enumerable.Empty<ValorInstitucional>();
        public IEnumerable<ObjetivoEstrategico> Objetivos { get; set; } = Enumerable.Empty<ObjetivoEstrategico>();
        public IEnumerable<UEN> UENes { get; set; } = Enumerable.Empty<UEN>();
    }
}
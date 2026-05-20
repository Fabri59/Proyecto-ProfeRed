using Proyecto_Red.Models;

namespace Proyecto_Red.ViewModels
{
    public class PlanResumenViewModel
    {
        public PresentacionPlan? Presentacion { get; set; }
        public Mision? Mision { get; set; }
        public Vision? Vision { get; set; }
        public IEnumerable<ValorInstitucional> Valores { get; set; } = Enumerable.Empty<ValorInstitucional>();
        public IEnumerable<ObjetivoEstrategico> Objetivos { get; set; } = Enumerable.Empty<ObjetivoEstrategico>();
        public IEnumerable<UEN> UENes { get; set; } = Enumerable.Empty<UEN>();
        public AnalisisInterno? AnalisisInterno { get; set; }
        public AnalisisExterno? AnalisisExterno { get; set; }
        public CadenaValor? CadenaValor { get; set; }
        public IEnumerable<MatrizBCG> MatricesBCG { get; set; } = Enumerable.Empty<MatrizBCG>();
        public AnalisisPorter? AnalisisPorter { get; set; }
        public AnalisisPEST? AnalisisPEST { get; set; }
        public MatrizCAME? MatrizCAME { get; set; }
    }
}
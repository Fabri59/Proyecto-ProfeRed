using Proyecto_Red.Models;

namespace Proyecto_Red.ViewModels
{
    public class PlanAnalisisViewModel
    {
        public IEnumerable<AnalisisInterno> AnalisisInternos { get; set; } = Enumerable.Empty<AnalisisInterno>();
        public IEnumerable<AnalisisExterno> AnalisisExternos { get; set; } = Enumerable.Empty<AnalisisExterno>();
        public IEnumerable<CadenaValor> CadenasValor { get; set; } = Enumerable.Empty<CadenaValor>();
        public IEnumerable<MatrizBCG> MatricesBCG { get; set; } = Enumerable.Empty<MatrizBCG>();
        public IEnumerable<AnalisisPorter> AnalisisPorters { get; set; } = Enumerable.Empty<AnalisisPorter>();
        public IEnumerable<AnalisisPEST> AnalisisPESTs { get; set; } = Enumerable.Empty<AnalisisPEST>();
        public IEnumerable<MatrizCAME> MatricesCAME { get; set; } = Enumerable.Empty<MatrizCAME>();
    }
}
using Proyecto_Red.Interfaces;
using Proyecto_Red.Models;
using Proyecto_Red.ViewModels;

namespace Proyecto_Red.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var planes = await _unitOfWork.Repository<PresentacionPlan>().GetAllAsync();
            var objetivos = await _unitOfWork.Repository<ObjetivoEstrategico>().GetAllAsync();
            var estrategias = await _unitOfWork.Repository<Estrategia>().GetAllAsync();

            var porcentaje = objetivos.Any()
                ? objetivos.Average(o => o.PorcentajeAvance)
                : 0.0;

            return new DashboardViewModel
            {
                TotalPlanes = planes.Count(),
                TotalObjetivos = objetivos.Count(),
                TotalEstrategias = estrategias.Count(),
                PorcentajeAvance = Math.Round(porcentaje, 2)
            };
        }
    }
}

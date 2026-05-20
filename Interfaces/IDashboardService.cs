using Proyecto_Red.ViewModels;

namespace Proyecto_Red.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardAsync();
    }
}

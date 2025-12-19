using EcoDashboardService.Application.DTO;

namespace EcoDashboardService.Application.Services
{
    public interface IDashboardService
    {
        Task<DashboardDtoSend> GetDashboard();
    }
}

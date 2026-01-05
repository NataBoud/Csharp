using HealthDashboardService.Application.DTOs;

namespace HealthDashboardService.Application.Services
{
    public interface IDashboardService
    {
        Task<DashboardResponseDto> GetDashboardAsync();
        Task<PatientHistoriqueResponseDto?> GetPatientHistoriqueAsync(Guid patientId);
    }
}

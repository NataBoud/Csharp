using static HealthDashboardService.Application.DTOs.ExternalDtos;

namespace HealthDashboardService.Infrastructure.HttpClients
{
    public interface IPatientServiceClient
    {
        Task<List<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(Guid id);
    }
}

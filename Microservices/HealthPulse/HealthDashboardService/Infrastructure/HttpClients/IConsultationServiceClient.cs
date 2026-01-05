using static HealthDashboardService.Application.DTOs.ExternalDtos;

namespace HealthDashboardService.Infrastructure.HttpClients
{
    public interface IConsultationServiceClient
    {
        Task<List<ConsultationDto>> GetAllConsultationsAsync();
        Task<List<ConsultationDto>> GetConsultationsByPatientIdAsync(Guid patientId);
    }
}

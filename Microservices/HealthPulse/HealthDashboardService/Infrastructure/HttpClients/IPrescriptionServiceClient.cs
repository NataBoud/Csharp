using static HealthDashboardService.Application.DTOs.ExternalDtos;

namespace HealthDashboardService.Infrastructure.HttpClients
{
    public interface IPrescriptionServiceClient
    {
        Task<List<PrescriptionDto>> GetAllPrescriptionsAsync();
        Task<List<PrescriptionDto>> GetPrescriptionsByConsultationIdAsync(Guid consultationId);
    }
}

using static HealthDashboardService.Application.DTOs.ExternalDtos;

namespace HealthDashboardService.Infrastructure.HttpClients
{
    public class ConsultationServiceClient : IConsultationServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ConsultationServiceClient> _logger;

        public ConsultationServiceClient(HttpClient httpClient, ILogger<ConsultationServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ConsultationDto>> GetAllConsultationsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ConsultationDto>>("api/consultations");
                return response ?? new List<ConsultationDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des consultations");
                return new List<ConsultationDto>();
            }
        }

        public async Task<List<ConsultationDto>> GetConsultationsByPatientIdAsync(Guid patientId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ConsultationDto>>($"api/consultations/patient/{patientId}");
                return response ?? new List<ConsultationDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des consultations du patient {Id}", patientId);
                return new List<ConsultationDto>();
            }
        }
    }
}

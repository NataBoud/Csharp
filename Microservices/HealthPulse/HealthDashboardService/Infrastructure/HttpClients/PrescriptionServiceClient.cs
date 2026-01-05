using static HealthDashboardService.Application.DTOs.ExternalDtos;

namespace HealthDashboardService.Infrastructure.HttpClients
{
    public class PrescriptionServiceClient : IPrescriptionServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PrescriptionServiceClient> _logger;

        public PrescriptionServiceClient(HttpClient httpClient, ILogger<PrescriptionServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<PrescriptionDto>> GetAllPrescriptionsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<PrescriptionDto>>("api/prescriptions");
                return response ?? new List<PrescriptionDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des prescriptions");
                return new List<PrescriptionDto>();
            }
        }

        public async Task<List<PrescriptionDto>> GetPrescriptionsByConsultationIdAsync(Guid consultationId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<PrescriptionDto>>($"api/prescriptions/consultation/{consultationId}");
                return response ?? new List<PrescriptionDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des prescriptions de la consultation {Id}", consultationId);
                return new List<PrescriptionDto>();
            }
        }
    }
}

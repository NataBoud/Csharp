using static HealthDashboardService.Application.DTOs.ExternalDtos;

namespace HealthDashboardService.Infrastructure.HttpClients
{
    public class PatientServiceClient : IPatientServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PatientServiceClient> _logger;

        public PatientServiceClient(HttpClient httpClient, ILogger<PatientServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<PatientDto>>("api/patients");
                return response ?? new List<PatientDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des patients");
                return new List<PatientDto>();
            }
        }

        public async Task<PatientDto?> GetPatientByIdAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PatientDto>($"api/patients/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération du patient {Id}", id);
                return null;
            }
        }
    }
}

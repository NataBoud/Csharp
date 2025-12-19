using Gateway.Application.DTO.DashboardDto;

namespace Gateway.Infrastructure
{
    public class DashboardApiClient
    {
        private readonly RestClient<DashboardDtoSend, object> _client;

        public DashboardApiClient(string baseUrl)
        {
            _client = new RestClient<DashboardDtoSend, object>(baseUrl);
        }

        // Récupère les données du dashboard
        public async Task<DashboardDtoSend> GetAsync()
        {
            return await _client.GetRequest<DashboardDtoSend>("");
        }
    }
}

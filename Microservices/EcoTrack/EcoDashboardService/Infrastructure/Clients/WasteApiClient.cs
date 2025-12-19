using EcoDashboardService.Application.DTO;
using EcoDashboardService.Infrastructure.Clients.Rest;

namespace EcoDashboardService.Infrastructure.Clients
{
    public class WasteApiClient
    {
        private readonly RestClient<WasteDtoReceive, object> _client;

        public WasteApiClient(string baseUrl)
        {
            _client = new RestClient<WasteDtoReceive, object>(baseUrl);
        }

        // Récupérer tous les déchets
        public async Task<List<WasteDtoReceive>> GetAllAsync()
        {
            return await _client.GetListRequest("");
        }
    }
}

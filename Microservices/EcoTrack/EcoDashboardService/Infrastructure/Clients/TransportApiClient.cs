using EcoDashboardService.Application.DTO;
using EcoDashboardService.Infrastructure.Clients.Rest;

namespace EcoDashboardService.Infrastructure.Clients
{
    public class TransportApiClient
    {
        private readonly RestClient<TransportDtoReceive, object> _client;

        public TransportApiClient(string baseUrl)
        {
            _client = new RestClient<TransportDtoReceive, object>(baseUrl);
        }

        // Récupérer tous les transports
        public async Task<List<TransportDtoReceive>> GetAllAsync()
        {
            return await _client.GetListRequest("");
        }
    }
}

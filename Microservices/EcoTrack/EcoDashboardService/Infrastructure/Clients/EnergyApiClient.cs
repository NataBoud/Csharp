using EcoDashboardService.Application.DTO;
using EcoDashboardService.Infrastructure.Clients.Rest;

namespace EcoDashboardService.Infrastructure.Clients
{
    public class EnergyApiClient
    {
        private readonly RestClient<EnergyDtoReceive, object> _client;

        public EnergyApiClient(string baseUrl)
        {
            _client = new RestClient<EnergyDtoReceive, object>(baseUrl);
        }

        // Récupérer tous les records d'énergie
        public async Task<List<EnergyDtoReceive>> GetAllAsync()
        {
            return await _client.GetListRequest("");
        }

    }
}

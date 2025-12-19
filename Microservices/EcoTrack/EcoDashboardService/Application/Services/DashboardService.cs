using EcoDashboardService.Application.DTO;
using EcoDashboardService.Infrastructure.Clients;

namespace EcoDashboardService.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly EnergyApiClient _energyClient;
        private readonly WasteApiClient _wasteClient;
        private readonly TransportApiClient _transportClient;

        // Ici on instancie directement les clients avec les URLs
        public DashboardService()
        {
            _energyClient = new EnergyApiClient("https://localhost:7159/api/energy/");
            _wasteClient = new WasteApiClient("http://localhost:5025/api/waste/");
            _transportClient = new TransportApiClient("https://localhost:7185/api/transport/");
        }

        public async Task<DashboardDtoSend> GetDashboard()
        {
            // Appels aux microservices via les clients spécifiques
            var energies = await _energyClient.GetAllAsync();
            var wastes = await _wasteClient.GetAllAsync();
            var transports = await _transportClient.GetAllAsync();

            // Agrégation des données
            return new DashboardDtoSend
            {
                TotalEnergyConsumption = energies.Sum(e => e.ConsommationKWh),
                TotalWasteQuantity = wastes.Sum(w => w.QuantiteKg),
                TotalCO2Emission = transports.Sum(t => t.EmissionCO2)
            };
        }

    }
}

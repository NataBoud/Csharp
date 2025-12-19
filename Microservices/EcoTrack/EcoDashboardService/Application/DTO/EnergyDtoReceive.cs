using System.Text.Json.Serialization;

namespace EcoDashboardService.Application.DTO
{
    public class EnergyDtoReceive
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("consommation_kwh")]
        public double ConsommationKWh { get; set; }
    }
}

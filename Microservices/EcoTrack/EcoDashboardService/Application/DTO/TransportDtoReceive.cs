using System.Text.Json.Serialization;

namespace EcoDashboardService.Application.DTO
{
    public class TransportDtoReceive
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("emission_co2")]
        public double EmissionCO2 { get; set; }
    }
}

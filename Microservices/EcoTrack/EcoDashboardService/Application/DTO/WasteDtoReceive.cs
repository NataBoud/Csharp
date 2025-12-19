using System.Text.Json.Serialization;

namespace EcoDashboardService.Application.DTO
{
    public class WasteDtoReceive
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("quantite_kg")]
        public double QuantiteKg { get; set; }
    }
}

using EnergyMicroservice.Domain.Entity;
using System.Text.Json.Serialization;

namespace EnergyMicroservice.Application.DTO
{
    public class EnergyDtoSend
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("source")]
        public EnergySource Source { get; set; }

        [JsonPropertyName("consommation_kwh")]
        public double ConsommationKWh { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}

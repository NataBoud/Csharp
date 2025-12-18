using WasteMicroservice.Domain.Entity;
using System.Text.Json.Serialization;

namespace WasteMicroservice.Application.DTO
{
    public class WasteDtoSend
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("type")]
        public WasteType Type { get; set; }

        [JsonPropertyName("quantite_kg")]
        public double QuantiteKg { get; set; }

        [JsonPropertyName("taux_recyclage")]
        public double TauxRecyclage { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Gateway.Application.DTO.WasteDto
{
    public enum WasteType
    {
        Plastique,
        Papier,
        Organique
    }

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

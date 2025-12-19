using System.Text.Json.Serialization;

namespace Gateway.Application.DTO.TransportDto
{
    public enum TransportMode
    {
        Voiture,
        Bus,
        Train,
        Velo
    }
    public class TransportDtoSend
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("mode")]
        public TransportMode Mode { get; set; }

        [JsonPropertyName("distance_km")]
        public double DistanceKm { get; set; }

        [JsonPropertyName("facteur_emission")]
        public double FacteurEmission { get; set; }

        [JsonPropertyName("emission_co2")]
        public double EmissionCO2 { get; set; }
    }
}

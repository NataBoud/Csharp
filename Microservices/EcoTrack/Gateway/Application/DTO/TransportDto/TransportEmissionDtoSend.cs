using System.Text.Json.Serialization;

namespace Gateway.Application.DTO.TransportDto
{
    public class TransportEmissionDtoSend
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("emissionCO2")]
        public double EmissionCO2 { get; set; }
    }
}

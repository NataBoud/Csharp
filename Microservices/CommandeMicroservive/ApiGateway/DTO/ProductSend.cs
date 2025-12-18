using System.Text.Json.Serialization;

namespace ApiGateway.DTO
{
    public class ProductSend
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace ApiGateway.DTO
{
    public class OrderSend
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("products")]
        public List<OrderProductSend> Products { get; set; } = new();
    }
}

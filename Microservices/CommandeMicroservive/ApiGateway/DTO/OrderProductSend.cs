using System.Text.Json.Serialization;

namespace ApiGateway.DTO
{
    public class OrderProductSend
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }
    }
}

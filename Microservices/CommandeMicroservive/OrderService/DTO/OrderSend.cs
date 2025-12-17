namespace OrderService.DTO
{
    public class OrderSend
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderProductSend> Products { get; set; } = new();
    }
}

namespace ApiGateway.DTO
{
    public class OrderReceive
    {
        public int UserId { get; set; }
        public List<OrderProductReceive> Products { get; set; }
    }
}

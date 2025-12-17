using OrderService.DTO;
using OrderService.Models;
using OrderService.Repository;

namespace OrderService.Service
{
    public class OrderAppService : IService<OrderReceive, OrderSend>
    {
        private readonly IRepository<Order> repository;

        public OrderAppService(IRepository<Order> repository)
        {
            this.repository = repository;
        }
    }
}

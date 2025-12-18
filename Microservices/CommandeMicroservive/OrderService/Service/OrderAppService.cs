using OrderService.DTO;
using OrderService.Models;
using OrderService.Repository;
using OrderService.Rest;

namespace OrderService.Service
{
    public class OrderAppService : IService<OrderReceive, OrderSend>
    {
        private readonly IRepository<Order> _repository;
        private readonly RestClient<UserExternalDtoSend> _userClient;
        private readonly RestClient<ProductExternalDtoSend> _productClient;

        public OrderAppService(IRepository<Order> repository)
        {
            _repository = repository;
            _userClient = new RestClient<UserExternalDtoSend>("https://localhost:7144/api/user/");
            _productClient = new RestClient<ProductExternalDtoSend>("https://localhost:7234/api/product/");
        }

        private Order DtoToEntity(OrderReceive receive, int? id)
        {
            Order order = new Order()
            {
                UserId = receive.UserId,
                CreatedAt = DateTime.Now,
                Products = receive.Products
                    .Select(p => new OrderProduct
                    {
                        ProductId = p.ProductId,
                        Quantity = p.Quantity
                    }).ToList()
            };

            if (id != null)
                order.Id = (int)id;

            return order;
        }

        private async Task<OrderSend> EntityToDtoAsync(Order order)
        {
            var products = new List<OrderProductSend>();

            foreach (var p in order.Products)
            {
                // Appel au ProductService pour récupérer le vrai nom et prix
                var productInfo = await _productClient.GetRequest(p.ProductId.ToString());
                if (productInfo == null)
                    throw new Exception($"Product {p.ProductId} not found");

                products.Add(new OrderProductSend
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    ProductName = productInfo.Name,
                    Price = productInfo.Price
                });
            }

            return new OrderSend
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                Products = products
            };
        }

        // Create a new order
        public async Task<OrderSend> Create(OrderReceive receive)
        {
            var user = await _userClient.GetRequest(receive.UserId.ToString());
            if (user == null)
                throw new Exception("User not found");

            foreach (var p in receive.Products)
            {
                var product = await _productClient.GetRequest(p.ProductId.ToString());
                if (product == null)
                    throw new Exception($"Product with id {p.ProductId} not found");
            }

            var order = _repository.Create(DtoToEntity(receive, null));

            // Utiliser la version async pour récupérer ProductName et Price
            return await EntityToDtoAsync(order);
        }

        // Update an existing order
        public async Task<OrderSend> Update(OrderReceive receive, int id)
        {
            var order = _repository.GetById(id);
            if (order == null) return null;

            order = _repository.Update(DtoToEntity(receive, id));

            return await EntityToDtoAsync(order);
        }

        // Delete an order by id
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        // Get order by id
        public async Task<OrderSend> GetById(int id)
        {
            var order = _repository.GetById(id);
            if (order == null) return null;

            return await EntityToDtoAsync(order);
        }

        // Get all orders
        public async Task<List<OrderSend>> GetAll()
        {
            var orders = _repository.GetAll();
            var list = new List<OrderSend>();

            foreach (var order in orders)
            {
                list.Add(await EntityToDtoAsync(order));
            }

            return list;
        }
    }
}

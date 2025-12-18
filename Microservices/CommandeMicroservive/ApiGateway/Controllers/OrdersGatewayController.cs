using ApiGateway.DTO;
using ApiGateway.RestClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ApiGateway.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrdersGatewayController : ControllerBase
    {
        private readonly RestClient<OrderSend, OrderReceive> _restClient;

        public OrdersGatewayController()
        {
            _restClient = new RestClient<OrderSend, OrderReceive>("https://localhost:7041/api/order/");
        }

        [HttpGet]
        public async Task<List<OrderSend>> GetAll()
        {
            return await _restClient.GetListRequest("");
        }

        [HttpPost]
        public async Task<OrderSend> PostOrder([FromBody] OrderReceive receive)
        {
            return await _restClient.PostRequest("", receive);
        }
    }
}

using ApiGateway.DTO;
using ApiGateway.RestClient;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductsGatewayController : ControllerBase
    {
        private readonly RestClient<ProductSend, ProductReceive> _restClient;

        public ProductsGatewayController()
        {
            _restClient = new RestClient<ProductSend, ProductReceive>("https://localhost:7234/api/product/");
        }

        [HttpGet]
        public async Task<List<ProductSend>> GetAll()
        {
            return await _restClient.GetListRequest("");
        }

        [HttpPost]
        public async Task<ProductSend> PostUser([FromBody] ProductReceive receive)
        {
            return await _restClient.PostRequest("", receive);
        }
    }
}

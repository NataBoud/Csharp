using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersGatewayController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public OrdersGatewayController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _httpClient.GetAsync("https://localhost:7041/api/Order");
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // GET api/orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7041/api/Order/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // POST api/orders
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] object body)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:7041/api/Order",
                body
            );

            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // PUT api/orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] object body)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"https://localhost:7041/api/Order/{id}",
                body
            );

            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // DELETE api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync(
                $"https://localhost:7041/api/Order/{id}"
            );

            return StatusCode((int)response.StatusCode);
        }
    }
}

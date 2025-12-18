using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersGatewayController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public UsersGatewayController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _httpClient.GetAsync("https://localhost:7144/api/user");
            var content = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, content);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7144/api/user/{id}");
            var content = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, content);
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var body = await new StreamReader(Request.Body).ReadToEndAsync();

            var content = new StringContent(
                body,
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(
                "https://localhost:7144/api/user",
                content
            );

            var responseBody = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, responseBody);
        }
    }
}


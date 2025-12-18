using ApiGateway.DTO;
using ApiGateway.RestClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ApiGateway.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersGatewayController : ControllerBase
    {
        private readonly RestClient<UserSend, UserReceive> _restClient;

        public UsersGatewayController()
        {
            _restClient = new RestClient<UserSend, UserReceive>("https://localhost:7144/api/user");
        }

        [HttpGet]
        public async Task<List<UserSend>> GetAll()
        {
            return await _restClient.GetListRequest("");
        }

        [HttpPost]
        public async Task<UserSend> PostUser([FromBody] UserReceive receive)
        {
            return await _restClient.PostRequest("", receive);
        }

    }
}


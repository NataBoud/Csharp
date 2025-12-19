using Gateway.Application.DTO.DashboardDto;
using Gateway.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardApiClient _dashboardClient;

        public DashboardController()
        {
            _dashboardClient = new DashboardApiClient("https://localhost:7121/api/dashboard/");
        }

        // GET: api/dashboard
        [HttpGet]
        public async Task<ActionResult<DashboardDtoSend>> GetDashboard()
        {
            try
            {
                var dashboard = await _dashboardClient.GetAsync();
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

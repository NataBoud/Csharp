using EcoDashboardService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoDashboardService.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            try
            {
                var dashboard = await _service.GetDashboard();
                return Ok(dashboard);
            }
            catch (HttpRequestException ex)
            {
                // Erreur liée aux appels HTTP aux microservices
                return StatusCode(503, new { Message = "Service indisponible : " + ex.Message });
            }
            catch (Exception ex)
            {
                // Autres erreurs imprévues
                return StatusCode(500, new { Message = "Une erreur est survenue : " + ex.Message });
            }
        }

    }
}

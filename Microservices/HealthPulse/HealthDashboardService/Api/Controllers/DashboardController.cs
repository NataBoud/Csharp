using HealthDashboardService.Application.DTOs;
using HealthDashboardService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthDashboardService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardResponseDto>> GetDashboard()
        {
            var dashboard = await _dashboardService.GetDashboardAsync();
            return Ok(dashboard);
        }

        [HttpGet("patient/{patientId:guid}")]
        public async Task<ActionResult<PatientHistoriqueResponseDto>> GetPatientHistorique(Guid patientId)
        {
            var historique = await _dashboardService.GetPatientHistoriqueAsync(patientId);

            if (historique == null)
                return NotFound(new { message = $"Patient avec l'ID {patientId} non trouvé" });

            return Ok(historique);
        }
    }
}

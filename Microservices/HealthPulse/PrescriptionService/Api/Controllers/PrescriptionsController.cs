using Microsoft.AspNetCore.Mvc;
using PrescriptionService.Application.DTOs;
using PrescriptionService.Application.Services;

namespace PrescriptionService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionAppService _service;

        public PrescriptionsController(IPrescriptionAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var prescription = await _service.GetByIdAsync(id);
            return prescription == null ? NotFound() : Ok(prescription);
        }

        [HttpGet("consultation/{consultationId:guid}")]
        public async Task<IActionResult> GetByConsultation(Guid consultationId)
            => Ok(await _service.GetByConsultationIdAsync(consultationId));

        [HttpPost]
        public async Task<IActionResult> Create(PrescriptionRequestDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}/total-prises")]
        public async Task<IActionResult> GetTotalPrises(Guid id)
        {
            var result = await _service.GetTotalPrisesAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

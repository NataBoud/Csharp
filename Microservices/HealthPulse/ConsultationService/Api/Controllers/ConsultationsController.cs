using Microsoft.AspNetCore.Mvc;
using ConsultationService.Application.Services;
using ConsultationService.Application.DTOs;

namespace ConsultationService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultationsController : ControllerBase
    {
        private readonly IConsultationAppService _service;

        public ConsultationsController(IConsultationAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationResponseDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ConsultationResponseDto>> GetById(Guid id)
        {
            var c = await _service.GetByIdAsync(id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        [HttpGet("patient/{patientId:guid}")]
        public async Task<ActionResult<IEnumerable<ConsultationResponseDto>>> GetByPatient(Guid patientId)
        {
            var list = await _service.GetByPatientIdAsync(patientId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<ConsultationResponseDto>> Create([FromBody] ConsultationRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ConsultationResponseDto>> Update(Guid id, [FromBody] ConsultationRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // Endpoint pour le coût horaire
        [HttpGet("{id:guid}/cout-horaire")]
        public async Task<ActionResult<CoutHoraireResponseDto>> GetCoutHoraire(Guid id)
        {
            var result = await _service.GetCoutHoraireAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}

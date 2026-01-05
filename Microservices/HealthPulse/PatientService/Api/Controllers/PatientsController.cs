using Microsoft.AspNetCore.Mvc;
using PatientService.Application.DTOs;
using PatientService.Application.Services;

namespace PatientService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientAppService _service;

        public PatientsController(IPatientAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResponseDto>>> GetAll()
        {
            var patients = await _service.GetAllAsync();
            return Ok(patients);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PatientResponseDto>> GetById(Guid id)
        {
            var patient = await _service.GetByIdAsync(id);
            if (patient == null)
                return NotFound(new { message = $"Patient avec l'ID {id} non trouvé" });

            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<PatientResponseDto>> Create([FromBody] PatientRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PatientResponseDto>> Update(Guid id, [FromBody] PatientRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = $"Patient avec l'ID {id} non trouvé" });

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Patient avec l'ID {id} non trouvé" });

            return NoContent();
        }
    }
}

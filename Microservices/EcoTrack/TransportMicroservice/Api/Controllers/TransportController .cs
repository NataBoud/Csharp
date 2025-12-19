using Microsoft.AspNetCore.Mvc;
using TransportMicroservice.Application.DTO;
using TransportMicroservice.Application.Service;

namespace TransportMicroservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _service;

        public TransportController(ITransportService service)
        {
            _service = service;
        }

        // GET: api/transport
        [HttpGet]
        public IActionResult GetAll()
        {
            var transports = _service.GetAll();
            return Ok(transports);
        }

        // GET: api/transport/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var transport = _service.GetById(id);
            if (transport == null)
                return NotFound(new { Message = $"Transport with id {id} not found" });

            return Ok(transport);
        }

        // GET: api/transport/{id}/emission
        [HttpGet("{id}/emission")]
        public IActionResult GetEmission(Guid id)
        {
            try
            {
                var emission = _service.GetEmissionById(id);
                return Ok(new { Id = id, EmissionCO2 = emission });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // POST: api/transport
        [HttpPost]
        public IActionResult Create([FromBody] TransportDtoReceive receive)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transport = _service.Create(receive);
            return CreatedAtAction(nameof(GetById), new { id = transport.Id }, transport);
        }

        // PATCH: api/transport/{id}
        [HttpPatch("{id}")]
        public IActionResult Update(Guid id, [FromBody] TransportDtoReceive receive)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _service.Update(id, receive);
            if (updated == null)
                return NotFound(new { Message = $"Transport with id {id} not found" });

            return Ok(updated);
        }
    }
}

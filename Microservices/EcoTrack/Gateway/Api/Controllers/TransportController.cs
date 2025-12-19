using Gateway.Application.DTO.TransportDto;
using Gateway.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TransportController : ControllerBase
    {
        private readonly TransportApiClient _transportClient;

        public TransportController()
        {
            _transportClient = new TransportApiClient("https://localhost:7185/api/transport/");
        }

        // GET: api/transport
        [HttpGet]
        public async Task<ActionResult<List<TransportDtoSend>>> GetAll()
        {
            var transports = await _transportClient.GetAllAsync();
            return Ok(transports);
        }

        // GET: api/transport/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TransportDtoSend>> GetByID(Guid id)
        {
            try
            {
                var transport = await _transportClient.GetByIdAsync(id);
                return Ok(transport);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/transport/{id}/emission
        [HttpGet("{id:guid}/emission")]
        public async Task<ActionResult<TransportEmissionDtoSend>> GetEmission(Guid id)
        {
            try
            {
                var emission = await _transportClient.GetEmissionByIdAsync(id);
                return Ok(emission);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        // POST: api/transport
        [HttpPost]
        public async Task<ActionResult<TransportDtoSend>> Create([FromBody] TransportDtoReceive receive)
        {
            var created = await _transportClient.PostAsync(receive);

            return CreatedAtAction(
                nameof(GetByID),           // action GET by id
                new { id = created.Id },   // route values
                created                   // response body
            );
        }

        // PUT: api/transport/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TransportDtoSend>> Update(
            Guid id,
            [FromBody] TransportDtoReceive receive)
        {
            try
            {
                var updated = await _transportClient.UpdateAsync(id, receive);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/transport/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _transportClient.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}

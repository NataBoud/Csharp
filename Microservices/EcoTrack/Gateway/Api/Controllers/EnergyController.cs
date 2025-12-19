using Gateway.Application.DTO.EnergyDto;
using Gateway.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EnergyController : ControllerBase
    {
        private readonly EnergyApiClient _energyClient;

        public EnergyController()
        {
            _energyClient = new EnergyApiClient("https://localhost:7159/api/energy/");
        }

        // GET: api/energy
        [HttpGet]
        public async Task<ActionResult<List<EnergyDtoSend>>> GetAll()
        {
            var energies = await _energyClient.GetAllAsync();
            return Ok(energies);
        }

        // GET: api/energy/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EnergyDtoSend>> GetByID(Guid id)
        {
            try
            {
                var energy = await _energyClient.GetByIdAsync(id);
                return Ok(energy);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: api/energy
        [HttpPost]
        public async Task<ActionResult<EnergyDtoSend>> Create([FromBody] EnergyDtoReceive receive)
        {
            var created = await _energyClient.PostAsync(receive);

            return CreatedAtAction(
                nameof(GetByID),          // action GET by id
                new { id = created.Id },  // route values
                created                  // body
            );
        }
    }
}

using Gateway.Application.DTO.WasteDto;
using Gateway.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WasteController : ControllerBase
    {
        private readonly WasteApiClient _wasteClient;

        public WasteController()
        {
            _wasteClient = new WasteApiClient("http://localhost:5025/api/waste/");
        }

        // GET: api/waste
        // Récupère la liste de tous les déchets
        [HttpGet]
        public async Task<ActionResult<List<WasteDtoSend>>> GetAll()
        {
            var wastes = await _wasteClient.GetAllAsync();
            return Ok(wastes);
        }

        // GET: api/waste/{id}
        // Récupère un déchet par son identifiant
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<WasteDtoSend>> GetByID(Guid id)
        {
            try
            {
                var waste = await _wasteClient.GetByIdAsync(id);
                return Ok(waste);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST: api/waste
        // Crée un nouveau déchet
        [HttpPost]
        public async Task<ActionResult<WasteDtoSend>> Create([FromBody] WasteDtoReceive receive)
        {
            var created = await _wasteClient.PostAsync(receive);

            return CreatedAtAction(
                nameof(GetByID),           // action GET by id
                new { id = created.Id },   // route values
                created                   // response body
            );
        }

        // PUT: api/waste/{id}
        // Met à jour un déchet existant
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<WasteDtoSend>> Update(
            Guid id,
            [FromBody] WasteDtoReceive receive)
        {
            try
            {
                var updated = await _wasteClient.UpdateAsync(id, receive);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

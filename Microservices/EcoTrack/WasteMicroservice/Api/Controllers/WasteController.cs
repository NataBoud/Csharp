using Microsoft.AspNetCore.Mvc;
using WasteMicroservice.Application.DTO;
using WasteMicroservice.Application.Service;

namespace WasteMicroservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WasteController : ControllerBase
    {
        private readonly IWasteService _wasteService;

        public WasteController(IWasteService wasteService)
        {
            _wasteService = wasteService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var wastes = _wasteService.GetAll();
            return Ok(wastes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var waste = _wasteService.GetById(id);
            if (waste == null) return NotFound(new { Message = $"Waste with id {id} not found" });
            return Ok(waste);
        }

        [HttpPost]
        public IActionResult Create([FromBody] WasteDtoReceive receive)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var waste = _wasteService.Create(receive);
            return CreatedAtAction(nameof(GetById), new { id = waste.Id }, waste);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] WasteDtoReceive receive)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = _wasteService.Update(id, receive);
            if (updated == null) return NotFound(new { Message = $"Waste with id {id} not found" });
            return Ok(updated);
        }
    }
}
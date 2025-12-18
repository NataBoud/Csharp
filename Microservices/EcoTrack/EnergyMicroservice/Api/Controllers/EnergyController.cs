using EnergyMicroservice.Application.DTO;
using EnergyMicroservice.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMicroservice.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EnergyController : ControllerBase
    {

        private readonly IEnergyService _service;

        public EnergyController(IEnergyService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<EnergyDtoSend>> GetAll()
        {
            var energies = _service.GetAll();
            return Ok(energies);
        }

        [HttpGet("{id}")]
        public ActionResult<EnergyDtoSend> GetByID(Guid id)
        {
            var energy = _service.GetById(id);
            return Ok(energy);
        }

        [HttpPost]
        public ActionResult<EnergyDtoSend> Create([FromBody] EnergyDtoReceive request)
        {
            var energy = _service.Create(request);
            return CreatedAtAction(nameof(GetByID), new { id = energy.Id }, energy);
        }

    }
}

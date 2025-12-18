using Microsoft.AspNetCore.Mvc;
using OrderService.DTO;
using OrderService.Service;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IService<OrderReceive, OrderSend> _service;

        public OrderController(IService<OrderReceive, OrderSend> service)
        {
            _service = service;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _service.GetAll();
            return Ok(orders); // 200 OK
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _service.GetById(id);
            if (order == null)
                return NotFound(new { Message = $"Order with id {id} not found" }); // 404
            return Ok(order); // 200 OK
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderReceive orderReceive)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 Bad Request

            try
            {
                var order = await _service.Create(orderReceive);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order); // 201 Created
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message }); // Gestion des erreurs liées aux User/Product non trouvés
            }
        }

        // PUT: api/Order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderReceive orderReceive)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedOrder = await _service.Update(orderReceive, id);
            if (updatedOrder == null)
                return NotFound(new { Message = $"Order with id {id} not found" }); // 404

            return Ok(updatedOrder); // 200 OK
        }

        // DELETE: api/Order/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result)
                return NotFound(new { Message = $"Order with id {id} not found" }); // 404

            return NoContent(); // 204 No Content
        }
    }
}

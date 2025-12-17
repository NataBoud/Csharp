using Microsoft.AspNetCore.Mvc;
using UserService.DTO;
using UserService.Service;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IService<UserReceive, UserSend> _service;

        public UserController(IService<UserReceive, UserSend> service)
        {
            _service = service;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _service.GetAll();
            return Ok(users); // 200 OK avec la liste
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _service.GetById(id);
            if (user == null)
                return NotFound(new { Message = "User not found" }); // 404
            return Ok(user); // 200 OK
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Create([FromBody] UserReceive userReceive)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 Bad Request si données invalides

            var userSend = _service.Create(userReceive);
            return CreatedAtAction(nameof(GetById), new { id = userSend.Id }, userSend); // 201 Created
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserReceive userReceive)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = _service.Update(userReceive, id);
            if (updatedUser == null)
                return NotFound(new { Message = "User not found" }); // 404

            return Ok(updatedUser); // 200 OK
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result)
                return NotFound(new { Message = "User not found" }); // 404

            return NoContent(); // 204 No Content
        }
    }
}

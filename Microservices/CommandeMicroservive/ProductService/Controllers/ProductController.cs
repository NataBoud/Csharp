using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IService<ProductReceive, ProductSend> _service;

        public ProductController(IService<ProductReceive, ProductSend> service)
        {
            _service = service;
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _service.GetAll();
            return Ok(products); // 200 OK avec la liste
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            if (product == null)
                return NotFound(new { Message = "Product not found" }); // 404
            return Ok(product); // 200 OK
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Create([FromBody] ProductReceive productReceive)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 Bad Request si données invalides

            var productSend = _service.Create(productReceive);
            return CreatedAtAction(nameof(GetById), new { id = productSend.Id }, productSend); // 201 Created
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductReceive productReceive)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedProduct = _service.Update(productReceive, id);
            if (updatedProduct == null)
                return NotFound(new { Message = "Product not found" }); // 404

            return Ok(updatedProduct); // 200 OK
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result)
                return NotFound(new { Message = "Product not found" }); // 404

            return NoContent(); // 204 No Content
        }
    }
}

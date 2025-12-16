using Microsoft.AspNetCore.Mvc;
using Pizzeria.Models;
using Pizzeria.Repository;

namespace Pizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : Controller
    {
        private readonly IRepository<Ingredient> _repository;

        public IngredientController(IRepository<Ingredient> repository)
        {
            _repository = repository;
        }

        // GET api/ingredient
        [HttpGet]
        public IActionResult GetAll()
        {
            var ingredients = _repository.GetAll();
            return Ok(ingredients);
        }

        // GET api/ingredient/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var ingredient = _repository.Get(id);
                return Ok(ingredient);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Ingredient non trouvé" });
            }
        }

        // POST api/ingredient
        [HttpPost]
        public IActionResult Post([FromBody] Ingredient ingredient)
        {
            _repository.Create(ingredient);
            return CreatedAtAction(nameof(GetById), new { id = ingredient.Id }, ingredient);
        }

        // PUT api/ingredient/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Ingredient ingredient)
        {
            try
            {
                ingredient.Id = id; // s'assurer que l'ID correspond
                _repository.Update(ingredient);
                return NoContent(); // 204
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Ingredient non trouvé" });
            }
        }

        // DELETE api/ingredient/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return NoContent(); // 204
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Ingredient non trouvé" });
            }
        }
    }
}

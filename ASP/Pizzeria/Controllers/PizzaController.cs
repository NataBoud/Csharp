using Microsoft.AspNetCore.Mvc;
using Pizzeria.Models;
using Pizzeria.Repository;
using System.Reflection.Metadata;

namespace Pizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : Controller
    {

        private readonly IRepository<Pizza> _repository;

        public PizzaController(IRepository<Pizza> repository)
        {
            _repository = repository;
        }

        // GET api/pizza
        [HttpGet]
        public IActionResult GetAll()
        {  
            List<Pizza> pizzas = _repository.GetAll();
            return Ok(pizzas);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pizza pizza)
        {
            _repository.Create(pizza);

        // nameof(GetById) : route vers la pizza créée
        // new { id = pizza.Id } : paramètre pour la route
        // pizza: retourne l’objet créé
            return CreatedAtAction(nameof(GetById), new { id = pizza.Id }, pizza);
        }

        // GET api/pizza/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var pizza = _repository.Get(id);
                return Ok(pizza);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Pizza non trouvée" });
            }
        }

        // PUT api/pizza/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pizza pizza)
        {
            try
            {
                pizza.Id = id; // s'assurer que l'ID correspond
                _repository.Update(pizza);
                return NoContent(); // 204
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Pizza non trouvée" });
            }
        }

        // DELETE api/pizza/5
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
                return NotFound(new { Message = "Pizza non trouvée" });
            }
        }
    }
}

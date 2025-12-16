using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public string? Descriptif { get; set; }

        // MANY-TO-MANY
        public List<Pizza> Pizzas { get; set; } = [];
    }
}

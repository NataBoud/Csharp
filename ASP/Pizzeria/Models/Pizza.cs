using Pizzeria.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public string? Description { get; set; }

        [Required]
        public PizzaStatut Statut { get; set; }

        // MANY-TO-MANY
        public List<Ingredient> Ingredients { get; set; } = [];
    }
}

using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Nom du produit")]
        public required string Name { get; set; }

        [Display(Name = "Prix")]
        public decimal Price { get; set; }

        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Display(Name = "En promotion")]
        public bool IsOnDiscount { get; set; }

        // Propriété calculée pour l'affichage
        public decimal DiscountedPrice => IsOnDiscount ? Price * 0.8m : Price;
    }
}

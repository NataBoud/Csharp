using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CaisseEnregistreuse.Models
{
    [Index(nameof(Nom), IsUnique = true)]
    public class Categorie
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; } = string.Empty;

        // Relation avec les produits
        public List<Produit> Produits { get; set; } = new List<Produit>();
    }
}

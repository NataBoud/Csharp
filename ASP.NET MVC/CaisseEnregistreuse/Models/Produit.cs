using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaisseEnregistreuse.Models
{
    [Index(nameof(Nom), IsUnique = true)]
    public class Produit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du produit est obligatoire")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "La description est obligatoire")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prix est obligatoire")]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix doit être positif")]
        public decimal Prix { get; set; }

        [Required(ErrorMessage = "La quantité en stock est obligatoire")]
        [Range(0, int.MaxValue, ErrorMessage = "La quantité ne peut pas être négative")]
        public int QuantiteStock { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner une catégorie")]
        public int CategorieId { get; set; }

        [ForeignKey(nameof(CategorieId))]
        [ValidateNever]
        public Categorie? Categorie { get; set; }
    }
}

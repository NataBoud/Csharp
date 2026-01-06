using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    [Display(Name = "Fiche contact")]
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [Display(Name = "Nom complet")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        [Display(Name = "Adresse e-mail")]
        public required string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PatientService.Application.DTOs
{
    public class PatientRequestDto
    {
        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre 2 et 100 caractères")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date de naissance est obligatoire")]
        public DateTime DateNaissance { get; set; }

        [Required(ErrorMessage = "Le groupe sanguin est obligatoire")]
        public string GroupeSanguin { get; set; } = string.Empty;
    }
}

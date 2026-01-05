using System.ComponentModel.DataAnnotations;

namespace ConsultationService.Application.DTOs
{
    public class ConsultationRequestDto
    {
        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public string Motif { get; set; } = string.Empty;

        [Required]
        public DateTime DateConsultation { get; set; }

        [Required]
        [Range(1, 480, ErrorMessage = "La durée doit être entre 1 et 480 minutes")]
        public int DureeMinutes { get; set; }

        [Required]
        [Range(0.01, 1000, ErrorMessage = "Le tarif doit être entre 0.01 et 1000 €")]
        public decimal Tarif { get; set; }
    }
}

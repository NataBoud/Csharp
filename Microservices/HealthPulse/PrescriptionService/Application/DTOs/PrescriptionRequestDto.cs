using System.ComponentModel.DataAnnotations;

namespace PrescriptionService.Application.DTOs
{
    public class PrescriptionRequestDto
    {
        [Required]
        public Guid ConsultationId { get; set; }

        [Required]
        [StringLength(200)]
        public string Medicament { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Dosage { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Frequence { get; set; } = string.Empty;

        [Required]
        [Range(1, 365)]
        public int DureeJours { get; set; }

        public bool Renouvelable { get; set; } = false;
    }
}

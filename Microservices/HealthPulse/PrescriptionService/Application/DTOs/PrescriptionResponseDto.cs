namespace PrescriptionService.Application.DTOs
{
    public class PrescriptionResponseDto
    {
        public Guid Id { get; set; }
        public Guid ConsultationId { get; set; }
        public string Medicament { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Frequence { get; set; } = string.Empty;
        public int DureeJours { get; set; }
        public bool Renouvelable { get; set; }
    }
}

namespace ConsultationService.Application.DTOs
{
    public class ConsultationResponseDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string Motif { get; set; } = string.Empty;
        public DateTime DateConsultation { get; set; }
        public int DureeMinutes { get; set; }
        public decimal Tarif { get; set; }
    }

    public class CoutHoraireResponseDto
    {
        public Guid ConsultationId { get; set; }
        public decimal Tarif { get; set; }
        public int DureeMinutes { get; set; }
        public decimal CoutHoraire { get; set; }
    }
}

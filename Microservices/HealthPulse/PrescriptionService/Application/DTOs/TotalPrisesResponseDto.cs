namespace PrescriptionService.Application.DTOs
{
    public class TotalPrisesResponseDto
    {
        public Guid PrescriptionId { get; set; }
        public string Medicament { get; set; } = string.Empty;
        public string Frequence { get; set; } = string.Empty;
        public int DureeJours { get; set; }
        public int TotalPrises { get; set; }
    }
}

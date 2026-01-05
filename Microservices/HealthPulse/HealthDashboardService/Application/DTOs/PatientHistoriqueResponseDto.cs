using static HealthDashboardService.Application.DTOs.ExternalDtos;

namespace HealthDashboardService.Application.DTOs
{
    public class PatientHistoriqueResponseDto
    {
        public PatientDto Patient { get; set; } = null!;
        public List<ConsultationDto> Consultations { get; set; } = new();
        public List<PrescriptionDto> Prescriptions { get; set; } = new();
    }
}

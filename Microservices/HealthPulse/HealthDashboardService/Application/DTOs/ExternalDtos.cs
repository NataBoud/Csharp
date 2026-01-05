namespace HealthDashboardService.Application.DTOs
{
    public class ExternalDtos
    {
        // DTOs pour désérialiser les réponses des autres services
        public class PatientDto
        {
            public Guid Id { get; set; }
            public string Nom { get; set; } = string.Empty;
            public string GroupeSanguin { get; set; } = string.Empty;
        }

        public class ConsultationDto
        {
            public Guid Id { get; set; }
            public Guid PatientId { get; set; }
            public string Motif { get; set; } = string.Empty;
            public decimal Tarif { get; set; }
        }

        public class PrescriptionDto
        {
            public Guid Id { get; set; }
            public Guid ConsultationId { get; set; }
            public string Medicament { get; set; } = string.Empty;
        }
    }
}

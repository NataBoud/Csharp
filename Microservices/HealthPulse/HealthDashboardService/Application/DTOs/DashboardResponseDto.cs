namespace HealthDashboardService.Application.DTOs
{
    public class DashboardResponseDto
    {
        public int TotalPatients { get; set; }
        public Dictionary<string, int> ConsultationsParType { get; set; } = new();
        public decimal ChiffreAffairesTotal { get; set; }
        public int TotalPrescriptions { get; set; }
        public Dictionary<string, int> PatientsParGroupeSanguin { get; set; } = new();
        public DateTime GenereLe { get; set; } = DateTime.UtcNow;
    }
}

namespace ConsultationService.Domain.Entities
{

    public enum MotifConsultation
    {
        Routine,
        Urgence,
        Suivi,
        Vaccination
    }
    public class Consultation
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public MotifConsultation Motif { get; set; }
        public DateTime DateConsultation { get; set; }
        public int DureeMinutes { get; set; }
        public decimal Tarif { get; set; }

        public decimal CalculerCoutHoraire()
        {
            if (DureeMinutes <= 0) return 0;
            return (Tarif / DureeMinutes) * 60;
        }
    }
}

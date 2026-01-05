namespace PatientService.Application.DTOs
{
    public class PatientResponseDto
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public DateTime DateNaissance { get; set; }
        public string GroupeSanguin { get; set; } = string.Empty;
        public DateTime DateInscription { get; set; }
        public int Age { get; set; }
    }
}

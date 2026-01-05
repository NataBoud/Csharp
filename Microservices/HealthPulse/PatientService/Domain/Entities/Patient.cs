namespace PatientService.Domain.Entities
{
    public enum GroupeSanguin
    {
        APositif,
        ANegatif,
        BPositif,
        BNegatif,
        ABPositif,
        ABNegatif,
        OPositif,
        ONegatif
    }

    public class Patient
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public DateTime DateNaissance { get; set; }
        public GroupeSanguin GroupeSanguin { get; set; }
        public DateTime DateInscription { get; set; }

        public int CalculerAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateNaissance.Year;
            if (DateNaissance.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}

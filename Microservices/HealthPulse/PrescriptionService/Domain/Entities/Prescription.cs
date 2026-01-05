namespace PrescriptionService.Domain.Entities
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public Guid ConsultationId { get; set; }
        public string Medicament { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Frequence { get; set; } = string.Empty;
        public int DureeJours { get; set; }
        public bool Renouvelable { get; set; }

        public int CalculerTotalPrises()
        {
            // Parse la fréquence pour extraire le nombre de prises par jour
            // Ex: "3 fois/jour" -> 3
            var frequenceParJour = ExtractFrequenceParJour();
            return frequenceParJour * DureeJours;
        }

        private int ExtractFrequenceParJour()
        {
            // Extraction simple du premier nombre trouvé
            var match = System.Text.RegularExpressions.Regex.Match(Frequence, @"\d+");
            return match.Success ? int.Parse(match.Value) : 1;
        }
    }
}

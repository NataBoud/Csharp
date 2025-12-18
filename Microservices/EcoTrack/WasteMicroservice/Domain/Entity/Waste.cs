namespace WasteMicroservice.Domain.Entity

{
    public enum WasteType
    {
        Plastique,
        Papier,
        Organique
    }

    public class Waste
    {
        public Guid Id { get; private set; } 

        public WasteType Type { get; set; }

        public double QuantiteKg { get; set; }

        public double TauxRecyclage { get; set; } // en pourcentage : 50.0 = 50%
    }
}

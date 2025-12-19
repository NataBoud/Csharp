using System.ComponentModel.DataAnnotations.Schema;

namespace TransportMicroservice.Domain.Entity

{
    public enum TransportMode
    {
        Voiture,
        Bus,
        Train,
        Velo
    }

    public class Transport
    {
        public Guid Id { get; set; }

        public TransportMode Mode { get; set; }

        public double DistanceKm { get; set; }

        public double FacteurEmission { get; set; } // g CO2 / km

        // annotation pour ne pas sauvegarder en bdd 
        [NotMapped]
        public double EmissionCO2 => DistanceKm * FacteurEmission;
    }
}

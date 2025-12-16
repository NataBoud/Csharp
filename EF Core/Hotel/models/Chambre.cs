using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.models
{
    public class Chambre
    {
        [Key]
        public int Numero { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Le nombre de lits doit être entre 1 et 10.")]
        public int? NombreLits { get; set; }

        [Required]
        [Range(0.0, 10000.0, ErrorMessage = "Le prix par nuit doit être positif.")]
        public decimal? Tarif { get; set; }

        [Required]
        public StatutChambre Statut { get; set; }
        public List<Reservation> Reservations { get; set; }

        // Constructeur par défaut
        public Chambre() 
        {
            Statut = StatutChambre.Libre;
        }

        
    }
}

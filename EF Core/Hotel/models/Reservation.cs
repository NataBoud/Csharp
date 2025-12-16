using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.models
{
    public class Reservation
    {
        public int Id { get; set; }

        // Relation avec Client
        [Required]
        public Client Client { get; set; }

        public List<Chambre> Chambres { get; set; }

        // Statut de la réservation
        [Required]
        public StatutReservation Statut { get; set; } = StatutReservation.prevue;

        // Constructeur par défaut
        public Reservation() { }

       

    }
}

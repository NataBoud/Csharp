using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.models
{
    internal class Reservation
    {
        public int Id { get; set; }

        // Relation avec Client
        [Required]
        public int? ClientId { get; set; }
        public Client Client { get; set; }

        // Relation avec Chambre
        [Required]
        public int? ChambreId { get; set; }
        public Chambre Chambre { get; set; }

        // Dates de la réservation
        [Required]
        public DateTime DateDebut { get; set; }

        [Required]
        public DateTime DateFin { get; set; }

        // Statut de la réservation
        [Required]
        public StatutReservation Statut { get; set; } = StatutReservation.prevue;

        // Constructeur par défaut
        public Reservation() { }

        // Constructeur avec paramètres
        public Reservation(Client client, Chambre chambre, DateTime dateDebut, DateTime dateFin, StatutReservation statut = StatutReservation.prevue)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            ClientId = client.Id;

            Chambre = chambre ?? throw new ArgumentNullException(nameof(chambre));
            ChambreId = chambre.Id;

            DateDebut = dateDebut;
            DateFin = dateFin;
            Statut = statut;
        }

        // Override ToString
        public override string ToString()
        {
            return $"Reservation {Id} : {Client.Nom} {Client.Prenom} - Chambre {Chambre.Id} du {DateDebut:d} au {DateFin:d}, Statut : {Statut}";
        }
    }
}

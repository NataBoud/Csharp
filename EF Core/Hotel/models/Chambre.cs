using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.models
{
    internal class Chambre
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Le nombre de lits doit être entre 1 et 10.")]
        public int NbLit { get; set; }

        [Required]
        [Range(0.0, 10000.0, ErrorMessage = "Le prix par nuit doit être positif.")]
        public decimal PrixParNuit { get; set; }

        [Required]
        public StatutChambre Statut { get; set; } = StatutChambre.Libre;

        // Relation avec Client : facultative si la chambre est libre
        public int? ClientId { get; set; }  // clé étrangère optionnelle
        public Client Client { get; set; }  // navigation property

        // Constructeur par défaut
        public Chambre() { }

        // Constructeur avec paramètres
        public Chambre(int nbLit, decimal prixParNuit, StatutChambre statut = StatutChambre.Libre, Client client = null)
        {
            NbLit = nbLit;
            PrixParNuit = prixParNuit;
            Statut = statut;
            Client = client;
            ClientId = client?.Id;
        }

        // Override ToString
        public override string ToString()
        {
            string clientInfo = Client != null ? $" - Occupée par {Client.Nom} {Client.Prenom}" : "";
            return $"Chambre {Id} : {NbLit} lits, {PrixParNuit:C} par nuit, Statut : {Statut}{clientInfo}";
        }
    }
}

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ExoCommande.Classes
{
    internal class Commande
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public DateTime DateCommande { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Constructeur pour créer une nouvelle commande
        public Commande(Client client, decimal total)
        {
            Client = client;
            Total = total;
            DateCommande = DateTime.Now;
        }

        // Constructeur pour hydrater une commande depuis SQL
        public Commande(int id, Client client, DateTime dateCommande, decimal total,
                        DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            Client = client;
            DateCommande = dateCommande;
            Total = total;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // :d = format date courte (ex : 27/02/2025) 
        public override string ToString()
        {
            string totalFormate = $"{Total:0.00} €";  // Format 230.00 €

            return $"{Id} - {Client.Nom} - {DateCommande:dd/MM/yyyy} - {totalFormate}";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ExoCommande.Classes
{
    internal class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string CodePostal { get; set; } = string.Empty;
        public string Ville { get; set; } = string.Empty;
        public string? Telephone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Constructeur pour une création côté code
        public Client(string nom, string prenom, string adresse, string codePostal, string ville, string? telephone = null)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            CodePostal = codePostal;
            Ville = ville;
            Telephone = telephone;
        }

        // Constructeur pour l’hydratation depuis SQL
        public Client(int id, string nom, string prenom, string adresse, string codePostal, string ville, string? telephone,
                      DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            CodePostal = codePostal;
            Ville = ville;
            Telephone = telephone;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public override string ToString()
        {
            return $"{Id} - {Nom} {Prenom} - {Ville}";
        }   

    }
}

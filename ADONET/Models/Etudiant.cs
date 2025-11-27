using System;
using System.Collections.Generic;
using System.Text;

namespace ADONET.Models
{
    internal class Etudiant
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public int NumeroClasse { get; set; }
        public DateTime? DateDiplome { get; set; }

        // Constructeur vide
        // (obligatoire pour l'hydratation - (remplir un objet avec des valeurs venant de la BDD) - depuis la BDD)
        public Etudiant() { }

        // Constructeur sans ID (pour un nouvel étudiant)
        public Etudiant(string nom, string prenom, int numeroClasse, DateTime? dateDiplome)
        {
            Nom = nom;
            Prenom = prenom;
            NumeroClasse = numeroClasse;
            DateDiplome = dateDiplome;
        }

        // Constructeur complet (utile pour GetById, GetList)
        public Etudiant(int id, string nom, string prenom, int numeroClasse, DateTime? dateDiplome)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            NumeroClasse = numeroClasse;
            DateDiplome = dateDiplome;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nom: {Nom}, Prénom: {Prenom}, Classe: {NumeroClasse}, " +
                   $"Diplôme: {(DateDiplome?.ToString("yyyy-MM-dd") ?? "N/A")}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace test2.Classes
{
    internal class Salarie
    {
        // Attributs d'instance
        public string Matricule { get; set; }
        public string Service { get; set; }
        public string Categorie { get; set; }
        public string Nom { get; set; }
        public double Salaire { get; set; }

        // Champs statiques pour suivre le total
        private static int nbEmployes = 0;
        private static double salaireTotal = 0;

        // Constructeur par défaut
        public Salarie()
        {
            Matricule = "0000";
            Service = "Inconnu";
            Categorie = "A";
            Nom = "Inconnu";
            Salaire = 0;

            nbEmployes++;
            salaireTotal += Salaire;
        }

        // Constructeur avec paramètres
        public Salarie(string matricule, string service, string categorie, string nom, double salaire)
        {
            Matricule = matricule;
            Service = service;
            Categorie = categorie;
            Nom = nom;
            Salaire = salaire;

            nbEmployes++;
            salaireTotal += salaire;
        }

        // Méthode pour afficher le salaire
        public void AfficherSalaire()
        {
            Console.WriteLine($"Nom : {Nom}, Matricule : {Matricule}, Service : {Service}, Catégorie : {Categorie}, Salaire : {Salaire} €");
        }

        // Méthodes statiques pour obtenir les totaux
        public static void AfficherTotaux()
        {
            Console.WriteLine($"Nombre total d'employés : {nbEmployes}");
            Console.WriteLine($"Salaire total : {salaireTotal} €");
        }

        public static void RemettreAZero()
        {
            nbEmployes = 0;
            salaireTotal = 0;
            Console.WriteLine("Le nombre d'employés et le salaire total ont été remis à zéro.");
        }
    }
}

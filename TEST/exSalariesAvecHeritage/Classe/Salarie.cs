using System;
using System.Collections.Generic;
using System.Text;

namespace exSalariesAvecHeritage.Classe
{
    internal class Salarie
    {
        // Attributs
        public string Matricule { get; set; }
        public string Service { get; set; }
        public string Categorie { get; set; }
        public string Nom { get; set; }
        public double Salaire { get; set; }

        // Attributs statiques
        public static int NombreEmployes { get; private set; } = 0;
        public static double SalaireTotal { get; private set; } = 0;

        // Constructeur par défaut
        public Salarie()
        {
            NombreEmployes++;
        }

        // Constructeur avec paramètres
        public Salarie(string matricule, string nom, string service, string categorie, double salaire)
        {
            Matricule = matricule;
            Nom = nom;
            Service = service;
            Categorie = categorie;
            Salaire = salaire;

            NombreEmployes++;
            SalaireTotal += salaire;
        }

        // Afficher le salaire
        public virtual void AfficherSalaire()
        {
            Console.WriteLine($"{Nom} ({Matricule}) : {Salaire} €");
        }

        // Remettre à zéro les statistiques
        public static void ResetStatistiques()
        {
            NombreEmployes = 0;
            SalaireTotal = 0;
        }

        public override string ToString()
        {
            return $"[{Matricule}] {Nom} - Service: {Service}, Catégorie: {Categorie}, Salaire: {Salaire}€";
        }

    }


}

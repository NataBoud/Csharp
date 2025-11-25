using System;
using System.Collections.Generic;
using System.Text;
using ExHeritageCompteBancaire.Enums;

namespace ExHeritageCompteBancaire.Classes
{
    internal class CompteEpargne : CompteBancaire
    {
        public double TauxInteret { get; set; } // en %

        public CompteEpargne(Client client, double soldeInitial = 0, double tauxInteret = 0)
           : base(client, soldeInitial)
        {
            TauxInteret = tauxInteret;
        }

        public void AppliquerInteret()
        {
            double interet = Solde * TauxInteret / 100.0;
            Depot(interet);
            Console.WriteLine($"Intérêt de {interet}€ appliqué.");
        }

    }
}

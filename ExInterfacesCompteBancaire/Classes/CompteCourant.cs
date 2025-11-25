using System;
using System.Collections.Generic;
using System.Text;
using ExHeritageCompteBancaire.Enums;

namespace ExHeritageCompteBancaire.Classes
{
    internal class CompteCourant : CompteBancaire
    {
        public double DecouvertAutorise { get; set; }

        // Le constructeur correct :
        public CompteCourant(Client client, double soldeInitial = 0, double decouvertAutorise = 0)
            : base(client, soldeInitial) 
        {
            DecouvertAutorise = decouvertAutorise;
        }



        public override bool Retrait(double montant)
        {
            if (montant <= 0)
            {
                Console.WriteLine("Montant invalide.");
                return false;
            }

            if (montant > Solde + DecouvertAutorise)
            {
                Console.WriteLine("Fonds insuffisants, découvert dépassé !");
                return false;
            }

            Solde -= montant;

            Operations.Add(new Operation(
                Operations.Count + 1,
                montant,
                StatutOperation.Retrait
            ));

            return true;
        }


    }
}

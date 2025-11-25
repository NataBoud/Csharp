using System;
using System.Collections.Generic;
using System.Text;
using ExHeritageCompteBancaire.Enums;

namespace ExHeritageCompteBancaire.Classes
{
    internal class ComptePayant : CompteBancaire
    {
        public double Frais { get; set; }

        public ComptePayant(Client client, double soldeInitial = 0, double frais = 1.0)
            : base(client, soldeInitial)
        {
            Frais = frais;
        }

        public override void Depot(double montant)
        {
            base.Depot(montant - Frais);
            Console.WriteLine($"Frais de {Frais}€ appliqués sur le dépôt.");
        }

        public override bool Retrait(double montant)
        {
            return base.Retrait(montant + Frais);
        }
    }
}

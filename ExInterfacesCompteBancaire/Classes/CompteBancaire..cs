using ExHeritageCompteBancaire.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExHeritageCompteBancaire.Classes
{
    internal abstract class CompteBancaire
    {
        public double Solde { get; protected set; }
        public Client Client { get; set; }
        public List<Operation> Operations { get; private set; }

        public int NumeroCompte { get; private set; }
        private static int compteur = 1;

        public CompteBancaire(Client client, double soldeInitial = 0)
        {
            Client = client;
            Solde = soldeInitial;
            Operations = new List<Operation>();

            NumeroCompte = compteur++;
        }

 
        public virtual void Depot(double montant)
        {
            if (montant <= 0)
            {
                Console.WriteLine("Montant invalide.");
                return;
            }

            Solde += montant;

            Operations.Add(new Operation(
                Operations.Count + 1,
                montant,
                StatutOperation.Depot
            ));
        }

        public virtual bool Retrait(double montant)
        {
            if (montant <= 0)
            {
                Console.WriteLine("Montant invalide.");
                return false;
            }

            if (montant > Solde)
            {
                Console.WriteLine("Fonds insuffisants !");
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

        public void AfficherHistorique()
        {
            Console.WriteLine($"\nHistorique du compte {NumeroCompte} :");

            foreach (var op in Operations)
            {
                Console.WriteLine(op);
            }
        }

        public override string ToString()
        {
            return $"Compte n°{NumeroCompte} | Solde : {Solde}€ | Client : {Client.Nom} {Client.Prenom}";
        }
    }
}

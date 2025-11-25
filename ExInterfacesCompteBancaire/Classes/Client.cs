using System;
using System.Collections.Generic;
using System.Text;

namespace ExHeritageCompteBancaire.Classes
{
    internal class Client
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Identifiant { get; set; }
        public string Telephone { get; set; }
        public List<CompteBancaire> Comptes { get; private set; }

        public Client(string nom, string prenom, string identifiant, string telephone)
        {
            Nom = nom;
            Prenom = prenom;
            Identifiant = identifiant;
            Telephone = telephone;
            Comptes = new List<CompteBancaire>();
        }

        public void AjouterCompte(CompteBancaire compte)
        {
            if (compte != null)
            {
                Comptes.Add(compte);
                compte.Client = this; // associer le compte au client
            }
        }

        public override string ToString()
        {
            return $"{Nom} {Prenom} | ID : {Identifiant} | Tel : {Telephone}";
        }
    }
}

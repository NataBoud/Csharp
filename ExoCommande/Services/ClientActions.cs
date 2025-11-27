using ExoCommande.Classes;
using ExoCommande.Dao;
using ExoCommande.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoCommande.Services
{
    internal class ClientActions
    {
        private readonly ClientDao clientDao;
        private readonly CommandeDao commandeDao;

        public ClientActions(ClientDao cd, CommandeDao coDao)
        {
            clientDao = cd;
            commandeDao = coDao;
        }

        public void AfficherClients() => clientDao?.GetAll().ForEach(Console.WriteLine);

        public void CreerClient()
        {
            string nom = InputHelper.AskString("Nom : ");
            string prenom = InputHelper.AskString("Prénom : ");
            string adresse = InputHelper.AskString("Adresse : ");
            string codePostal = InputHelper.AskString("Code Postal : ");
            string ville = InputHelper.AskString("Ville : ");
            string? tel = InputHelper.AskOptionalString("Téléphone : ");

            Client client = new Client(nom, prenom, adresse, codePostal, ville, tel);
            clientDao.Save(client);
            Console.WriteLine($"Client {client.Nom} créé avec l'id {client.Id} !");
        }

        public void ModifierClient()
        {
            Console.Write("Id du client à modifier : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Client? client = clientDao.getOneById(id);
            if (client == null) { Console.WriteLine("Client introuvable !"); return; }

            Console.Write($"Nom ({client.Nom}) : "); string nom = Console.ReadLine()!;
            Console.Write($"Prénom ({client.Prenom}) : "); string prenom = Console.ReadLine()!;
            Console.Write($"Adresse ({client.Adresse}) : "); string adresse = Console.ReadLine()!;
            Console.Write($"Code Postal ({client.CodePostal}) : "); string codePostal = Console.ReadLine()!;
            Console.Write($"Ville ({client.Ville}) : "); string ville = Console.ReadLine()!;
            Console.Write($"Téléphone ({client.Telephone}) : "); string? tel = Console.ReadLine();

            client.Nom = string.IsNullOrEmpty(nom) ? client.Nom : nom;
            client.Prenom = string.IsNullOrEmpty(prenom) ? client.Prenom : prenom;
            client.Adresse = string.IsNullOrEmpty(adresse) ? client.Adresse : adresse;
            client.CodePostal = string.IsNullOrEmpty(codePostal) ? client.CodePostal : codePostal;
            client.Ville = string.IsNullOrEmpty(ville) ? client.Ville : ville;
            client.Telephone = string.IsNullOrEmpty(tel) ? client.Telephone : tel;

            clientDao.Update(client);
            Console.WriteLine("Client modifié !");
        }

        public void SupprimerClient()
        {
            Console.Write("Id du client à supprimer : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            if (clientDao.Delete(id))
                Console.WriteLine("Client supprimé !");
            else
                Console.WriteLine("Erreur lors de la suppression !");
        }

        public void AfficherDetailClient()
        {
            Console.Write("Id du client : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Client? client = clientDao?.getOneById(id);
            if (client == null) { Console.WriteLine("Client introuvable !"); return; }

            Console.WriteLine(client);
            Console.WriteLine("Commandes :");
            commandeDao?.GetCommandesByClientId(id).ForEach(Console.WriteLine);

        }


    }
}

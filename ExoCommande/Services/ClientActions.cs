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

        public void AfficherClients() => clientDao.GetAll().ForEach(Console.WriteLine);

        public void CreerClient()
        {
            string nom = InputHelper.AskString("Nom : ");
            string prenom = InputHelper.AskString("Prénom : ");
            string adresse = InputHelper.AskString("Adresse : ");
            string codePostal = InputHelper.AskString("Code Postal : ");
            string ville = InputHelper.AskString("Ville : ");
            string? tel = InputHelper.AskOptionalString("Téléphone : ", "");

            Client client = new(nom, prenom, adresse, codePostal, ville, tel);
            clientDao.Save(client);
            Console.WriteLine($"Client {client.Nom} créé avec l'id {client.Id} !");
        }

        public void ModifierClient()
        {
            int id = InputHelper.AskInt("Id du client à modifier : ");

            Client? client = clientDao.getOneById(id);
            if (client == null) { Console.WriteLine("Client introuvable !"); return; }

            string nom = InputHelper.AskOptionalString($"Nom ({client.Nom}) : ", client.Nom)!;
            string prenom = InputHelper.AskOptionalString($"Prénom ({client.Prenom}) : ", client.Prenom)!;
            string adresse = InputHelper.AskOptionalString($"Adresse ({client.Adresse}) : ", client.Adresse)!;
            string codePostal = InputHelper.AskOptionalString($"Code Postal ({client.CodePostal}) : ", client.CodePostal)!;
            string ville = InputHelper.AskOptionalString($"Ville ({client.Ville}) : ", client.Ville)!;

            string? tel = InputHelper.AskOptionalString($"Téléphone ({client.Telephone}) : ", client.Telephone);

            client.Nom = nom;
            client.Prenom = prenom;
            client.Adresse = adresse;
            client.CodePostal = codePostal;
            client.Ville = ville;
            client.Telephone = tel;

            clientDao.Update(client);
            Console.WriteLine("Client modifié !");
        }

        public void SupprimerClient()
        {
            int id = InputHelper.AskInt("Id du client à supprimer : ");

            if (clientDao.Delete(id))
                Console.WriteLine("Client supprimé !");
            else
                Console.WriteLine("Erreur lors de la suppression ou client introuvable !");
        }

        public void AfficherDetailClient()
        {
            int id = InputHelper.AskInt("Id du client : ");

            Client? client = clientDao.getOneById(id);
            if (client == null)
            {
                Console.WriteLine("Client introuvable !");
                return;
            }

            Console.WriteLine(client);
            Console.WriteLine("Commandes :");
            commandeDao.GetCommandesByClientId(id).ForEach(Console.WriteLine);
        }

    }
}

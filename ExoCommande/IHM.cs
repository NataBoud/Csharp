using System;
using System.Collections.Generic;
using System.Text;
using ExoCommande.Classes;
using ExoCommande.Dao;

namespace ExoCommande
{
    internal class IHM
    {
        private readonly ClientDao clientDao = new ClientDao();
        private readonly CommandeDao commandeDao = new CommandeDao();

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n=== MENU ===");
                Console.WriteLine("1 - Afficher tous les clients");
                Console.WriteLine("2 - Créer un client");
                Console.WriteLine("3 - Modifier un client");
                Console.WriteLine("4 - Supprimer un client");
                Console.WriteLine("5 - Afficher le détail d'un client");
                Console.WriteLine("6 - Ajouter une commande");
                Console.WriteLine("7 - Modifier une commande");
                Console.WriteLine("8 - Supprimer une commande");
                Console.WriteLine("0 - Quitter");
                Console.Write("Choix : ");

                switch (Console.ReadLine())
                {
                    case "1": AfficherClients();break;
                    case "2":CreerClient();break;
                    case "3":ModifierClient();break;
                    case "4":SupprimerClient();break;
                    case "5":AfficherDetailClient();break;
                    case "6":AjouterCommande();break;
                    case "7":ModifierCommande();break;
                    case "8":SupprimerCommande();break;
                    case "0": return;
                    default:Console.WriteLine("Erreur de saisie !");break;
                }
            }
        }

        private void ModifierCommande()
        {
            Console.Write("Saisir l'ID de la commande à modifier : ");
            int id;
            while (!int.TryParse(Console.ReadLine()!, out id))
            Console.Write("Erreur de saisie, réessayez : ");

            var commande = commandeDao.getOneById(id);
            if (commande == null)
            {
                Console.WriteLine("Commande introuvable !");
                return;
            }
            Console.WriteLine(commande);

            Console.Write("Nouveau total (laissez vide pour ne pas modifier) : ");
            string input = Console.ReadLine()!;
            if (decimal.TryParse(input, out decimal total))
            {
                commande.Total = total;
            }

            // modifier la date commande
            Console.Write("Nouvelle date (jj/mm/aaaa, laissez vide pour ne pas modifier) : ");
            input = Console.ReadLine()!;
            if (DateTime.TryParse(input, out DateTime dateCommande))
            {
                commande.DateCommande = dateCommande;
            }

            if (commandeDao.Update(commande) != null)
            {
                Console.WriteLine("Commande modifiée avec succès !");
            }
            else
            {
                Console.WriteLine("Erreur lors de la modification.");
            }
        }

        private void SupprimerCommande()
        {
            Console.Write("Saisir l'ID de la commande à supprimer : ");
            int id;
            while (!int.TryParse(Console.ReadLine()!, out id))
                Console.Write("Erreur de saisie, réessayez : ");

            if (commandeDao.Delete(id))
            {
                Console.WriteLine("Commande supprimée avec succès !");
            }
            else
            {
                Console.WriteLine("Erreur lors de la suppression ou commande introuvable.");
            }
        }


        private void AfficherClients()
        {
            clientDao.GetAll().ForEach(Console.WriteLine);
        }

        private void CreerClient()
        {
            Console.Write("Nom : "); string nom = Console.ReadLine()!;
            Console.Write("Prénom : "); string prenom = Console.ReadLine()!;
            Console.Write("Adresse : "); string adresse = Console.ReadLine()!;
            Console.Write("Code Postal : "); string codePostal = Console.ReadLine()!;
            Console.Write("Ville : "); string ville = Console.ReadLine()!;
            Console.Write("Téléphone : "); string? tel = Console.ReadLine();

            Client client = new Client(nom, prenom, adresse, codePostal, ville, tel);
            clientDao.Save(client);
            Console.WriteLine($"Client {client.Nom} créé avec l'id {client.Id} !");
        }

        private void ModifierClient()
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

        private void SupprimerClient()
        {
            Console.Write("Id du client à supprimer : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            if (clientDao.Delete(id))
                Console.WriteLine("Client supprimé !");
            else
                Console.WriteLine("Erreur lors de la suppression !");
        }

        private void AfficherDetailClient()
        {
            Console.Write("Id du client : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Client? client = clientDao.getOneById(id);
            if (client == null) { Console.WriteLine("Client introuvable !"); return; }

            Console.WriteLine(client);

            var commandes = commandeDao.GetCommandesByClientId(id); 
            Console.WriteLine("Commandes :");
            commandes.ForEach(Console.WriteLine);
        }

        private void AjouterCommande()
        {
            Console.Write("Id du client : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Client? client = clientDao.getOneById(id);
            if (client == null) { Console.WriteLine("Client introuvable !"); return; }

            Console.Write("Montant total : ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal total)) return;

            Commande commande = commandeDao.AddCommandeToClient(client, total);
            
        }


    }
}

using ExoCommande.Classes;
using ExoCommande.Dao;
using ExoCommande.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoCommande
{
    internal class IHM
    {
        private readonly ClientActions clientActions;
        private readonly CommandeActions commandeActions;

        private readonly ClientDao clientDao = new ClientDao();
        private readonly CommandeDao commandeDao = new CommandeDao();

        public IHM()
        {
            clientActions = new ClientActions(clientDao, commandeDao);
            commandeActions = new CommandeActions(commandeDao, clientDao);
        }

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
                    case "1": AfficherClients(); break;
                    case "2": CreerClient(); break;
                    case "3": ModifierClient(); break;
                    case "4": SupprimerClient(); break;
                    case "5": AfficherDetailClient(); break;
                    case "6": AjouterCommande(); break;
                    case "7": ModifierCommande(); break;
                    case "8": SupprimerCommande(); break;
                    case "0": return;
                    default: Console.WriteLine("Erreur de saisie !"); break;
                }
            }
        }

        private void AfficherClients() => clientActions.AfficherClients();
        private void CreerClient() => clientActions.CreerClient();
        private void ModifierClient() => clientActions.ModifierClient();
        private void SupprimerClient() => clientActions.SupprimerClient();
        private void AfficherDetailClient() => clientActions.AfficherDetailClient();
        private void AjouterCommande() => commandeActions.AjouterCommande();
        private void ModifierCommande() => commandeActions.ModifierCommande();
        private void SupprimerCommande() => commandeActions.SupprimerCommande();
    }
}
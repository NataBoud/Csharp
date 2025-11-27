using ExoCommande.Classes;
using ExoCommande.Dao;
using ExoCommande.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoCommande.Services
{
    internal class CommandeActions
    {
        private readonly CommandeDao commandeDao;
        private readonly ClientDao clientDao;

        public CommandeActions(CommandeDao dao, ClientDao cdao)
        {
            commandeDao = dao;
            clientDao = cdao;
        }


        public void AjouterCommande()
        {
            Console.Write("Id du client : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Client? client = clientDao.getOneById(id);
            if (client == null)
            {
                Console.WriteLine("Client introuvable !");
                return;
            }

            decimal total = InputHelper.AskDecimal("Montant total : ");
            commandeDao.AddCommandeToClient(client, total);

            Console.WriteLine("Commande ajoutée !");
        }

        public void ModifierCommande()
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

            // === MODIFICATION TOTAL ===
            Console.Write($"({commande.Total}) - laissez vide pour ne pas modifier : ");
            string inputTotal = Console.ReadLine()!;

            if (!string.IsNullOrWhiteSpace(inputTotal))
            {
                decimal newTotal;

                while (!decimal.TryParse(inputTotal, out newTotal))
                {
                    Console.Write("Total invalide ! Saisir un nombre (ou vide pour annuler) : ");
                    inputTotal = Console.ReadLine()!;

                    if (string.IsNullOrWhiteSpace(inputTotal))
                    {
                        Console.WriteLine("Modification du total annulée.");
                        break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(inputTotal))
                    commande.Total = newTotal;
            }


            // === MODIFICATION DATE ===
            Console.Write($"({commande.DateCommande}) - jj/mm/aaaa, laissez vide pour ne pas modifier : ");
            string inputDate = Console.ReadLine()!;

            if (!string.IsNullOrWhiteSpace(inputDate))
            {
                DateTime newDate;

                while (!DateTime.TryParse(inputDate, out newDate))
                {
                    Console.Write("Date invalide ! Réessayer (jj/mm/aaaa) ou vide pour annuler : ");
                    inputDate = Console.ReadLine()!;

                    if (string.IsNullOrWhiteSpace(inputDate))
                    {
                        Console.WriteLine("Modification de la date annulée.");
                        break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(inputDate))
                    commande.DateCommande = newDate;
            }

            // === ENREGISTREMENT SQL ===
            commandeDao.Update(commande);
            Console.WriteLine("Modifications sauvegardées !");
        }


        public void SupprimerCommande()
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


    }
}

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
            int id = InputHelper.AskInt("Id du client : ");
            Client? client = clientDao.GetOneById(id);
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
            int id = InputHelper.AskInt("Saisir l'ID de la commande à modifier : ");
            var commande = commandeDao.GetOneById(id);

            if (commande == null)
            {
                Console.WriteLine("Commande introuvable !");
                return;
            }

            Console.WriteLine(commande);

            // modif total
            decimal? newTotal = InputHelper.AskDecimalOrEmpty($"({commande.Total}) - laissez vide pour ne pas modifier : ");
            if (newTotal.HasValue)
                commande.Total = newTotal.Value;

            // modif date
            DateTime? newDate = InputHelper.AskDateOrEmpty($"({commande.DateCommande}) - jj/mm/aaaa, laissez vide pour ne pas modifier : ");
            if (newDate.HasValue)
                commande.DateCommande = newDate.Value;

            // sauvegaede en bdd
            commandeDao.Update(commande);
            Console.WriteLine("Modifications sauvegardées !");
        }

        public void SupprimerCommande()
        {
            int id = InputHelper.AskInt("Saisir l'ID de la commande à supprimer : ");

            bool isDeleted = commandeDao.Delete(id);

            if (isDeleted)
                Console.WriteLine("Commande supprimée avec succès !");
            else
                Console.WriteLine("Erreur lors de la suppression ou commande introuvable.");
        }

    }
}

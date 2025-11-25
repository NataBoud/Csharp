using ExHeritageCompteBancaire.Classes;
using System;
using System.Collections.Generic;
using System.Text;

using ExHeritageCompteBancaire.Enums;

namespace ExHeritageCompteBancaire
{
    internal interface IHM
    {

        private static List<Client> _clients = new List<Client>();

        public static void Start()
        {
            while (true)
            {
                AfficherMenu();
                string choix = Console.ReadLine()!;

                switch (choix)
                {
                    case "1":
                        CreerClientEtCompte();
                        break;
                    case "2":
                        AfficherClients();
                        break;
                    case "3":
                        GererCompte();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Erreur de saisie !");
                        break;
                }
            }
        }

        private static void AfficherMenu()
        {
            Console.WriteLine("\n=== Gestion des comptes bancaires ===");
            Console.WriteLine("1-- Créer un client et un compte");
            Console.WriteLine("2-- Afficher les clients et leurs comptes");
            Console.WriteLine("3-- Gérer un compte (dépôt/retrait/historique)");
            Console.WriteLine("0-- Quitter");
            Console.Write("Votre choix : ");
        }

        private static void CreerClientEtCompte()
        {
            Client? client = null;

            if (_clients.Count > 0)
            {
                Console.WriteLine("\nSouhaitez-vous créer un compte pour un client existant ?");
                Console.WriteLine("1 - Oui");
                Console.WriteLine("2 - Non, créer un nouveau client");
                Console.Write("Votre choix : ");
                string choixClient = Console.ReadLine()!;

                if (choixClient == "1")
                {
                    // Affichage liste des clients
                    Console.WriteLine("\nListe des clients existants :");
                    for (int i = 0; i < _clients.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {_clients[i].Nom} {_clients[i].Prenom}");
                    }
                    Console.Write("Sélectionnez un client par numéro : ");
                    if (int.TryParse(Console.ReadLine(), out int indexClient) &&
                        indexClient >= 1 && indexClient <= _clients.Count)
                    {
                        client = _clients[indexClient - 1];
                        Console.WriteLine($"\nVous allez ajouter un compte pour {client.Nom} {client.Prenom}");
                    }
                    else
                    {
                        Console.WriteLine("Client invalide !");
                        return;
                    }
                }
            }

            // Si pas de client choisi, création d'un nouveau
            if (client == null)
            {
                Console.Write("Nom du client : ");
                string nom = Console.ReadLine()!;
                Console.Write("Prénom : ");
                string prenom = Console.ReadLine()!;
                Console.Write("Identifiant : ");
                string identifiant = Console.ReadLine()!;
                Console.Write("Téléphone : ");
                string tel = Console.ReadLine()!;

                client = new Client(nom, prenom, identifiant, tel);
                _clients.Add(client);
                Console.WriteLine($"\nNouveau client créé : {client.Nom} {client.Prenom} !");
            }

            // Création du compte
            Console.WriteLine("\nQuel type de compte créer ?");
            Console.WriteLine("1-- Compte courant");
            Console.WriteLine("2-- Compte épargne");
            Console.WriteLine("3-- Compte payant");
            Console.Write("Votre choix : ");
            string typeCompte = Console.ReadLine()!;

            double solde = 0;
            Console.Write("Solde initial (€) : ");
            while (!double.TryParse(Console.ReadLine(), out solde) || solde < 0)
            {
                Console.Write("Erreur ! Entrez un nombre positif : ");
            }

            CompteBancaire? compte = typeCompte switch
            {
                "1" => new CompteCourant(client, solde),
                "2" => new CompteEpargne(client, solde),
                "3" => new ComptePayant(client, solde),
                _ => null
            };

            if (compte != null)
            {
                client.AjouterCompte(compte);
                Console.WriteLine($"\nCompte créé pour {client.Nom} {client.Prenom} !");
            }
            else
            {
                Console.WriteLine("Type de compte invalide ! Le compte n'a pas été créé.");
            }
        }


        private static void AfficherClients()
        {
            if (_clients.Count == 0)
            {
                Console.WriteLine("Aucun client enregistré !");
                return;
            }

            foreach (var client in _clients)
            {
                Console.WriteLine($"\n{client}");
                if (client.Comptes.Count == 0)
                {
                    Console.WriteLine("  Aucun compte.");
                    continue;
                }

                foreach (var compte in client.Comptes)
                {
                    // Traduction du type de compte en texte lisible
                    string typeCompteLisible = compte.GetType().Name switch
                    {
                        "CompteCourant" => "compte courant",
                        "CompteEpargne" => "compte épargne",
                        "ComptePayant" => "compte payant",
                        _ => "type inconnu"
                    };

                    Console.WriteLine($"  {typeCompteLisible} | Solde : {compte.Solde}€");
                }
            }
        }

        private static void GererCompte()
        {
            if (_clients.Count == 0)
            {
                Console.WriteLine("Aucun client pour gérer un compte !");
                return;
            }

            Console.WriteLine("\nListe des clients :");
            for (int i = 0; i < _clients.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_clients[i].Nom} {_clients[i].Prenom}");
            }

            Console.Write("Sélectionnez un client par numéro : ");
            if (!int.TryParse(Console.ReadLine(), out int indexClient) ||
                indexClient < 1 || indexClient > _clients.Count)
            {
                Console.WriteLine("Client invalide !");
                return;
            }

            Client client = _clients[indexClient - 1];
            if (client.Comptes.Count == 0)
            {
                Console.WriteLine("Ce client n'a aucun compte !");
                return;
            }

            Console.WriteLine("\nListe des comptes du client :");
            for (int i = 0; i < client.Comptes.Count; i++)
            {
                string typeCompteLisible = client.Comptes[i].GetType().Name switch
                {
                    "CompteCourant" => "compte courant",
                    "CompteEpargne" => "compte épargne",
                    "ComptePayant" => "compte payant",
                    _ => "type inconnu"
                };
                Console.WriteLine($"{i + 1} - {typeCompteLisible} | Solde : {client.Comptes[i].Solde}€");
            }

            Console.Write("Sélectionnez un compte par numéro : ");
            if (!int.TryParse(Console.ReadLine(), out int indexCompte) ||
                indexCompte < 1 || indexCompte > client.Comptes.Count)
            {
                Console.WriteLine("Compte invalide !");
                return;
            }

            CompteBancaire compteSelectionne = client.Comptes[indexCompte - 1];

            while (true)
            {
                Console.WriteLine($"\nGestion du compte ({compteSelectionne.GetType().Name}) | Solde : {compteSelectionne.Solde}€");
                Console.WriteLine("1 - Déposer de l'argent");
                Console.WriteLine("2 - Retirer de l'argent");
                Console.WriteLine("3 - Afficher l'historique des opérations");
                Console.WriteLine("0 - Retour");
                Console.Write("Votre choix : ");
                string action = Console.ReadLine()!;

                switch (action)
                {
                    case "1":
                        Console.Write("Montant à déposer : ");
                        if (double.TryParse(Console.ReadLine(), out double depot))
                        {
                            compteSelectionne.Depot(depot);
                            Console.WriteLine("Dépôt effectué !");
                        }
                        else
                        {
                            Console.WriteLine("Montant invalide !");
                        }
                        break;
                    case "2":
                        Console.Write("Montant à retirer : ");
                        if (double.TryParse(Console.ReadLine(), out double retrait))
                        {
                            if (compteSelectionne.Retrait(retrait))
                                Console.WriteLine("Retrait effectué !");
                        }
                        else
                        {
                            Console.WriteLine("Montant invalide !");
                        }
                        break;
                    case "3":
                        compteSelectionne.AfficherHistorique();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Erreur de saisie !");
                        break;
                }
            }
        }

    }
}

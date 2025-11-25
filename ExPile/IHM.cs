using ExPile.Classes;
using System;

namespace ExPile
{
    internal class IHM
    {
        private readonly Pile<string> pileString = new();
        private readonly Pile<decimal> pileDecimal = new();
        private readonly Pile<Personne> pilePersonne = new();

        public void Demarrer()
        {
            while (true)
            {
                Console.WriteLine("\n=== MENU PRINCIPAL ===");
                Console.WriteLine("1 - Pile de STRING");
                Console.WriteLine("2 - Pile de DECIMAL");
                Console.WriteLine("3 - Pile de PERSONNE");
                Console.WriteLine("0 - Quitter");
                Console.Write("Choix : ");
                string choix = Console.ReadLine()!;

                switch (choix)
                {
                    case "1": MenuPileString(); break;
                    case "2": MenuPileDecimal(); break;
                    case "3": MenuPilePersonne(); break;
                    case "0": return;
                    default: Console.WriteLine("Choix invalide !"); break;
                }
            }
        }

        // ---------------------- MENU STRING ----------------------

        private void MenuPileString()
        {
            while (true)
            {
                Console.WriteLine("\n=== PILE DE STRING ===");
                Console.WriteLine("1 - Empiler");
                Console.WriteLine("2 - Dépiler");
                Console.WriteLine("3 - Retirer par index");
                Console.WriteLine("4 - Afficher");
                Console.WriteLine("0 - Retour");
                Console.Write("Choix : ");
                string choix = Console.ReadLine()!;

                switch (choix)
                {
                    case "1":
                        Console.Write("Valeur : ");
                        pileString.Empiler(Console.ReadLine()!);
                        break;

                    case "2":
                        Console.WriteLine($"Retiré : {pileString.Depiler()}");
                        break;

                    case "3":
                        Console.Write("Index à retirer : ");
                        int index = int.Parse(Console.ReadLine()!);
                        Console.WriteLine($"Retiré : {pileString.RetirerParIndex(index)}");
                        break;

                    case "4": pileString.Afficher(); break;
                    case "0": return;
                    default: Console.WriteLine("Erreur !"); break;
                }
            }
        }

        // ---------------------- MENU DECIMAL ----------------------

        private void MenuPileDecimal()
        {
            while (true)
            {
                Console.WriteLine("\n=== PILE DE DECIMAL ===");
                Console.WriteLine("1 - Empiler");
                Console.WriteLine("2 - Dépiler");
                Console.WriteLine("3 - Retirer par index");
                Console.WriteLine("4 - Afficher");
                Console.WriteLine("0 - Retour");
                Console.Write("Choix : ");
                string choix = Console.ReadLine()!;

                switch (choix)
                {
                    case "1":
                        Console.Write("Valeur : ");
                        decimal val = decimal.Parse(Console.ReadLine()!);
                        pileDecimal.Empiler(val);
                        break;

                    case "2":
                        Console.WriteLine($"Retiré : {pileDecimal.Depiler()}");
                        break;

                    case "3":
                        Console.Write("Index : ");
                        int index = int.Parse(Console.ReadLine()!);
                        Console.WriteLine($"Retiré : {pileDecimal.RetirerParIndex(index)}");
                        break;

                    case "4": pileDecimal.Afficher(); break;
                    case "0": return;
                    default: Console.WriteLine("Erreur !"); break;
                }
            }
        }

        // ---------------------- MENU PERSONNE ----------------------

        private void MenuPilePersonne()
        {
            while (true)
            {
                Console.WriteLine("\n=== PILE DE PERSONNES ===");
                Console.WriteLine("1 - Empiler personne");
                Console.WriteLine("2 - Dépiler");
                Console.WriteLine("3 - Retirer par index");
                Console.WriteLine("4 - Afficher");
                Console.WriteLine("0 - Retour");
                Console.Write("Choix : ");
                string choix = Console.ReadLine()!;

                switch (choix)
                {
                    case "1":
                        pilePersonne.Empiler(Personne.SaisiePersonne());
                        break;

                    case "2":
                        Console.WriteLine($"Retiré : {pilePersonne.Depiler()}");
                        break;

                    case "3":
                        Console.Write("Index : ");
                        int index = int.Parse(Console.ReadLine()!);
                        Console.WriteLine($"Retiré : {pilePersonne.RetirerParIndex(index)}");
                        break;

                    case "4": pilePersonne.Afficher(); break;
                    case "0": return;
                    default: Console.WriteLine("Erreur !"); break;
                }
            }
        }

   
    }
}

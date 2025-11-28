using ExoLibrary.Helpers;
using ExoLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.IHM
{
    internal class BorrowMenu
    {
        private readonly BorrowService borrowService;

        public BorrowMenu(BorrowService service)
        {
            borrowService = service;
        }

        public void Show()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("   ******************************************");
                Console.WriteLine();
                Console.WriteLine("             Gestion des emprunts         ");
                Console.WriteLine();
                Console.WriteLine("   ******************************************");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("    1. Emprunter un livre");
                Console.WriteLine();
                Console.WriteLine("    2. Retourner un livre");
                Console.WriteLine();
                Console.WriteLine("    3. Lister les emprunts en cours");
                Console.WriteLine();
                Console.WriteLine("    4. Lister tous les emprunts");
                Console.WriteLine();
                Console.WriteLine("    0. Retour");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                int choice = InputHelper.AskInt("\n    Choix : ");
                Console.ResetColor();
                Console.WriteLine();

                switch (choice)
                {
                    case 1: borrowService.BorrowBook(); break;
                    case 2: borrowService.ReturnBook(); break;
                    case 3: borrowService.ListCurrentBorrows(); break;
                    case 4: borrowService.ListBorrows(); break;
                    case 0: back = true; continue;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("    Choix invalide !");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine("\n    -----------------------------------");
                Console.WriteLine("    Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }
    }


}

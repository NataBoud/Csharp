using ExoLibrary.Helpers;
using ExoLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.IHM
{
    internal class IhmService
    {

        private readonly BookMenu bookMenu;
        private readonly MemberMenu memberMenu;
        private readonly BorrowMenu borrowMenu;

        public IhmService(BookService bs, MemberSevice ms, BorrowService borS)
        {
            bookMenu = new BookMenu(bs);
            memberMenu = new MemberMenu(ms);
            borrowMenu = new BorrowMenu(borS);
        }

        private void ShowBooksMenu() => bookMenu.Show();
        private void ShowMembersMenu() => memberMenu.Show();
        private void ShowBorrowsMenu() => borrowMenu.Show();

        public void Start()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear(); // Efface la console pour chaque affichage de menu

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"
      _     _ _                          
     | |   (_) |__  _ __ __ _ _ __ _   _ 
     | |   | | '_ \| '__/ _` | '__| | | |
     | |___| | |_) | | | (_| | |  | |_| |
     |_____|_|_.__/|_|  \__,_|_|   \__, |
                                   |___/  
");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("   ******************************************");
                Console.WriteLine();
                Console.WriteLine("             Bibliothèque Municipale         ");
                Console.WriteLine();
                Console.WriteLine("   ******************************************");
                Console.WriteLine();
                Console.WriteLine("             1. Gérer les livres             ");
                Console.WriteLine();
                Console.WriteLine("             2. Gérer les membres            ");
                Console.WriteLine();
                Console.WriteLine("             3. Gérer les emprunts           ");
                Console.WriteLine();
                Console.WriteLine("             0. Quitter                      ");
                Console.WriteLine();
                Console.WriteLine("   ******************************************");
                Console.ResetColor(); 

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                int choice = InputHelper.AskInt("   Sélectionnez une option : ");
                Console.ResetColor();

                Console.WriteLine(); 

                switch (choice)
                {
                    case 1: ShowBooksMenu(); break;
                    case 2: ShowMembersMenu(); break;
                    case 3: ShowBorrowsMenu(); break;
                    case 0:
                        exit = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Merci d'avoir utilisé la bibliothèque !");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Choix invalide ! Veuillez réessayer.");
                        Console.ResetColor();
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey();
                }
            }

        }
    }
}

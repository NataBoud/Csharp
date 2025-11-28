using ExoLibrary.Helpers;
using ExoLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.IHM
{
    internal class BookMenu
    {
        private readonly BookService bookService;

        public BookMenu(BookService service)
        {
            bookService = service;
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
                Console.WriteLine("             Gestion des livres         ");
                Console.WriteLine();
                Console.WriteLine("   ******************************************");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("    1. Lister les livres");
                Console.WriteLine();
                Console.WriteLine("    2. Ajouter un livre");
                Console.WriteLine();
                Console.WriteLine("    3. Modifier un livre");
                Console.WriteLine();
                Console.WriteLine("    4. Supprimer un livre");
                Console.WriteLine();
                Console.WriteLine("    5. Rechercher par auteur");
                Console.WriteLine();
                Console.WriteLine("    0. Retour");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                int choice = InputHelper.AskInt("\n    Choix : ");
                Console.ResetColor();
                Console.WriteLine();

                switch (choice)
                {
                    case 1: bookService.ListBooks(); break;
                    case 2: bookService.CreateBook(); break;
                    case 3: bookService.UpdateBook(); break;
                    case 4: bookService.DeleteBook(); break;
                    case 5: bookService.SearchByAuthor(); break;
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
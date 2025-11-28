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
            Console.WriteLine("\n--- Gestion des livres ---");
            Console.WriteLine("1. Lister les livres");
            Console.WriteLine("2. Ajouter un livre");
            Console.WriteLine("3. Modifier un livre");
            Console.WriteLine("4. Supprimer un livre");
            Console.WriteLine("5. Rechercher par auteur");
            Console.WriteLine("0. Retour");

            int choice = InputHelper.AskInt("Choix : ");

            switch (choice)
            {
                case 1: bookService.ListBooks(); break;
                case 2: bookService.CreateBook(); break;
                case 3: bookService.UpdateBook(); break;
                case 4: bookService.DeleteBook(); break;
                case 5: bookService.SearchByAuthor(); break;
                case 0: return;
                default: Console.WriteLine("Choix invalide !"); break;
            }

        }
    }
}
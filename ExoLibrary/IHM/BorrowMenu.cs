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
            Console.WriteLine("\n--- Gestion des emprunts ---");
            Console.WriteLine("1. Emprunter un livre");
            Console.WriteLine("2. Retourner un livre");
            Console.WriteLine("3. Lister les emprunts en cours");
            Console.WriteLine("0. Retour");
            int choice = InputHelper.AskInt("Choix : ");
            switch (choice)
            {
                case 1: borrowService.BorrowBook(); break;
                case 2: borrowService.ReturnBook(); break;
                case 3: borrowService.ListCurrentBorrows(); break;
                case 0: return;
                default: Console.WriteLine("Choix invalide !"); break;
            }
        }
    }
}

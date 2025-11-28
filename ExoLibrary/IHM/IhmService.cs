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
                Console.WriteLine("\n===== Bibliothèque Municipale =====");
                Console.WriteLine("1. Gérer les livres");
                Console.WriteLine("2. Gérer les membres");
                Console.WriteLine("3. Gérer les emprunts");
                Console.WriteLine("0. Quitter");

                int choice = InputHelper.AskInt("Choix : ");

                switch (choice)
                {
                    case 1: ShowBooksMenu(); break;
                    case 2: ShowMembersMenu(); break;
                    case 3: ShowBorrowsMenu(); break;
                    case 0: exit = true; break;
                    default: Console.WriteLine("Choix invalide !"); break;
                }
            }
        }

    }
}

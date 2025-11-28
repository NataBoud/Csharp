using ExoLibrary.Services;
using ExoLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.IHM
{
    internal class MemberMenu
    {
        private readonly MemberSevice memberService;

        public MemberMenu(MemberSevice service)
        {
            memberService = service;
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
                Console.WriteLine("             Gestion des membres         ");
                Console.WriteLine();
                Console.WriteLine("   ******************************************");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("    1. Lister les membres");
                Console.WriteLine();
                Console.WriteLine("    2. Ajouter un membre");
                Console.WriteLine();
                Console.WriteLine("    3. Supprimer un membre");
                Console.WriteLine();
                Console.WriteLine("    4. Rechercher par email");
                Console.WriteLine();
                Console.WriteLine("    0. Retour");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                int choice = InputHelper.AskInt("\n    Choix : ");
                Console.ResetColor();
                Console.WriteLine();

                switch (choice)
                {
                    case 1: memberService.ListMembers(); break;
                    case 2: memberService.CreateMember(); break;
                    case 3: memberService.DeleteMember(); break;
                    case 4: memberService.SearchMemberByEmail(); break;
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

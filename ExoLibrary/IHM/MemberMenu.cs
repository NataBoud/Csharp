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
            Console.WriteLine("\n--- Gestion des membres ---");
            Console.WriteLine("1. Lister les membres");
            Console.WriteLine("2. Ajouter un membre");
            Console.WriteLine("3. Supprimer un membre");
            Console.WriteLine("4. Rechercher par email");
            Console.WriteLine("0. Retour");
            int choice = InputHelper.AskInt("Choix : ");
            switch (choice)
            {
                case 1: memberService.ListMembers(); break;
                case 2: memberService.CreateMember(); break;
                case 3: memberService.DeleteMember(); break;
                case 4: memberService.SearchMemberByEmail(); break;
                case 0: return;
                default: Console.WriteLine("Choix invalide !"); break;
            }
        }
    }
}

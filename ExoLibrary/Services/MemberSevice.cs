using ExoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ExoLibrary.Classes;
using ExoLibrary.Dao;
using ExoLibrary.Helpers;

namespace ExoLibrary.Services
{
    internal class MemberSevice : IMemberService
    {
        private readonly MemberDao memberDao;

        public MemberSevice(MemberDao dao)
        {
            memberDao = dao;
        }
        public void CreateMember()
        {
            string lastName = InputHelper.AskString("Nom : ");
            string firstName = InputHelper.AskString("Prénom : ");
            string email = InputHelper.AskString("Email : ");
            DateTime registrationDate = DateTime.Now; // Date actuelle par défaut

            Member member = new Member(lastName, firstName, email, registrationDate);
            memberDao.Save(member);

            Console.WriteLine($"Membre {member.FirstName} {member.LastName} créé avec l'id {member.Id} !");
        }

        public void DeleteMember()
        {
            int id = InputHelper.AskInt("Id du membre à supprimer : ");
            if (memberDao.Delete(id))
                Console.WriteLine("Membre supprimé !");
            else
                Console.WriteLine("Erreur lors de la suppression ou membre introuvable !");
        }

        public void ListMembers()
        {
            var members = memberDao.GetAll();
            if (members.Count == 0)
            {
                Console.WriteLine("Aucun membre trouvé.");
                return;
            }

            int index = 1;
            foreach (var member in members)
            {
                Console.WriteLine($"        ........... Membre {index++} ...........");
                Console.WriteLine(member); // <-- appelle automatiquement member.ToString()
            }
        }

        public void SearchMemberByEmail()
        {
            string email = InputHelper.AskString("Email à rechercher : ");
            var member = memberDao.GetAll().FirstOrDefault(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (member == null)
                Console.WriteLine("Aucun membre trouvé avec cet email.");
            else
                Console.WriteLine(member);
        }

        public void UpdateMember()
        {
            int id = InputHelper.AskInt("Id du membre à modifier : ");
            Member? member = memberDao.GetOneById(id);

            if (member == null)
            {
                Console.WriteLine("Membre introuvable !");
                return;
            }

            string lastName = InputHelper.AskOptionalString($"Nom ({member.LastName}) : ", member.LastName)!;
            string firstName = InputHelper.AskOptionalString($"Prénom ({member.FirstName}) : ", member.FirstName)!;
            string email = InputHelper.AskOptionalString($"Email ({member.Email}) : ", member.Email)!;

            member.LastName = lastName;
            member.FirstName = firstName;
            member.Email = email;

            memberDao.Update(member);
            Console.WriteLine("Membre modifié !");
        }
    }
}

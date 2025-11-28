using ExoLibrary.Classes;
using ExoLibrary.Dao;
using ExoLibrary.Helpers;
using ExoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Services
{
    internal class BorrowService : IBorrowService
    {

        private readonly BorrowDao borrowDao;
        private readonly BookDao bookDao;
        private readonly MemberDao memberDao;

        public BorrowService(BorrowDao bDao, BookDao boDao, MemberDao mDao)
        {
            borrowDao = bDao;
            bookDao = boDao;
            memberDao = mDao;
        }

        /// <summary>
        /// Permet à un membre d'emprunter un livre disponible.
        /// Met à jour l'état du livre pour qu'il ne soit plus disponible.
        /// </summary>
        public void BorrowBook()
        {
            var members = memberDao.GetAll();
            if (members.Count == 0)
            {
                Console.WriteLine("Aucun membre disponible !");
                return;
            }

            Console.WriteLine("Sélectionnez le membre :");
            members.ForEach(m => Console.WriteLine($"{m.Id}. {m.FirstName} {m.LastName}"));

            int memberId = InputHelper.AskInt("Id du membre : ");
            Member? member = memberDao.GetOneById(memberId);
            if (member == null)
            {
                Console.WriteLine("Membre introuvable !");
                return;
            }

            var availableBooks = bookDao.GetAll().Where(b => b.IsAvailable).ToList();
            if (availableBooks.Count == 0)
            {
                Console.WriteLine("Aucun livre disponible !");
                return;
            }

            Console.WriteLine("Sélectionnez le livre à emprunter :");
            availableBooks.ForEach(b => Console.WriteLine($"{b.Id}. {b.Title} by {b.Author}"));

            int bookId = InputHelper.AskInt("Id du livre : ");
            Book? book = bookDao.GetOneById(bookId);
            if (book == null || !book.IsAvailable)
            {
                Console.WriteLine("Livre non disponible !");
                return;
            }

            Borrow borrow = new Borrow(bookId, memberId, DateTime.Now);
            borrowDao.Save(borrow);

            // Marquer le livre comme non disponible
            book.IsAvailable = false;
            bookDao.Update(book);

            Console.WriteLine($"Le livre '{book.Title}' a été emprunté par {member.FirstName} {member.LastName}.");
        }

        /// <summary>
        /// Affiche tous les emprunts, y compris ceux déjà retournés.
        /// </summary>
        public void ListBorrows()
        {
            var borrows = borrowDao.GetAll();
            if (borrows.Count == 0)
            {
                Console.WriteLine("Aucun emprunt trouvé.");
                return;
            }

            int index = 1;
            foreach (var borrow in borrows)
            {
                // Remplir les propriétés pour l'affichage
                var book = bookDao.GetOneById(borrow.BookId);
                var member = memberDao.GetOneById(borrow.MemberId);

                borrow.BookTitle = book?.Title;
                borrow.MemberName = member != null ? $"{member.FirstName} {member.LastName}" : "Inconnu";

                Console.WriteLine($"        .......... Emprunt {index++} ..........");
                Console.WriteLine(borrow);  // utilise le ToString() amélioré de Borrow
            }
        }

        /// <summary>
        /// Affiche uniquement les emprunts en cours (non retournés).
        /// </summary>
        public void ListCurrentBorrows()
        {
            var borrows = borrowDao.GetAll().Where(b => b.ReturnDate == null).ToList();
            if (borrows.Count == 0)
            {
                Console.WriteLine("Aucun emprunt en cours.");
                return;
            }

            int index = 1;
            foreach (var borrow in borrows)
            {
                var book = bookDao.GetOneById(borrow.BookId);
                var member = memberDao.GetOneById(borrow.MemberId);

                borrow.BookTitle = book?.Title;
                borrow.MemberName = member != null ? $"{member.FirstName} {member.LastName}" : "Inconnu";

                Console.WriteLine($"        .......... Emprunt en cours {index++} ..........");
                Console.WriteLine(borrow);
            }
        }

        /// <summary>
        /// Permet de retourner un livre emprunté.
        /// Met à jour la date de retour et rend le livre disponible.
        /// </summary>
        public void ReturnBook()
        {
            var currentBorrows = borrowDao.GetAll().Where(b => b.ReturnDate == null).ToList();
            if (currentBorrows.Count == 0)
            {
                Console.WriteLine("Aucun emprunt en cours !");
                return;
            }

            Console.WriteLine("Sélectionnez l'emprunt à retourner :");
            foreach (var b in currentBorrows)
            {
                var member = memberDao.GetOneById(b.MemberId);
                var book = bookDao.GetOneById(b.BookId);
                Console.WriteLine($"{b.Id}. {book?.Title} emprunté par {member?.FirstName} {member?.LastName}");
            }

            int borrowId = InputHelper.AskInt("Id de l'emprunt : ");
            Borrow? borrowToReturn = borrowDao.GetOneById(borrowId);

            if (borrowToReturn == null || borrowToReturn.ReturnDate != null)
            {
                Console.WriteLine("Emprunt invalide !");
                return;
            }

            borrowToReturn.ReturnDate = DateTime.Now;
            borrowDao.Update(borrowToReturn);

            // Remettre le livre disponible
            var returnedBook = bookDao.GetOneById(borrowToReturn.BookId);
            if (returnedBook != null)
            {
                returnedBook.IsAvailable = true;
                bookDao.Update(returnedBook);
            }

            Console.WriteLine("Livre retourné avec succès !");
        }
    }
}

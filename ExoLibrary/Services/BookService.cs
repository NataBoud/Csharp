using ExoLibrary.Classes;
using ExoLibrary.Dao;
using ExoLibrary.Helpers;
using ExoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Services
{
    internal class BookService : IBookService
    {
        private readonly BookDao bookDao;

        public BookService(BookDao dao)
        {
            bookDao = dao;
        }

        public void CreateBook()
        {
            string title = InputHelper.AskString("Titre : ");
            string author = InputHelper.AskString("Auteur : ");
            string isbn = InputHelper.AskString("ISBN : ");
            // Demande la date complète au format jj/MM/yyyy
            DateTime publicationDate = InputHelper.AskDate("Année de publication (jj/MM/yyyy) : ");

            Book book = new Book(title, author, isbn, publicationDate.Year); 
            bookDao.Save(book);

            Console.WriteLine($"Livre '{book.Title}' créé avec l'id {book.Id} !");
        }

        public void DeleteBook()
        {
            int id = InputHelper.AskInt("Id du livre à supprimer : ");
            if (bookDao.Delete(id))
                Console.WriteLine("Livre supprimé !");
            else
                Console.WriteLine("Erreur lors de la suppression ou livre introuvable !");
        }

        public void ListBooks()
        {
            var books = bookDao.GetAll();

            if (books.Count == 0)
            {
                Console.WriteLine("Aucun livre trouvé.");
                return;
            }

            int index = 1;
            foreach (var book in books)
            {
                Console.WriteLine($"{index++}. {book.Title} by {book.Author} (ISBN: {book.ISBN}) - Disponible : {book.IsAvailable}");
            }
        }

        public void SearchByAuthor()
        {
            string author = InputHelper.AskString("Auteur à rechercher : ");
            var books = bookDao.GetAll().FindAll(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

            if (books.Count == 0)
                Console.WriteLine("Aucun livre trouvé pour cet auteur.");
            else
                books.ForEach(Console.WriteLine);
        }

        public void UpdateBook()
        {
            int id = InputHelper.AskInt("Id du livre à modifier : ");
            Book? book = bookDao.GetOneById(id);

            if (book == null)
            {
                Console.WriteLine("Livre introuvable !");
                return;
            }

            string title = InputHelper.AskOptionalString($"Titre ({book.Title}) : ", book.Title)!;
            string author = InputHelper.AskOptionalString($"Auteur ({book.Author}) : ", book.Author)!;
            string isbn = InputHelper.AskOptionalString($"ISBN ({book.ISBN}) : ", book.ISBN)!;
            int year = InputHelper.AskInt($"Année de publication ({book.PublicationYear}) : ");

            bool isAvailable = InputHelper.AskOptionalString($"Disponible (oui/non) ({book.IsAvailable}) : ", book.IsAvailable.ToString())!.ToLower() == "oui";

            book.Title = title;
            book.Author = author;
            book.ISBN = isbn;
            book.PublicationYear = year;
            book.IsAvailable = isAvailable;

            bookDao.Update(book);
            Console.WriteLine("Livre modifié !");
        }
    }
}

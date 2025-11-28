using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Classes
{
    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }      
        public DateTime? UpdatedAt { get; set; }

       
        public Book(string title, string author, string isbn, int publicationYear)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            IsAvailable = true;
            CreatedAt = DateTime.Now;  // Initialisation automatique
            UpdatedAt = null;
        }

        public Book(int id, string title, string author, string isbn, int publicationYear, bool isAvailable, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            IsAvailable = isAvailable;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public override string ToString()
        {
            return $"{Title} par {Author}, ISBN : {ISBN}, Publié en {PublicationYear}, Disponible : {IsAvailable}, Créé le : {CreatedAt}, Modifié le : {UpdatedAt}";
        }

    }
}

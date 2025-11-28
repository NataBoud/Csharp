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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"        Titre       : {Title}");
            sb.AppendLine($"        Auteur      : {Author}");
            sb.AppendLine($"        ISBN        : {ISBN}");
            sb.AppendLine($"        Publié en   : {PublicationYear}");
            sb.AppendLine($"        Disponible  : {(IsAvailable ? "Oui" : "Non")}");
            sb.AppendLine($"        Créé le     : {CreatedAt:dd/MM/yyyy HH:mm}");

            if (UpdatedAt != null)
                sb.AppendLine($"        Modifié le  : {UpdatedAt:dd/MM/yyyy HH:mm}");

            return sb.ToString();
        }
    }
}

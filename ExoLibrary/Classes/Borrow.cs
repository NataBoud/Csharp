using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Classes
{
    internal class Borrow
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime CreatedAt { get; set; }     
        public DateTime? UpdatedAt { get; set; }

        // Propriétés pour affichage (non stockées en base)
        public string? BookTitle { get; set; }
        public string? MemberName { get; set; }

        public Borrow(int bookId, int memberId, DateTime borrowDate, DateTime? returnDate = null)
        {
            BookId = bookId;
            MemberId = memberId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
        }

        public Borrow(int id, int bookId, int memberId, DateTime borrowDate, DateTime? returnDate, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            BookId = bookId;
            MemberId = memberId;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"        Emprunt Id      : {Id}");
            sb.AppendLine($"        Livre           : {BookTitle ?? "Inconnu"}");
            sb.AppendLine($"        Membre          : {MemberName ?? "Inconnu"}");
            sb.AppendLine($"        Date d'emprunt  : {BorrowDate:dd/MM/yyyy}");
            sb.AppendLine($"        Date de retour  : {(ReturnDate.HasValue ? ReturnDate.Value.ToString("dd/MM/yyyy") : "En cours")}");
            sb.AppendLine($"        Créé le         : {CreatedAt:dd/MM/yyyy HH:mm}");
            if (UpdatedAt != null)
                sb.AppendLine($"        Modifié le      : {UpdatedAt:dd/MM/yyyy HH:mm}");
            return sb.ToString();
        }

    }
}

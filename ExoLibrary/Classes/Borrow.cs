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
            return $"Emprunt Id : {Id}, Livre Id : {BookId}, Membre Id : {MemberId}, Date d'emprunt : {BorrowDate}, Date de retour : {ReturnDate}, Créé le : {CreatedAt}, Modifié le : {UpdatedAt}";
        }

    }
}

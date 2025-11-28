using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Interfaces
{
    internal interface IBorrowService
    {
        void ListBorrows();          // Voir tous les emprunts
        void BorrowBook();           // Emprunter un livre
        void ReturnBook();           // Retourner un livre
        void ListCurrentBorrows();   // Voir les emprunts en cours (non retournés)
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Interfaces
{
    internal interface IBookService
    {
        void ListBooks();
        void CreateBook();
        void UpdateBook();
        void DeleteBook();
        void SearchByAuthor();
    }
}

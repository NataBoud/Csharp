using ExoLibrary.IHM;
using ExoLibrary.Services;
using ExoLibrary.Dao;
using System;

class Program
{
    static void Main()
    {
        // Créer les DAOs
        var bookDao = new BookDao();
        var memberDao = new MemberDao();
        var borrowDao = new BorrowDao();

        // Créer les Services
        var bookService = new BookService(bookDao);
        var memberService = new MemberSevice(memberDao);
        var borrowService = new BorrowService(borrowDao, bookDao, memberDao);

        // Créer l'IHM
        try
        {
            var ihm = new IhmService(bookService, memberService, borrowService);
            ihm.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur inattendue : " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Appuyez sur une touche pour quitter...");
            Console.ReadKey();
        }

    }
}


using System;
using Hotel.data;
using Hotel.models;
using Hotel.repository;

internal class Program
{
    private static void Main(string[] args)
    {
        using var db = new ApplicationDbContext();

        var clientRepo = new ClientRepository(db);
        var chambreRepo = new ChambreRepository(db);
        var reservationRepo = new ReservationRepository(db);

        //var client1 = new Client { Nom = "Doe", Prenom = "John", NumeroTelephone = "0601020304" };
        //var client2 = new Client { Nom = "Dupont", Prenom = "Marie", NumeroTelephone = "0605060708" };

        //clientRepo.Add(client1);
        //clientRepo.Add(client2);

        //var chambre1 = new Chambre { NbLit = 2, PrixParNuit = 50, Statut = StatutChambre.Libre };
        //var chambre2 = new Chambre { NbLit = 1, PrixParNuit = 35, Statut = StatutChambre.Libre };

        //chambreRepo.Add(chambre1);
        //chambreRepo.Add(chambre2);

        //var reservation1 = new Reservation(client1, chambre1, DateTime.Today, DateTime.Today.AddDays(3), StatutReservation.prevue);
        //var reservation2 = new Reservation(client2, chambre2, DateTime.Today.AddDays(1), DateTime.Today.AddDays(4), StatutReservation.prevue);

        //reservationRepo.Add(reservation1);
        //reservationRepo.Add(reservation2);


        //Console.WriteLine("\nClients :");
        //foreach (var c in clientRepo.GetAll())
        //{
        //    Console.WriteLine(c);
        //}

        //Console.WriteLine("\nChambres :");
        //foreach (var ch in chambreRepo.GetAll())
        //{
        //    Console.WriteLine(ch);
        //}

        //Console.WriteLine("\nRéservations :");
        //foreach (var r in reservationRepo.GetAll())
        //{
        //    Console.WriteLine(r);
        //}




    }
}

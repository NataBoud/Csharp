using Hotel.data;
using Hotel.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.repository
{
    internal class ReservationRepository : BaseRepository<Reservation, int>
    {
        public ReservationRepository(ApplicationDbContext db) : base(db)
        {
          
        }
        public override Reservation? GetById(int entityId)
        {
            return _db.Reservations.Find(entityId);
        }

        public override List<Reservation> GetAll()
        {
            return _db.Reservations.ToList();
        }

        public override Reservation Update(int id, Reservation entity)
        {
            Reservation reservation = GetById(id);

            if (reservation is null)
                return null;

            if (reservation.Statut != entity.Statut)
                reservation.Statut = entity.Statut;
            if (reservation.Client != entity.Client)
                reservation.Client = entity.Client;

            _db.SaveChanges();
            return reservation;
        }

        public override bool Delete(int entityId)
        {
            Reservation reservation = GetById(entityId);

            if (reservation is null)
                return false;

            reservation.Statut = StatutReservation.annulee;
            return _db.SaveChanges() == 1;
        }

    }
}

using Hotel.data;
using Hotel.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.repository
{
    internal class ReservationRepository : IRepository<Reservation, int>
    {
        private readonly ApplicationDbContext _db;

        public ReservationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Reservation? Add(Reservation entity)
        {
            EntityEntry<Reservation> reservationEntity = _db.Add(entity);
            _db.SaveChanges();
            return reservationEntity.Entity;
        }

        public bool Delete(int id)
        {
            var reservation = GetById(id);
            if (reservation == null) return false;
            _db.Remove(reservation);
            return _db.SaveChanges() == 1;
        }

        public Reservation? Get(Func<Reservation, bool> predicate)
        {
            return _db.Reservations.FirstOrDefault(predicate);
        }

        public List<Reservation> GetAll()
        {
            return _db.Reservations.ToList();
        }

        public List<Reservation> GetAll(Func<Reservation, bool> predicate)
        {
            return _db.Reservations.Where(predicate).ToList();
        }

        public Reservation? GetById(int id)
        {
            return _db.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public Reservation? Update(int id, Reservation entity)
        {
            var reservation = GetById(id);
            if (reservation == null) return null;

            reservation.Client = entity.Client;
            reservation.ClientId = entity.Client?.Id;

            reservation.Chambre = entity.Chambre;
            reservation.ChambreId = entity.Chambre?.Id;

            reservation.DateDebut = entity.DateDebut;
            reservation.DateFin = entity.DateFin;
            reservation.Statut = entity.Statut;

            _db.SaveChanges();
            return reservation;
        }
    }
}

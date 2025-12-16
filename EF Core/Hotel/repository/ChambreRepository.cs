using Hotel.data;
using Hotel.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.repository
{
    internal class ChambreRepository : BaseRepository<Chambre, int>
    {
        public ChambreRepository(ApplicationDbContext db) : base(db)
        {
        }

        public override Chambre? GetById(int entityId)
        {
            return _db.Chambres.Find(entityId);
        }

        public override List<Chambre> GetAll()
        {
            return _db.Chambres.ToList();
        }

        public override Chambre Update(int id, Chambre entity)
        {
            Chambre chambre = GetById(id);

            if (chambre is null)
                return null;

            if (chambre.Statut != entity.Statut)
                chambre.Statut = entity.Statut;
            if (chambre.NombreLits == entity.NombreLits)
                chambre.NombreLits = entity.NombreLits;
            if (chambre.Tarif != entity.Tarif)
                chambre.Tarif = entity.Tarif;

            _db.SaveChanges();
            return chambre;
        }

        public override bool Delete(int entityId)
        {
            Chambre chambre = GetById(entityId);

            if (chambre is null)
                return false;

            _db.Remove(chambre);
            return _db.SaveChanges() == 1;
        }

        public List<Chambre> getAllFreeRooms()
        {
            return _db.Chambres.Where(c => c.Statut == StatutChambre.Libre).ToList();
        }
    }
}

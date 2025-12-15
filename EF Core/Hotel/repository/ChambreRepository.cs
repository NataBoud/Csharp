using Hotel.data;
using Hotel.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.repository
{
    internal class ChambreRepository : IRepository<Chambre, int>
    {
        private readonly ApplicationDbContext _db;

        public ChambreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Chambre? Add(Chambre entity)
        {
            EntityEntry<Chambre> chambreEntity = _db.Add(entity);
            _db.SaveChanges();
            return chambreEntity.Entity;
        }

        public bool Delete(int id)
        {
            var chambre = GetById(id);
            if (chambre == null) return false;
            _db.Remove(chambre);
            return _db.SaveChanges() == 1;
        }

        public Chambre? Get(Func<Chambre, bool> predicate)
        {
            return _db.Chambres.FirstOrDefault(predicate);
        }

        public List<Chambre> GetAll()
        {
            return _db.Chambres.ToList();
        }

        public List<Chambre> GetAll(Func<Chambre, bool> predicate)
        {
            return _db.Chambres.Where(predicate).ToList();
        }

        public Chambre? GetById(int id)
        {
            return _db.Chambres.FirstOrDefault(c => c.Id == id);
        }

        public Chambre? Update(int id, Chambre entity)
        {
            var chambre = GetById(id);
            if (chambre == null) return null;

            chambre.NbLit = entity.NbLit;
            chambre.PrixParNuit = entity.PrixParNuit;
            chambre.Statut = entity.Statut;
            chambre.Client = entity.Client;
            chambre.ClientId = entity.Client?.Id;

            _db.SaveChanges();
            return chambre;
        }
    }
}

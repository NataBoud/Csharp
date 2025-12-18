using System;
using System.Collections.Generic;
using System.Linq;
using WasteMicroservice.Domain.Entity;
using WasteMicroservice.Domain.Ports;
using WasteMicroservice.Infrastructure.Data;

namespace WasteMicroservice.Infrastructure.Persistence
{
    public class WasteRepository : IWasteRepository
    {
        private readonly AppDbContext _db;

        public WasteRepository(AppDbContext db)
        {
            _db = db;
        }

        // Ajouter un nouveau déchet
        public Waste Add(Waste waste)
        {
            _db.Wastes.Add(waste);
            _db.SaveChanges();
            return waste;
        }

        // Récupérer tous les déchets
        public List<Waste> GetAll()
        {
            return _db.Wastes.ToList();
        }

        // Récupérer un déchet par son Id
        public Waste? GetById(Guid id)
        {
            return _db.Wastes.FirstOrDefault(w => w.Id == id);
        }

        // Mettre à jour un déchet existant
        public Waste? Update(Waste waste)
        {
            var existing = _db.Wastes.FirstOrDefault(w => w.Id == waste.Id);
            if (existing == null) return null;

            existing.Type = waste.Type;
            existing.QuantiteKg = waste.QuantiteKg;
            existing.TauxRecyclage = waste.TauxRecyclage;

            _db.SaveChanges();
            return existing;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TransportMicroservice.Domain.Entity;
using TransportMicroservice.Domain.Ports;
using TransportMicroservice.Infrastructure.Data;

namespace TransportMicroservice.Infrastructure.Persistence
{
    public class TransportRepository : ITransportRepository
    {
        private readonly AppDbContext _db;

        public TransportRepository(AppDbContext db)
        {
            _db = db;
        }

        public Transport Add(Transport transport)
        {
            _db.Transports.Add(transport);
            _db.SaveChanges();
            return transport;
        }

        public List<Transport> GetAll()
        {
            return _db.Transports.ToList();
        }

        public Transport? GetById(Guid id)
        {
            return _db.Transports.FirstOrDefault(t => t.Id == id);
        }

        public Transport? Update(Transport transport)
        {
            var existing = _db.Transports.FirstOrDefault(t => t.Id == transport.Id);
            if (existing == null) return null;

            existing.Mode = transport.Mode;
            existing.DistanceKm = transport.DistanceKm;
            existing.FacteurEmission = transport.FacteurEmission;

            _db.SaveChanges();
            return existing;
        }

        public bool Delete(Guid id)
        {
            var existing = _db.Transports.FirstOrDefault(t => t.Id == id);
            if (existing == null) return false;

            _db.Transports.Remove(existing);
            _db.SaveChanges();
            return true;
        }
    }
}

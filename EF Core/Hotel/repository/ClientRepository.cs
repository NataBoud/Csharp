using Hotel.data;
using Hotel.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.repository
{
    internal class ClientRepository : BaseRepository<Client, int>
    {
        public ClientRepository(ApplicationDbContext db) : base(db)
        {

        }

        public override Client? GetById(int entityId)
        {
            return _db.Clients.Find(entityId);
        }

        public override List<Client> GetAll()
        {
            return _db.Clients.ToList();
        }

        public override Client Update(int id, Client entity)
        {
            Client clientFound = GetById(id);

            if (clientFound is null)
                return null;

            if (clientFound.Nom != entity.Nom)
                clientFound.Nom = entity.Nom;
            if (clientFound.Prenom != entity.Prenom)
                clientFound.Prenom = entity.Prenom;
            if (clientFound.Telephone != entity.Telephone)
                clientFound.Telephone = entity.Telephone;

            _db.SaveChanges();
            return clientFound;
        }

        public override bool Delete(int entityId)
        {
            Client clientFound = GetById(entityId);

            if (clientFound is null)
                return false;

            _db.Remove(clientFound);
            return _db.SaveChanges() == 1;
        }


    }
}

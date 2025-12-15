using Contacts.data;
using Contacts.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Contacts.repository
{
    internal class ContactRepository : IRepository<Contact, int>
    {
        private readonly ApplicationDbContext _db;

        public ContactRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // Ajouter un nouveau contact
        public Contact? Add(Contact entity)
        {
            EntityEntry<Contact> contactEntity = _db.Add(entity);
            _db.SaveChanges();
            return contactEntity.Entity;
        }

        // Récupérer tous les contacts
        public List<Contact> GetAll()
        {
            return _db.Contact.ToList();
        }

        // Récupérer tous les contacts selon un filtre
        public List<Contact> GetAll(Func<Contact, bool> predicate)
        {
            return _db.Contact.Where(predicate).ToList();
        }

        // Récupérer un contact par Id
        public Contact? GetById(int id)
        {
            return _db.Contact.FirstOrDefault(c => c.Id == id);
        }

        // Récupérer un contact selon un critère
        public Contact? Get(Func<Contact, bool> predicate)
        {
            return _db.Contact.FirstOrDefault(predicate);
        }

        // Mettre à jour un contact
        public Contact? Update(int id, Contact entity)
        {
            var contact = GetById(id);

            if (contact is null)
                return null;

            if (contact.Nom != entity.Nom)
                contact.Nom = entity.Nom;
            if (contact.Prenom != entity.Prenom)
                contact.Prenom = entity.Prenom;
            if (contact.DateNaissance != entity.DateNaissance)
                contact.DateNaissance = entity.DateNaissance;
            if (contact.Genre != entity.Genre)
                contact.Genre = entity.Genre;
            if (contact.NumeroTelephone != entity.NumeroTelephone)
                contact.NumeroTelephone = entity.NumeroTelephone;
            if (contact.Email != entity.Email)
                contact.Email = entity.Email;

            _db.SaveChanges();

            return contact;
        }

        // Supprimer un contact
        public bool Delete(int id)
        {
            var contact = GetById(id);
            if (contact is null)
                return false;

            _db.Remove(contact);
            return _db.SaveChanges() == 1;
        }
    }
}

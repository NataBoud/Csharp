using Hotel.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.repository
{
    public abstract class BaseRepository<T, Tid>
    {
        protected ApplicationDbContext _db;

        protected BaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public T Add(T entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
            return entity;
        }
        abstract public T? GetById(Tid entityId);
        abstract public List<T> GetAll();
        abstract public T Update(Tid id, T entity);

        public abstract bool Delete(Tid entityId);

    }
}

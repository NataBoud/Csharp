using System;
using System.Collections.Generic;
using System.Text;

namespace ExoCommande.Dao
{
    internal abstract class BaseDao<T>
    {
        protected string request = "";
        public abstract T Save(T entity);
        public abstract T Update(T entity);
        public abstract List<T> GetAll();
        public abstract T? GetOneById(int id);
        public abstract bool Delete(int id);
    }
}

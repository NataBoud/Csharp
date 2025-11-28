using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Dao
{
    internal interface IBaseDao<T>
    {
        T Save(T entity);                
        T Update(T entity);          
        bool Delete(int id);             
        T? GetOneById(int id);          
        List<T> GetAll();               
    }
}

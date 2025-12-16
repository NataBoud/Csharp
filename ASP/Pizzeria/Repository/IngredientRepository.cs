using Pizzeria.Data;
using Pizzeria.Models;
using Microsoft.EntityFrameworkCore;

namespace Pizzeria.Repository
{
    public class IngredientRepository : IRepository<Ingredient>
    {
        private readonly AppDbContext _db;

        public IngredientRepository(AppDbContext db)
        {
            _db = db;
        }

        public Ingredient Get(int id)
        {
            return _db.Ingredients.Find(id)
                   ?? throw new KeyNotFoundException($"Ingredient {id} not found");
        }

        public List<Ingredient> GetAll()
        {
            return _db.Ingredients.ToList();
        }

        public bool Create(Ingredient entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Update(Ingredient entity)
        {
            var ingredientFound = Get(entity.Id);

            ingredientFound.Nom = entity.Nom;
            ingredientFound.Descriptif = entity.Descriptif;

            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var ingredientFound = Get(id);
            _db.Remove(ingredientFound);
            _db.SaveChanges();
            return true;
        }
    }
}

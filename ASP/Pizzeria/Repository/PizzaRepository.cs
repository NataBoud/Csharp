using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Repository
{
    public class PizzaRepository : IRepository<Pizza>
    {

        private readonly AppDbContext _db;

        public PizzaRepository(AppDbContext db)
        {
            _db = db;
        }
        public Pizza Get(int id)
        {
            var pizza = _db.Pizzas.Find(id);
            // L’opérateur ?? vérifie si c’est null
            return pizza ?? throw new KeyNotFoundException($"Pizza {id} not found");
        }

        public List<Pizza> GetAll()
        {
            return _db.Pizzas.ToList();
        }

        public bool Create(Pizza entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Update(Pizza entity)
        {
            var pizzaFound = Get(entity.Id); // lève KeyNotFoundException si pas trouvé

            // Mettre à jour les champs
            pizzaFound.Nom = entity.Nom;
            pizzaFound.Description = entity.Description;
            pizzaFound.Statut = entity.Statut;
            pizzaFound.Ingredients = entity.Ingredients;

            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var pizzaFound = Get(id);
            _db.Remove(pizzaFound);
            _db.SaveChanges();
            return true;
        }
    }
}

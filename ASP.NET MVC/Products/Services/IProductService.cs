using Products.Models;

namespace Products.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Remove(int id);
        void AddRandom();
    }
}

using ProductService.Data;
using ProductService.Models;

namespace ProductService.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get product by id
        public Product GetById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        // Get all products
        public List<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }


        // Create a new product
        public Product Create(Product entity)
        {
            _dbContext.Products.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        // Update an existing product
        public Product Update(Product entity)
        {
            Product product = GetById(entity.Id);
            if (product == null)
                return null;

            // Mettre à jour les propriétés
            product.Name = entity.Name;
            product.Price = entity.Price;
            product.Quantity = entity.Quantity;

            _dbContext.SaveChanges();
            return product;
        }


        // Delete a product by id
        public bool Delete(int id)
        {
            Product product = GetById(id);
            if (product == null) return false;

            _dbContext.Remove(product);
            _dbContext.SaveChanges();
            return true;
        }
    }
}

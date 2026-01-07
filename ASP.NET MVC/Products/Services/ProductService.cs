using Products.Models;

namespace Products.Services
{
    public class ProductService : IProductService
    {
        private readonly static List<Product> _products =
        [
            new Product { Id = 1, Name = "Clavier", Price = 49.99m, Stock = 10, IsOnDiscount = false },
            new Product { Id = 2, Name = "Souris", Price = 29.99m, Stock = 20, IsOnDiscount = true },
            new Product { Id = 3, Name = "Casque Audio", Price = 149.99m, Stock = 50, IsOnDiscount = false },
            new Product { Id = 4, Name = "Clavier Mécanique", Price = 89.99m, Stock = 25, IsOnDiscount = true },
            new Product { Id = 5, Name = "Souris Gaming", Price = 59.99m, Stock = 40, IsOnDiscount = false }
        ];

        private static int _nextId = 6;

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
        }

        public void Remove(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        public void AddRandom()
        {
            var random = new Random();

            var product = new Product
            {
                Id = _nextId++,
                Name = $"Product-{RandomString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 6)}",
                Price = random.Next(5, 200),
                Stock = random.Next(0, 50),
                IsOnDiscount = random.Next(0, 2) == 1
            };

            _products.Add(product);
        }

        private static string RandomString(string chars, int length)
        {
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

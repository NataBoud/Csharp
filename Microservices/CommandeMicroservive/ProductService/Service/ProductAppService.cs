using ProductService.DTO;
using ProductService.Models;
using ProductService.Repository;

namespace ProductService.Service
{
    public class ProductAppService : IService<ProductReceive, ProductSend>
    {
        private readonly IRepository<Product> repository;

        public ProductAppService(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        private Product DtoToEntity(ProductReceive receive, int? id)
        {
            Product product = new Product()
            {
                Name = receive.Name,
                Price = receive.Price,
                Quantity = receive.Quantity
            };

            if (id != null)
            {
                product.Id = (int)id;
            }

            return product;
        }


        private ProductSend EntityToDto(Product product)
        {
            return new ProductSend()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }

        // Create a new product
        public ProductSend Create(ProductReceive receive)
        {
            return EntityToDto(repository.Create(DtoToEntity(receive, null)));
        }

        // Update an existing product
        public ProductSend Update(ProductReceive receive, int id)
        {
            return EntityToDto(repository.Update(DtoToEntity(receive, id)));
        }

        // Delete a product by id
        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        // Get product by id
        public ProductSend GetById(int id)
        {
            Product product = repository.GetById(id);
            if (product == null)
            {
                return null;
            }

            return EntityToDto(product);
        }

        // Get all products
        public List<ProductSend> GetAll()
        {
            List<Product> products = repository.GetAll();
            List<ProductSend> productSends = new List<ProductSend>();
            foreach (var product in products)
            {
                productSends.Add(EntityToDto(product));
            }
            return productSends;
        }
    }
}

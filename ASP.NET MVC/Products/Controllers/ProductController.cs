using Microsoft.AspNetCore.Mvc;
using Products.Services;

namespace Products.Controllers
{
    public class ProductController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;

        //public ProductController(IProductService productService)
        //{
        //    _productService = productService;
        //}

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult CreateRandom()
        {
            _productService.AddRandom();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _productService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}

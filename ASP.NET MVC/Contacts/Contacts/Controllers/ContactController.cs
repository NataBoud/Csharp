using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            ViewData["ContactId"] = id;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}

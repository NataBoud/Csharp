using Contacts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class ContactController : Controller
    {

        // Fake data
        private static List<Contact> _contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "Alice", Email = "alice@mail.com" },
            new Contact { Id = 2, Name = "Bob", Email = "bob@mail.com" },
            new Contact { Id = 3, Name = "Charlie", Email = "charlie@mail.com" }
        };

        // LISTE → ViewBag
        public IActionResult Index()
        {
            ViewData["Title"] = "Contacts";
            ViewBag.Contacts = _contacts.OrderByDescending(c => c.Id).ToList();
            return View();
        }

        // DETAILS → Model
        public IActionResult Details(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound();
       
            ViewData["Title"] = "Détail contact";
            return View(contact);
        }

        // CREATE (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            if (!ModelState.IsValid) return View(contact);
     
            int newId = _contacts.Max(c => c.Id) + 1;
            contact.Id = newId;

            // .Insert(index, item), 0 → tout en haut de la liste
            _contacts.Insert(0, contact);

            return RedirectToAction("Index");
        }
    }
}

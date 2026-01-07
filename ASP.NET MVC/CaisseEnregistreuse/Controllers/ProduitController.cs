using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaisseEnregistreuse.Controllers
{
    public class ProduitController : Controller
    {
        private readonly IProduitService _produitService;
        private readonly ICategorieService _categorieService;

        public ProduitController(IProduitService produitService, ICategorieService categorieService)
        {
            _produitService = produitService;
            _categorieService = categorieService;
        }

        // Liste des produits
        public async Task<IActionResult> Index()
        {
            var produits = await _produitService.GetAllProduitsAsync();
            return View(produits);
        }

        // création
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categorieService.GetAllCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Produit produit)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categorieService.GetAllCategoriesAsync();
                return View(produit);
            }

            try
            {
                await _produitService.AddProduitAsync(produit);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex) // erreurs métiers, ex: unicité
            {
                ModelState.AddModelError(nameof(produit.Nom), ex.Message);
            }
            catch (Exception ex) // autres erreurs inattendues
            {
                ModelState.AddModelError("", "Une erreur inattendue est survenue : " + ex.Message);
            }
            ViewBag.Categories = await _categorieService.GetAllCategoriesAsync();
            return View(produit);
        }

        // édition
        public async Task<IActionResult> Edit(int id)
        {
            var produit = await _produitService.GetProduitByIdAsync(id);
            if (produit == null) return NotFound();
            ViewBag.Categories = await _categorieService.GetAllCategoriesAsync();
            return View(produit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Produit produit)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categorieService.GetAllCategoriesAsync();
                return View(produit);
            }

            try
            {
                await _produitService.UpdateProduitAsync(produit);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex) // erreurs métiers (unicité, etc.)
            {
                ModelState.AddModelError(nameof(produit.Nom), ex.Message);
            }
            catch (Exception ex) // autres erreurs inattendues
            {
                ModelState.AddModelError("", "Une erreur inattendue est survenue : " + ex.Message);
            }

            ViewBag.Categories = await _categorieService.GetAllCategoriesAsync();
            return View(produit);
        }


        // Détails d'un produit
        public async Task<IActionResult> Details(int id)
        {
            var produit = await _produitService.GetProduitByIdAsync(id);
            if (produit == null) return NotFound();

            return View(produit);
        } 

        // Supprimer
        public async Task<IActionResult> Delete(int id)
        {
            await _produitService.DeleteProduitAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}

using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services.Helpers;
using CaisseEnregistreuse.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaisseEnregistreuse.Controllers
{
    public class PanierController : Controller
    {
        private readonly IProduitService _produitService;

        public PanierController(IProduitService produitService)
        {
            _produitService = produitService;
        }

        // Affiche le panier
        public IActionResult Index()
        {
            var panier = HttpContext.Session.GetObjectFromJson<List<PanierItem>>("Panier") ?? new List<PanierItem>();
            ViewBag.Total = panier.Sum(p => p.Total);
            return View(panier);
        }

        // Ajouter un produit au panier
        public async Task<IActionResult> Ajouter(int produitId, int quantite = 1)
        {
            // Récupérer le produit depuis la base
            var produit = await _produitService.GetProduitByIdAsync(produitId);
            if (produit == null)
                return NotFound();

            // Récupérer le panier depuis la session
            var panier = HttpContext.Session.GetObjectFromJson<List<PanierItem>>("Panier") ?? new List<PanierItem>();

            // Vérifier si le produit est déjà dans le panier
            var item = panier.FirstOrDefault(p => p.ProduitId == produitId);
            if (item != null)
            {
                item.Quantite += quantite; // augmenter la quantité
            }
            else
            {
                panier.Add(new PanierItem
                {
                    ProduitId = produit.Id,
                    Nom = produit.Nom,
                    Prix = produit.Prix,
                    Quantite = quantite
                });
            }

            // Sauvegarder le panier en session
            HttpContext.Session.SetObjectAsJson("Panier", panier);

            return RedirectToAction("Index");
        }

        // Supprimer un produit du panier
        public IActionResult Supprimer(int produitId)
        {
            var panier = HttpContext.Session.GetObjectFromJson<List<PanierItem>>("Panier") ?? new List<PanierItem>();
            var item = panier.FirstOrDefault(p => p.ProduitId == produitId);
            if (item != null)
                panier.Remove(item);

            HttpContext.Session.SetObjectAsJson("Panier", panier);
            return RedirectToAction("Index");
        }

        // Vider le panier
        public IActionResult Vider()
        {
            HttpContext.Session.Remove("Panier");
            return RedirectToAction("Index");
        }

        // Modifier la quantité d'un produit dans le panier (+ ou -)
        [HttpPost]
        public IActionResult ModifierQuantite(int produitId, int variation)
        {
            var panier = HttpContext.Session.GetObjectFromJson<List<PanierItem>>("Panier") ?? new List<PanierItem>();
            var item = panier.FirstOrDefault(p => p.ProduitId == produitId);

            if (item != null)
            {
                item.Quantite += variation;
                if (item.Quantite < 1)
                    item.Quantite = 1;
            }

            HttpContext.Session.SetObjectAsJson("Panier", panier);
            return RedirectToAction("Index");
        }
    }
}

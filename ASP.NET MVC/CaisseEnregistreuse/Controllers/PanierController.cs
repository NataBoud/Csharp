using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services.Helpers;
using CaisseEnregistreuse.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        private async Task<int> AjouterProduitAuPanier(int produitId, int quantite)
        {
            var produit = await _produitService.GetProduitByIdAsync(produitId) ?? throw new Exception("Produit introuvable");
            var panier = HttpContext.Session.GetObjectFromJson<List<PanierItem>>("Panier")
                         ?? new List<PanierItem>();

            var item = panier.FirstOrDefault(p => p.ProduitId == produitId);
            if (item != null)
                item.Quantite += quantite;
            else
                panier.Add(new PanierItem
                {
                    ProduitId = produit.Id,
                    Nom = produit.Nom,
                    Prix = produit.Prix,
                    Quantite = quantite
                });

            HttpContext.Session.SetObjectAsJson("Panier", panier);
            return panier.Sum(p => p.Quantite);
        }

        // Ajouter un produit au panier via action normale
        public async Task<IActionResult> Ajouter(int produitId)
        {
            await AjouterProduitAuPanier(produitId, 1);
            return RedirectToAction("Index");
        }

        // Ajouter un produit au panier via AJAX
        [HttpPost]
        public async Task<IActionResult> AjouterAjax(int produitId)
        {
            var total = await AjouterProduitAuPanier(produitId, 1);
            return Json(new { success = true, totalItems = total });
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

        // Obtenir le nombre total d'articles dans le panier (pour mise à jour AJAX)
        public IActionResult Compteur()
        {
            var panier = HttpContext.Session.GetObjectFromJson<List<PanierItem>>("Panier")
                         ?? new List<PanierItem>();

            return Json(new
            {
                totalItems = panier.Sum(p => p.Quantite)
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

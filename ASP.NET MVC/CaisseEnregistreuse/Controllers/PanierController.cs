using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CaisseEnregistreuse.Controllers
{
    public class PanierController : Controller
    {
        private readonly IProduitService _produitService;
        private readonly IPanierService _panierService;

        public PanierController(IProduitService produitService, IPanierService panierService)
        {
            _produitService = produitService;
            _panierService = panierService;
        }

        // Affiche le panier
        public async Task<IActionResult> Index()
        {
            var panier = _panierService.GetPanier();

            // Récupérer le stock disponible pour chaque produit
            var panierAvecStock = new List<(PanierItem item, int stockDispo)>();

            foreach (var item in panier)
            {
                var produit = await _produitService.GetProduitByIdAsync(item.ProduitId);
                int stockDispo = produit?.QuantiteStock ?? 0;
                panierAvecStock.Add((item, stockDispo));
            }

            ViewBag.Total = _panierService.CalculerTotal();
            return View(panierAvecStock); //on envoie le bon type
        }


        // Ajouter un produit au panier via action normale
        public async Task<IActionResult> Ajouter(int produitId)
        {
            var produit = await _produitService.GetProduitByIdAsync(produitId);
            if (produit != null)
                _panierService.AjouterProduit(produit, 1);

            return RedirectToAction("Index");
        }

        // Ajouter un produit au panier via AJAX
        [HttpPost]
        public async Task<IActionResult> AjouterAjax(int produitId)
        {
            var produit = await _produitService.GetProduitByIdAsync(produitId);
            if (produit == null)
                return Json(new { success = false, message = "Produit introuvable" });

            _panierService.AjouterProduit(produit, 1);

            // Vérifier si la quantité max est atteinte
            var item = _panierService.GetPanier().FirstOrDefault(p => p.ProduitId == produitId);
            bool quantiteMaxAtteinte = item != null && item.Quantite >= produit.QuantiteStock;

            return Json(new
            {
                success = true,
                totalItems = _panierService.GetPanier().Sum(p => p.Quantite),
                quantiteMaxAtteinte
            });
        }

        // Modifier la quantité (+ ou -) via AJAX ou formulaire
        [HttpPost]
        public async Task<IActionResult> ModifierQuantite(int produitId, int variation)
        {
            await _panierService.ModifierQuantiteAsync(produitId, variation);
            return RedirectToAction("Index");
        }

        // Supprimer un produit du panier
        public IActionResult Supprimer(int produitId)
        {
            _panierService.SupprimerProduit(produitId);
            return RedirectToAction("Index");
        }

        // Vider le panier
        public IActionResult Vider()
        {
            _panierService.ViderPanier();
            return RedirectToAction("Index");
        }

        // Obtenir le nombre total d'articles dans le panier (pour le compteur AJAX du header)
        public IActionResult Compteur()
        {
            var totalItems = _panierService.GetPanier().Sum(p => p.Quantite);
            return Json(new { totalItems });
        }

        // Get quantite pour la page Details produit ou le panier n est pas accesible
        [HttpGet]
        public IActionResult QuantiteProduit(int produitId)
        {
            var item = _panierService
                .GetPanier()
                .FirstOrDefault(p => p.ProduitId == produitId);

            return Json(new
            {
                quantite = item?.Quantite ?? 0
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

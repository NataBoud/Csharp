using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services.Interfaces;
using System.Text.Json;

namespace CaisseEnregistreuse.Services
{
    public class PanierService : IPanierService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProduitService _produitService; // pour vérifier le stock
        private const string SessionKey = "Panier";

        public PanierService(IHttpContextAccessor httpContextAccessor, IProduitService produitService)
        {
            _httpContextAccessor = httpContextAccessor;
            _produitService = produitService;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        // Récupère la liste des produits dans le panier depuis la session
        public List<PanierItem> GetPanier()
        {
            var json = Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(json)) return new List<PanierItem>();
            return JsonSerializer.Deserialize<List<PanierItem>>(json)!;
        }

        // Sauvegarde la liste du panier dans la session
        private void SavePanier(List<PanierItem> panier)
        {
            var json = JsonSerializer.Serialize(panier);
            Session.SetString(SessionKey, json);
        }

        // Ajoute un produit au panier (limité à la quantité en stock)
        public void AjouterProduit(Produit produit, int quantite = 1)
        {
            var panier = GetPanier();
            var item = panier.FirstOrDefault(p => p.ProduitId == produit.Id);

            int nouvelleQuantite = (item?.Quantite ?? 0) + quantite;

            // Limiter à la quantité disponible en stock
            if (nouvelleQuantite > produit.QuantiteStock)
                nouvelleQuantite = produit.QuantiteStock;

            if (item != null)
                item.Quantite = nouvelleQuantite;
            else
                panier.Add(new PanierItem
                {
                    ProduitId = produit.Id,
                    Nom = produit.Nom,
                    Prix = produit.Prix,
                    Quantite = nouvelleQuantite
                });

            SavePanier(panier);
        }

        // Modifie la quantité d'un produit dans le panier (+ ou -), vérifie le stock depuis la BDD
        public async Task ModifierQuantiteAsync(int produitId, int variation)
        {
            var panier = GetPanier();
            var item = panier.FirstOrDefault(p => p.ProduitId == produitId);

            if (item != null)
            {
                // Récupère le produit depuis la BDD pour connaître le stock réel
                var produit = await _produitService.GetProduitByIdAsync(produitId);
                if (produit == null) return;

                int nouvelleQuantite = item.Quantite + variation;

                // Ne pas descendre en dessous de 1
                if (nouvelleQuantite < 1)
                    nouvelleQuantite = 1;

                // Ne pas dépasser le stock
                if (nouvelleQuantite > produit.QuantiteStock)
                    nouvelleQuantite = produit.QuantiteStock;

                item.Quantite = nouvelleQuantite;
                SavePanier(panier);
            }
        }

        // Supprime un produit du panier
        public void SupprimerProduit(int produitId)
        {
            var panier = GetPanier();
            var item = panier.FirstOrDefault(p => p.ProduitId == produitId);
            if (item != null)
            {
                panier.Remove(item);
                SavePanier(panier);
            }
        }

        // Vide entièrement le panier
        public void ViderPanier()
        {
            SavePanier(new List<PanierItem>());
        }

        // Calcule le total du panier
        public decimal CalculerTotal()
        {
            var panier = GetPanier();
            return panier.Sum(p => p.Total);
        }
    }
}

using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services.Interfaces;
using System.Text.Json;

namespace CaisseEnregistreuse.Services
{
    public class PanierService : IPanierService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKey = "Panier";

        public PanierService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public List<PanierItem> GetPanier()
        {
            var json = Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(json)) return new List<PanierItem>();
            return JsonSerializer.Deserialize<List<PanierItem>>(json)!;
        }

        private void SavePanier(List<PanierItem> panier)
        {
            var json = JsonSerializer.Serialize(panier);
            Session.SetString(SessionKey, json);
        }

        public void AjouterProduit(Produit produit, int quantite = 1)
        {
            var panier = GetPanier();
            var item = panier.FirstOrDefault(p => p.ProduitId == produit.Id);
            if (item != null)
            {
                item.Quantite += quantite; // si déjà dans le panier, on augmente la quantité
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
            SavePanier(panier);
        }

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

        public void ViderPanier()
        {
            SavePanier(new List<PanierItem>());
        }

        public decimal CalculerTotal()
        {
            var panier = GetPanier();
            return panier.Sum(p => p.Total);
        }
    }
}

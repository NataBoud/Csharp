using CaisseEnregistreuse.Models;

namespace CaisseEnregistreuse.Services.Interfaces
{
    public interface IPanierService
    {
        List<PanierItem> GetPanier();
        void AjouterProduit(Produit produit, int quantite = 1);
        Task ModifierQuantiteAsync(int produitId, int variation);
        void SupprimerProduit(int produitId);
        void ViderPanier();
        decimal CalculerTotal();
    }
}

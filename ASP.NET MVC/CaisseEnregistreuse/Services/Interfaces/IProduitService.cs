using CaisseEnregistreuse.Models;

namespace CaisseEnregistreuse.Services.Interfaces
{
    public interface IProduitService
    {
        Task<List<Produit>> GetAllProduitsAsync();
        Task<Produit?> GetProduitByIdAsync(int id);
        Task AddProduitAsync(Produit produit);
        Task UpdateProduitAsync(Produit produit);
        Task DeleteProduitAsync(int id);
    }
}

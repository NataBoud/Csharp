using CaisseEnregistreuse.Models;

namespace CaisseEnregistreuse.Services.Interfaces
{
    public interface ICategorieService
    {
        Task<List<Categorie>> GetAllCategoriesAsync();
        Task<Categorie?> GetCategorieByIdAsync(int id);
        Task AddCategorieAsync(Categorie categorie);
        Task UpdateCategorieAsync(Categorie categorie);
        Task DeleteCategorieAsync(int id);
    }
}

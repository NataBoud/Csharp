using CaisseEnregistreuse.Data;
using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services.Helpers;
using CaisseEnregistreuse.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaisseEnregistreuse.Services
{
    public class CategorieService : ICategorieService
    {
        private readonly AppDbContext _context;

        public CategorieService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categorie>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Include(c => c.Produits).ToListAsync();
        }

        public async Task<Categorie?> GetCategorieByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Produits)
                                            .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task AddCategorieAsync(Categorie categorie)
        {
            ValidateCategorie(categorie);
            try
            {
                _context.Categories.Add(categorie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                DbExceptionHelper.HandleUniqueConstraint(ex, "Catégorie", categorie.Nom);
            }
        }

        public async Task UpdateCategorieAsync(Categorie categorie)
        {
            ValidateCategorie(categorie);
            try
            {
                _context.Categories.Update(categorie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                DbExceptionHelper.HandleUniqueConstraint(ex, "Catégorie", categorie.Nom);
            }
        }

        public async Task DeleteCategorieAsync(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);
            if (categorie != null)
            {
                _context.Categories.Remove(categorie);
                await _context.SaveChangesAsync();
            }
        }

        private void ValidateCategorie(Categorie categorie)
        {
            if (string.IsNullOrWhiteSpace(categorie.Nom))
                throw new ArgumentException("Le nom de la catégorie est obligatoire.");
        }
    }
}

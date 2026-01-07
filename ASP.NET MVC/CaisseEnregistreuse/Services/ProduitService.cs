using CaisseEnregistreuse.Data;
using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services.Helpers;
using CaisseEnregistreuse.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaisseEnregistreuse.Services
{
    public class ProduitService : IProduitService
    {
        private readonly AppDbContext _context;

        public ProduitService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produit>> GetAllProduitsAsync()
        {
            return await _context.Produits.Include(p => p.Categorie).ToListAsync();
        }

        public async Task<Produit?> GetProduitByIdAsync(int id)
        {
            return await _context.Produits.Include(p => p.Categorie)
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        private void ValidateProduit(Produit produit)
        {
            if (produit.CategorieId <= 0)
                throw new ArgumentException("Le produit doit être associé à une catégorie valide.");

            if (produit.Prix < 0) produit.Prix = 0;
            if (produit.QuantiteStock < 0) produit.QuantiteStock = 0;
        }

        public async Task AddProduitAsync(Produit produit)
        {
            ValidateProduit(produit);
            try
            {
                _context.Produits.Add(produit);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                DbExceptionHelper.HandleUniqueConstraint(ex, "Produit", produit.Nom);
            }
        }

        public async Task UpdateProduitAsync(Produit produit)
        {
            ValidateProduit(produit);
            try
            {
                _context.Produits.Update(produit);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                DbExceptionHelper.HandleUniqueConstraint(ex, "Produit", produit.Nom);
            }
        }

        public async Task DeleteProduitAsync(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
                await _context.SaveChangesAsync();
            }
        }


    }
}

using CaisseEnregistreuse.Models;
using Microsoft.EntityFrameworkCore;

namespace CaisseEnregistreuse.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Produit> Produits { get; set; }
        public DbSet<Categorie> Categories { get; set; }
    }
}

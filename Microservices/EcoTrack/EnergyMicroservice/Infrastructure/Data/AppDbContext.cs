using EnergyMicroservice.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace EnergyMicroservice.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Energy> Energies { get; set; }
    }
}

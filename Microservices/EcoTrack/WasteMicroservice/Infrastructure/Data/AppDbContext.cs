
using Microsoft.EntityFrameworkCore;
using WasteMicroservice.Domain.Entity;

namespace WasteMicroservice.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Waste> Wastes { get; set; }
    }
}

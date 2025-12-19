
using Microsoft.EntityFrameworkCore;
using TransportMicroservice.Domain.Entity;

namespace TransportMicroservice.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Transport> Transports { get; set; }
    }
}

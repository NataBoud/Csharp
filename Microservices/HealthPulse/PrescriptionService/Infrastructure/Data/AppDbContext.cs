using Microsoft.EntityFrameworkCore;
using PrescriptionService.Domain.Entities;

namespace PrescriptionService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Prescription> Prescriptions { get; set; }
    }
}

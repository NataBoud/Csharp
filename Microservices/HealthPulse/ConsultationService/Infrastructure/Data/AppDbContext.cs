using ConsultationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsultationService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Consultation> Consultations { get; set; }
    }
}

using EnergyMicroservice.Domain.Entity;
using EnergyMicroservice.Domain.Ports;
using EnergyMicroservice.Infrastructure.Data;
using System.Threading.Tasks;

namespace EnergyMicroservice.Infrastructure.Persistence
{
    public class EnergyRepository : IEnergyrepository
    {
        private readonly AppDbContext _db;

        public EnergyRepository(AppDbContext db)
        {
            _db = db;
        }

        public Energy Add(Energy energy)
        {
            _db.Add(energy);
            _db.SaveChanges();
            return energy;
        }

        public List<Energy> GetAll()
        {
            return _db.Energies.ToList();
        }

        public Energy? GetById(Guid id)
        {
            return _db.Energies.FirstOrDefault(e => e.Id == id);
        }
    }
}

using EnergyMicroservice.Domain.Entity;

namespace EnergyMicroservice.Domain.Ports
{
    public interface IEnergyrepository
    {
        Energy? GetById(Guid id);
        List<Energy> GetAll();
        Energy Add(Energy energy);
    }
}

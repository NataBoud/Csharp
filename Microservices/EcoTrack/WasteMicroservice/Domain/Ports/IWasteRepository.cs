using WasteMicroservice.Domain.Entity;

namespace WasteMicroservice.Domain.Ports
{
    public interface IWasteRepository
    {
        Waste? GetById(Guid id);
        List<Waste> GetAll();
        Waste Add(Waste waste);
        Waste? Update(Waste waste);
    }
}

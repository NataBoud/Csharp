using TransportMicroservice.Domain.Entity;

namespace TransportMicroservice.Domain.Ports
{
    public interface ITransportRepository
    {
        Transport? GetById(Guid id);
        List<Transport> GetAll();
        Transport Add(Transport transport);
        Transport? Update(Transport transport);
        bool Delete(Guid id);
    }
}

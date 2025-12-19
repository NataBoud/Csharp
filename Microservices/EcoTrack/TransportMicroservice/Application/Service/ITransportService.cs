using TransportMicroservice.Application.DTO;

namespace TransportMicroservice.Application.Service
{
    public interface ITransportService
    {
        TransportDtoSend GetById(Guid id);
        List<TransportDtoSend> GetAll();
        TransportDtoSend Create(TransportDtoReceive receive);
        TransportDtoSend Update(Guid id, TransportDtoReceive receive);
        double GetEmissionById(Guid id);
    }
}


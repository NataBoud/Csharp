using EnergyMicroservice.Application.DTO;

namespace EnergyMicroservice.Application.Service
{
    public interface IEnergyService
    {
        EnergyDtoSend GetById(Guid id);
        List<EnergyDtoSend> GetAll();
        EnergyDtoSend Create(EnergyDtoReceive receive);
   
    }
}

using WasteMicroservice.Application.DTO;

namespace WasteMicroservice.Application.Service
{
    public interface IWasteService
    {
        WasteDtoSend GetById(Guid id);
        List<WasteDtoSend> GetAll();
        WasteDtoSend Create(WasteDtoReceive receive);
        WasteDtoSend Update(Guid id, WasteDtoReceive receive);

    }
}

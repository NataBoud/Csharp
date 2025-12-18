using WasteMicroservice.Application.DTO;
using WasteMicroservice.Domain.Entity;
using WasteMicroservice.Domain.Ports;

namespace WasteMicroservice.Application.Service
{
    public class WasteService : IWasteService
    {
        private readonly IWasteRepository _wasteRepository;

        public WasteService(IWasteRepository wasteRepository)
        {
            _wasteRepository = wasteRepository;
        }

        public WasteDtoSend Create(WasteDtoReceive receive)
        {
            var waste = new Waste
            {
                Type = receive.Type,
                QuantiteKg = receive.QuantiteKg,
                TauxRecyclage = receive.TauxRecyclage
            };

            var created = _wasteRepository.Add(waste);
            return MapToResponse(created);
        }

        public List<WasteDtoSend> GetAll()
        {
            var wastes = _wasteRepository.GetAll();
            return MapToListResponse(wastes);
        }

        public WasteDtoSend GetById(Guid id)
        {
            var waste = _wasteRepository.GetById(id);
            return waste == null ? null : MapToResponse(waste);
        }

        public WasteDtoSend Update(Guid id, WasteDtoReceive receive)
        {
            var existingWaste = _wasteRepository.GetById(id);
            if (existingWaste == null) return null;

            existingWaste.Type = receive.Type;
            existingWaste.QuantiteKg = receive.QuantiteKg;
            existingWaste.TauxRecyclage = receive.TauxRecyclage;

            var updated = _wasteRepository.Update(existingWaste);
            return MapToResponse(updated);
        }

        // ===== Mapping =====
        private WasteDtoSend MapToResponse(Waste waste)
        {
            return new WasteDtoSend
            {
                Id = waste.Id,
                Type = waste.Type,
                QuantiteKg = waste.QuantiteKg,
                TauxRecyclage = waste.TauxRecyclage
            };
        }

        private List<WasteDtoSend> MapToListResponse(List<Waste> wastes)
        {
            var list = new List<WasteDtoSend>();
            foreach (var waste in wastes)
            {
                list.Add(MapToResponse(waste));
            }
            return list;
        }
    }

}

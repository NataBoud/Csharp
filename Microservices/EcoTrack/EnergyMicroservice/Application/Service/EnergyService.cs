using EnergyMicroservice.Application.DTO;
using EnergyMicroservice.Domain.Entity;
using EnergyMicroservice.Domain.Ports;

namespace EnergyMicroservice.Application.Service
{
    public class EnergyService : IEnergyService
    {
        private readonly IEnergyrepository _repository;

        public EnergyService(IEnergyrepository repository)
        {
            _repository = repository;
        }

        public EnergyDtoSend GetById(Guid id)
        {
            var energy = _repository.GetById(id);
            return energy == null ? null : MapToResponse(energy);
        }

        public List<EnergyDtoSend> GetAll()
        {
            return MapToListResponse(_repository.GetAll());
        }

        public EnergyDtoSend Create(EnergyDtoReceive receive)
        {
            var energy = new Energy
            {
                Source = receive.Source,
                ConsommationKWh = receive.ConsommationKWh,
                Date = receive.Date
            };

            var created = _repository.Add(energy);
            return MapToResponse(created);
        }

        // ===== Mapping =====
        private EnergyDtoSend MapToResponse(Energy? energy)
        {
            if (energy == null) return null;
            return new EnergyDtoSend
            {
                Id = energy.Id,
                Source = energy.Source,
                ConsommationKWh = energy.ConsommationKWh,
                Date = energy.Date
            };
        }

        private List<EnergyDtoSend> MapToListResponse(List<Energy> energies)
        {
            List<EnergyDtoSend> result = new();

            foreach (var energy in energies)
            {
                result.Add(MapToResponse(energy));
            }

            return result;
        }
    }
}

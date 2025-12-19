
using TransportMicroservice.Application.DTO;
using TransportMicroservice.Domain.Entity;
using TransportMicroservice.Domain.Ports;
using TransportMicroservice.Infrastructure.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransportMicroservice.Application.Service
{
    public class TransportService : ITransportService
    {
        private readonly ITransportRepository _repository;

        // Facteurs d'émission prédéfinis
        private readonly Dictionary<TransportMode, double> _emissionFactors = new()
        {
            { TransportMode.Voiture, 120 },
            { TransportMode.Bus, 80 },
            { TransportMode.Train, 30 },
            { TransportMode.Velo, 0 }
        };

        public TransportService(ITransportRepository repository)
        {
            _repository = repository;
        }

        public TransportDtoSend GetById(Guid id)
        {
            var transport = _repository.GetById(id);
            return transport == null ? null : MapToResponse(transport);
        }

        public List<TransportDtoSend> GetAll()
        {
            var transports = _repository.GetAll();
            return MapToListResponse(transports);
        }

        public TransportDtoSend Create(TransportDtoReceive receive)
        {
            double facteurEmission = _emissionFactors[receive.Mode];

            var transport = new Transport
            {
                Mode = receive.Mode,
                DistanceKm = receive.DistanceKm,
                FacteurEmission = facteurEmission
            };

            var created = _repository.Add(transport);
            return MapToResponse(created);
        }

        public TransportDtoSend Update(Guid id, TransportDtoReceive receive)
        {
            var transport = _repository.GetById(id);
            if (transport == null) return null;

            transport.Mode = receive.Mode;
            transport.DistanceKm = receive.DistanceKm;
            transport.FacteurEmission = _emissionFactors[receive.Mode];

            var updated = _repository.Update(transport);
            return MapToResponse(updated);
        }

        public double GetEmissionById(Guid id)
        {
            var transport = _repository.GetById(id);
            if (transport == null) throw new Exception("Transport not found");

            return transport.EmissionCO2; // utilise directement la propriété calculée
        }

        // Delete a transport by id
        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        // ===== Mapping =====
        private TransportDtoSend MapToResponse(Transport transport)
        {
            return new TransportDtoSend
            {
                Id = transport.Id,
                Mode = transport.Mode,
                DistanceKm = transport.DistanceKm,
                FacteurEmission = transport.FacteurEmission,
                EmissionCO2 = transport.EmissionCO2
            };
        }

        private List<TransportDtoSend> MapToListResponse(List<Transport> transports)
        {
            var list = new List<TransportDtoSend>();
            foreach (var transport in transports)
            {
                list.Add(MapToResponse(transport));
            }
            return list;
        }
    }
}

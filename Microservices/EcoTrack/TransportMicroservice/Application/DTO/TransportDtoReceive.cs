using TransportMicroservice.Domain.Entity;

namespace TransportMicroservice.Application.DTO
{
    public class TransportDtoReceive
    {
        public TransportMode Mode { get; set; }
        public double DistanceKm { get; set; }
    }
}

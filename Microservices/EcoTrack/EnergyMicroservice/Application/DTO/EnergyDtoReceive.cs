using EnergyMicroservice.Domain.Entity;

namespace EnergyMicroservice.Application.DTO
{
    public class EnergyDtoReceive
    {
        public EnergySource Source { get; set; }
        public double ConsommationKWh { get; set; }
        public DateTime Date { get; set; }
    }
}

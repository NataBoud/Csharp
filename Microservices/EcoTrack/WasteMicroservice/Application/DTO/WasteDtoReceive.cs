using WasteMicroservice.Domain.Entity;

namespace WasteMicroservice.Application.DTO
{
    public class WasteDtoReceive
    {
        public WasteType Type { get; set; }
        public double QuantiteKg { get; set; }
        public double TauxRecyclage { get; set; }
    }
}

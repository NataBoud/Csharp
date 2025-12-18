namespace EnergyMicroservice.Domain.Entity
{
    public class Energy
    {
        public Guid Id { get; private set; }

        public EnergySource Source { get; set; }

        public double ConsommationKWh { get; set; }
      
        public DateTime Date { get; set; }
    }
}

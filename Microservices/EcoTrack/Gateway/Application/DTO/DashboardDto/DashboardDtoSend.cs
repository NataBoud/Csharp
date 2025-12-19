using System.Text.Json.Serialization;

namespace Gateway.Application.DTO.DashboardDto
{
    public class DashboardDtoSend
    {
   
        public double TotalEnergyConsumption { get; set; }


        public double TotalWasteQuantity { get; set; }


        public double TotalCO2Emission { get; set; }
    }
}

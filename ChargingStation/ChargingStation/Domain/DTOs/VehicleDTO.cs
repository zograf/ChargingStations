using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;

namespace ChargingStation.Domain.DTOs
{
    public class VehicleDTO
    {
        public string Name{ get; set; }
        public string RegistrationNumber{ get; set; }
        public int Power { get; set; }
        public decimal ClientId { get; set; }
    }
}

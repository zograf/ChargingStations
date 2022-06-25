namespace ChargingStation.Domain.Models;

public class StationDomainModel
{
     public decimal Id { get; set; }
     
     public string Name { get; set; }
     
     public bool IsDeleted { get; set; }
     
     public ManagerDomainModel Manager { get; set; }
     public AddressDomainModel Address { get; set; }
     public List<BasePriceDomainModel> BasePrices { get; set; }
     public List<ChargingSpotDomainModel> ChargingSpots { get; set; }   
}
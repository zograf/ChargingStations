namespace ChargingStation.Domain.Models;

public class VehicleDomainModel
{
    public decimal Id { get; set; }
    
    public decimal Name { get; set; }
    
    public string RegistrationPlate { get; set; }
    
    public decimal Power { get; set; }
    
    public decimal ClientId { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public CardDomainModel Card { get; set; }
    
}
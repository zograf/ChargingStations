namespace ChargingStation.Domain.Models;

public class AddressDomainModel
{
    public decimal Id { get; set; }
    
    public string Number { get; set; }
    
    public string Entrance { get; set; }
    
    public decimal StationId { get; set; }
    
    public decimal PlaceId { get; set; }
    
    public bool IsDeleted { get; set; }
    
}
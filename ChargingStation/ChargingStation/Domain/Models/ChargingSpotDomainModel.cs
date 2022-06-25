namespace ChargingStation.Domain.Models;

public class ChargingSpotDomainModel
{
    public decimal Id { get; set; }
    
    public int SerialNumber { get; set; }
    
    public decimal StationId { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public List<ChargingDomainModel> Chargings { get; set; }}
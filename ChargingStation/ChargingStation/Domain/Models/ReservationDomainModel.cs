namespace ChargingStation.Domain.Models;

public class ReservationDomainModel
{
    public decimal Id { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public decimal? ChargingId { get; set; }
    
    public decimal CardId { get; set; }
    
    public bool IsDeleted { get; set; }
}
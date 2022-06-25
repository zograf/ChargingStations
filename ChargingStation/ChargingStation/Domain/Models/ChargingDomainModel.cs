namespace ChargingStation.Domain.Models;

public class ChargingDomainModel
{
    public decimal Id { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public decimal ElectricitySpent { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public decimal ChargingSpotId { get; set; }
    
    public decimal? ReservationId { get; set; }
    
    public decimal CardId { get; set; }
    
    public bool IsDeleted { get; set; }

    public ReservationDomainModel Reservation { get; set; }
    
}
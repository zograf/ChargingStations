namespace ChargingStation.Domain.Models;

public class CardDomainModel
{
    public decimal Id { get; set; }
    
    public bool IsBlocked { get; set; }
    
    public decimal NotComingCounter { get; set; }
    
    public decimal StayedLongerCounter { get; set; }
    
    public decimal VehicleId { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public List<ChargingDomainModel> Chargings { get; set; }
    public List<ReservationDomainModel> Reservations { get; set; }}
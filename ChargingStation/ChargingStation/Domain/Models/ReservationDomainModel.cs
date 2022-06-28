using ChargingStation.Data.Entity;
using ChargingStation.Domain.Utilities;

namespace ChargingStation.Domain.Models;

public class ReservationDomainModel
{
    public decimal Id { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public decimal? ChargingId { get; set; }
    public decimal ChargingSpotId { get; set; }

    public decimal CardId { get; set; }
    
    public bool IsDeleted { get; set; }

    public bool IsOverlaping(DateTime start, DateTime end, int preMinOffset)
    {
        return DateTimeUtils.IsDateTimeOverlap(start, end, 
            this.StartTime.AddMinutes(-preMinOffset), this.EndTime);
    }
}
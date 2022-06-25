namespace ChargingStation.Domain.Models;

public class BasePriceDomainModel
{
     public decimal Id { get; set; }
     
     public decimal DayAmount { get; set; }
     
     public decimal NightAmount { get; set; }
     
     public DateTime ApplyTime { get; set; }
     
     public decimal StationId { get; set; }
     
     public bool IsDeleted { get; set; }   
}
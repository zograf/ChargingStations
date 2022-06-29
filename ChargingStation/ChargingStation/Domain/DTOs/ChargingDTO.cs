namespace ChargingStation.Domain.DTOs;

public class ArriveDTO
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal CardId { get; set; }
}
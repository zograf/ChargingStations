namespace ChargingStation.Domain.Models;

public class NotificationDomainModel
{
    public decimal Id { get; set; }

    public bool IsRead { get; set; }

    public decimal ClientId { get; set; }
}
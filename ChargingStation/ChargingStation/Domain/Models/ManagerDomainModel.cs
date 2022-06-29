namespace ChargingStation.Domain.Models;

public class ManagerDomainModel
{
    public decimal Id { get; set; }

    public decimal UserId { get; set; }

    public decimal StationId { get; set; }

    public UserDomainModel User { get; set; }
}
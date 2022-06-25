namespace ChargingStation.Domain.Models;

public class CredentialsDomainModel
{
    public string Username { get; set; }

    public string Password { get; set; }

    public decimal UserId { get; set; }

    public bool IsDeleted { get; set; }
}
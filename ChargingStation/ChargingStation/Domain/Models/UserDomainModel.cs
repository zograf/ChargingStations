namespace ChargingStation.Domain.Models;

public class UserDomainModel
{
    public decimal Id { get; set; }

    public string Role { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public CredentialsDomainModel Credentials { get; set; }
}
namespace ChargingStation.Domain.Models;

public class PlaceDomainModel
{
    public decimal Id { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public List<AddressDomainModel> Addresses { get; set; }
}
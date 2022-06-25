namespace ChargingStation.Domain.Models;

public class ClientDomainModel
{
    public decimal Id { get; set; }
    
    public decimal Balance { get; set; }
    
    public decimal UserIdentificationNumber { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string? LegalName { get; set; }
    
    public string? Vat { get; set; }
    
    public decimal UserId { get; set; }
    
    public UserDomainModel User { get; set; }
    public List<TransactionDomainModel> Transactions { get; set; }
    public List<VehicleDomainModel> Vehicles { get; set; }}
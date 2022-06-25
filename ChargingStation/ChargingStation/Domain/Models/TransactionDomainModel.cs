namespace ChargingStation.Domain.Models;

public class TransactionDomainModel
{
    public enum TransactionType
    {
        INCREASE,
        DECREASE 
    }

    public decimal Id { get; set; }
    
    public decimal Amount { get; set; }
    
    public DateTime Time { get; set; }
    
    public TransactionType Type { get; set; }
    
    public decimal ClientId { get; set; }
    
    public bool IsDeleted { get; set; }
    
}
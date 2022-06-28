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
    
    public string Type { get; set; }
    
    public decimal ClientId { get; set; }
    
    public bool IsDeleted { get; set; }

    public static string TranslateType(TransactionType type)
    {
        if (type == TransactionType.INCREASE)
            return "increase";
        else
            return "decrease";
    }


}
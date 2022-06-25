using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("client")]
public class Client
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    
    [Column("balance")]
    public decimal Balance { get; set; }
    
    [Column("uin")]
    public decimal UserIdentificationNumber { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("surname")]
    public string Surname { get; set; }
    
    [Column("legal_name")]
    public string? LegalName { get; set; }
    
    [Column("pib")]
    public string? Vat { get; set; }
    
    [Column("id1")]
    public decimal UserId { get; set; }
    
    public User User { get; set; }
    public List<Transaction> Transactions { get; set; }
    public List<Vehicle> Vehicles { get; set; }
}

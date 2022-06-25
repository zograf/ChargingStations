using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("vehicle")]
public class Vehicle
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    
    [Column("name")]
    public decimal Name { get; set; }
    
    [Column("register_number")]
    public string RegistrationPlate { get; set; }
    
    [Column("power")]
    public decimal Power { get; set; }
    
    [Column("client_id1")]
    public decimal ClientId { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}

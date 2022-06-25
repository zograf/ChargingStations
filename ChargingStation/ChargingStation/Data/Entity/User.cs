using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("user")]
public class User
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("role")]
    public string Role { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
    
    public Credentials Credentials { get; set; }
}
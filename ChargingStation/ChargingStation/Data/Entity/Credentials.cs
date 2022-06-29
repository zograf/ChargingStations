using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("credentials")]
public class Credentials
{
    [Column("username")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Username { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("user_id")]
    public decimal UserId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}
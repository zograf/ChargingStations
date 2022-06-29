using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("notifications")]
public class Notification
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("is_read")]
    public bool IsRead { get; set; }

    [Column("client_id")]
    public decimal ClientId { get; set; }
}
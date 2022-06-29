using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("manager")]
public class Manager
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("id1")]
    public decimal UserId { get; set; }

    [Column("station_id")]
    public decimal StationId { get; set; }

    public User User { get; set; }
}
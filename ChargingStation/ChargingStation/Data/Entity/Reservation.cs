using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("reservation")]
public class Reservation
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("start_time")]
    public DateTime StartTime { get; set; }

    [Column("end_time")]
    public DateTime EndTime { get; set; }

    [Column("unit_price")]
    public decimal UnitPrice { get; set; }

    [Column("charging_id")]
    public decimal? ChargingId { get; set; }

    [Column("charging_spot_id")]
    public decimal ChargingSpotId { get; set; }

    [Column("card_id")]
    public decimal CardId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}
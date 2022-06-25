using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("charging")]
public class Charging
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
    
    [Column("electricity_spent")]
    public decimal ElectricitySpent { get; set; }
    
    [Column("total_price")]
    public decimal TotalPrice { get; set; }
    
    [Column("charging_spot_id")]
    public decimal ChargingSpotId { get; set; }
    
    [Column("reservation_id")]
    public decimal? ReservationId { get; set; }
    
    [Column("card_id")]
    public decimal CardId { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    public Reservation Reservation { get; set; }
}

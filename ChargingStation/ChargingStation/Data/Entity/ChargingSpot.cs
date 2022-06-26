using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("charging_spot")]
public class ChargingSpot
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    
    [Column("serial_number")]
    public int SerialNumber { get; set; }
    
    [Column("station_id")]
    public decimal StationId { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
    
    [Column("can_be_reserved")]
    public bool IsReservable { get; set; }
    
    [Column("spot_state")]
    public decimal State { get; set; }
    
    public List<Charging> Chargings { get; set; }
}

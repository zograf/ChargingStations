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
}

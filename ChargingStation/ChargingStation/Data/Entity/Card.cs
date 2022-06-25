using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("card")]
public class Card
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    
    [Column("blocked")]
    public bool IsBlocked { get; set; }
    
    [Column("not_coming_counter")]
    public decimal NotComingCounter { get; set; }
    
    [Column("stayed_longer_counter")]
    public decimal StayedLongerCounter { get; set; }
    
    [Column("vehicle_id")]
    public decimal VehicleId { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}

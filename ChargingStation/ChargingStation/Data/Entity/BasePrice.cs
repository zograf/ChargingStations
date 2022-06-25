using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("base_price")]
public class BasePrice
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    
    [Column("day_amount")]
    public decimal DayAmount { get; set; }
    
    [Column("night_amount")]
    public decimal NightAmount { get; set; }
    
    [Column("starts_to_apply")]
    public DateTime ApplyTime { get; set; }
    
    [Column("station_id")]
    public decimal StationId { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}

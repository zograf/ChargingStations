using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("station")]
public class Station
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}

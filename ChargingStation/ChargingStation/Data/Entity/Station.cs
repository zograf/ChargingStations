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

    public Manager Manager { get; set; }
    public Address Address { get; set; }
    public List<BasePrice> BasePrices { get; set; }
    public List<ChargingSpot> ChargingSpots { get; set; }
}
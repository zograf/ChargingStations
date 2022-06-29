using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("adress")]
public class Address
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("number")]
    public string Number { get; set; }

    [Column("entrance")]
    public string Entrance { get; set; }

    [Column("station_id")]
    public decimal StationId { get; set; }

    [Column("place_id")]
    public decimal PlaceId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}
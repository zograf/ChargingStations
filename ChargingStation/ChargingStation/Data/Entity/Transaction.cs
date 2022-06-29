using System.ComponentModel.DataAnnotations.Schema;

namespace ChargingStation.Data.Entity;

[Table("Transaction")]
public class Transaction
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("time")]
    public DateTime Time { get; set; }

    [Column("type")]
    public string Type { get; set; }

    [Column("client_id1")]
    public decimal ClientId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}
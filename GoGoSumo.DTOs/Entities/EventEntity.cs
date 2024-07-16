using GoGoSumo.DTOs.Helpers.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Entities;

public class EventEntity
{
    [Column("event_id")]
    public int? EventId { get; set; }
    [MaxLength(100)]
    [Column("event_name")]
    public string? EventName { get; set; }
    [Column("event_date")]
    public DateTime? EventDate { get; set; }
    [MaxLength(255)]
    [Column("event_location")]
    public string? EventLocation { get; set; }
    [DataType(DataType.Currency)]
    [Column("event_gogo_price_yen")]
    public decimal? EventGoGoPrice { get; set; }
}

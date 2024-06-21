using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Entities;

public class EventEntity
{
    public int? EventId { get; set; }
    [MaxLength(100)]
    public string? EventName { get; set; }
    public DateOnly? EventDate { get; set; }
    [MaxLength(255)]
    public string? EventLocation { get; set; }
    [DataType(DataType.Currency)]
    public float? EventGoGoPrice { get; set; }
}

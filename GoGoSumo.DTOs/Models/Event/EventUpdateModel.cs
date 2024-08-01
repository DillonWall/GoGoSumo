using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Models.Event;

public class EventUpdateModel
{
    [Required]
    public required int EventId { get; set; }
    [MaxLength(100)]
    public string? EventName { get; set; }
    public DateOnly? EventDate { get; set; }
    [MaxLength(255)]
    public string? EventLocation { get; set; }
    [DataType(DataType.Currency)]
    public decimal? EventGoGoPrice { get; set; }
}

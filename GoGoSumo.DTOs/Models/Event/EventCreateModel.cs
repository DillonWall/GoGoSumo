using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Models.Event;

public class EventCreateModel
{
    public readonly int? EventId = null;
    [MaxLength(100)]
    [Required]
    public required string EventName { get; set; }
    [Required]
    public DateOnly? EventDate { get; set; }
    [MaxLength(255)]
    [Required]
    public required string EventLocation { get; set; }
    [DataType(DataType.Currency)]
    [Required]
    public required decimal EventGoGoPrice { get; set; }
}

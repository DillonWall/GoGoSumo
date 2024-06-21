using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Models.Event;

public class EventCreateModel
{
    [MaxLength(100)]
    [Required]
    public string? EventName { get; set; }
    [Required]
    public DateOnly? EventDate { get; set; }
    [MaxLength(255)]
    [Required]
    public string? EventLocation { get; set; }
    [DataType(DataType.Currency)]
    [Required]
    public decimal? EventGoGoPrice { get; set; }
}

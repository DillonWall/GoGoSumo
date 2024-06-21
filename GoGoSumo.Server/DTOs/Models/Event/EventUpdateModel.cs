using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Models.Event;

public class EventUpdateModel
{
    [MaxLength(100)]
    public string? EventName { get; set; }
    public DateOnly? EventDate { get; set; }
    [MaxLength(255)]
    public string? EventLocation { get; set; }
    [DataType(DataType.Currency)]
    public decimal? EventGoGoPrice { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Models.Event;

public class EventModel
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public DateOnly? Date { get; set; }
    public string? Location { get; set; }
    [DataType(DataType.Currency)]
    public float? GoGoPrice { get; set; }
    //tour_guide_id VARCHAR(32)
}

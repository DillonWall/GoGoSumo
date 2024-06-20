using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Models.Event;

public class WeddingEntity
{
    public int? Id { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }
    public DateOnly? Date { get; set; }
    [MaxLength(255)]
    public string? Location { get; set; }
    [DataType(DataType.Currency)]
    public float? GoGoPrice { get; set; }
    [MaxLength(100)]
    public string? BrideName { get; set; }
    [MaxLength(100)]
    public string? GroomName { get; set; }
    [DataType(DataType.Currency)]
    public float? Budget { get; set; }
    [Length(32, 32)]
    public string? PlannerId { get; set; }
}

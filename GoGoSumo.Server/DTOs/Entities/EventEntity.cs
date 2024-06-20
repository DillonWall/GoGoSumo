using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.Models;

public class EventEntity
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public DateOnly? Date { get; set; }
    public string? Location { get; set; }
    [DataType(DataType.Currency)]
    public float? GoGoPrice { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Entities;

public class WeddingEntity
{
    public int? WeddingId { get; set; }
    public EventEntity? Event { get; set; }
    [MaxLength(100)]
    public string? WeddingBrideName { get; set; }
    [MaxLength(100)]
    public string? WeddingGroomName { get; set; }
    [DataType(DataType.Currency)]
    public float? WeddingBudget { get; set; }
    [Length(32, 32)]
    public string? WeddingPlannerId { get; set; }
}

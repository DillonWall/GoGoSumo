using GoGoSumo.Server.Helpers.Mappers;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Entities;

public class WeddingEntity
{
    [Column("wedding_id")]
    public int? WeddingId { get; set; }
    public EventEntity? Event { get; set; }
    [MaxLength(100)]
    [Column("wedding_bride_name")]
    public string? WeddingBrideName { get; set; }
    [MaxLength(100)]
    [Column("wedding_groom_name")]
    public string? WeddingGroomName { get; set; }
    [DataType(DataType.Currency)]
    [Column("wedding_budget")]
    public float? WeddingBudget { get; set; }
    [Length(32, 32)]
    [Column("wedding_planner_id")]
    public string? WeddingPlannerId { get; set; }
}

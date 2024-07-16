using GoGoSumo.DTOs.Models.Event;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Models.Wedding;

public class WeddingCreateModel
{
    [Required]
    public EventCreateModel? Event { get; set; }
    [MaxLength(100)]
    [Required]
    public string? WeddingBrideName { get; set; }
    [MaxLength(100)]
    [Required]
    public string? WeddingGroomName { get; set; }
    [DataType(DataType.Currency)]
    [Required]
    public float? WeddingBudget { get; set; }
    [Length(32, 32)]
    [Required]
    public string? WeddingPlannerId { get; set; }
}

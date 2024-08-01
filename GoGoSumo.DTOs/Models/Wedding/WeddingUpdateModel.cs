using GoGoSumo.DTOs.Models.Event;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Models.Wedding;

public class WeddingUpdateModel
{
    [Required]
    public required int WeddingId { get; set; }
    public EventUpdateModel? Event { get; set; }
    [MaxLength(100)]
    public string? WeddingBrideName { get; set; }
    [MaxLength(100)]
    public string? WeddingGroomName { get; set; }
    [DataType(DataType.Currency)]
    public decimal? WeddingBudget { get; set; }
    [Length(32, 32)]
    public string? WeddingPlannerId { get; set; }
}

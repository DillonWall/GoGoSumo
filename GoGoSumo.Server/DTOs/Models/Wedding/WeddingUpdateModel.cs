using GoGoSumo.Server.DTOs.Models.Event;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Models.Wedding;

public class WeddingUpdateModel
{
    public EventUpdateModel? Event { get; set; }
    [MaxLength(100)]
    public string? WeddingBrideName { get; set; }
    [MaxLength(100)]
    public string? WeddingGroomName { get; set; }
    [DataType(DataType.Currency)]
    public float? WeddingBudget { get; set; }
    [Length(32, 32)]
    public string? WeddingPlannerId { get; set; }
}

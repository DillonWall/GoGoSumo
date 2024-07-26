using GoGoSumo.DTOs.Models.Event;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Models.Wedding;

public class WeddingCreateModel
{
    [Required]
    public required EventCreateModel Event { get; set; }
    [MaxLength(100)]
    [Required]
    public required string WeddingBrideName { get; set; }
    [MaxLength(100)]
    [Required]
    public required string WeddingGroomName { get; set; }
    [DataType(DataType.Currency)]
    [Required]
    public required float WeddingBudget { get; set; }
    [Length(32, 32)]
    [Required]
    public required string WeddingPlannerId { get; set; }
}

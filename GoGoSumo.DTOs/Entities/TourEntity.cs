using GoGoSumo.DTOs.Helpers.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Entities;

public class TourEntity
{
    [Column("tour_id")]
    public int? TourId { get; set; }
    public EventEntity? Event { get; set; }
    [Length(32, 32)]
    [Column("tour_guide_id")]
    public string? TourGuideId { get; set; }
}

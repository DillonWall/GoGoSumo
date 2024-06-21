using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Entities;

public class TourEntity
{
    public int? TourId { get; set; }
    public EventEntity? Event { get; set; }
    [Length(32, 32)]
    public string? TourGuideId { get; set; }
}

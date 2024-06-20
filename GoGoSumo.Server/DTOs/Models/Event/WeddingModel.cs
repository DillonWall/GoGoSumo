using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Models.Event;

public class WeddingModel
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public DateOnly? Date { get; set; }
    public string? Location { get; set; }
    [DataType(DataType.Currency)]
    public float? GoGoPrice { get; set; }

    //   wedding_bride_name VARCHAR(100),
    //   wedding_groom_name VARCHAR(100),
    //   wedding_budget_yen DECIMAL(13, 2),
    //wedding_planner_id VARCHAR(32)
}

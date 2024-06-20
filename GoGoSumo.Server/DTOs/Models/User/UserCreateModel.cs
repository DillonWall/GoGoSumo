using GoGoSumo.Server.Helpers.Annotations;
using GoGoSumo.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Models.User;

public class UserCreateModel
{
    [Required]
    public string? ClerkId { get; set; }
    [Phone]
    [Required]
    public string? Phone { get; set; }
    [EnumDataTypeArray(typeof(Locale))]
    [Required]
    public IEnumerable<string>? FluentLanguages { get; set; }
    [EnumDataType(typeof(UserRole))]
    [Required]
    public string? Role { get; set; }
}

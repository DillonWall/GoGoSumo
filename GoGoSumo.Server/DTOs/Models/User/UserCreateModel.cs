using GoGoSumo.Server.Helpers.Annotations;
using GoGoSumo.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Models.User;

public class UserCreateModel
{
    [Length(32, 32)]
    [Required]
    public string? ClerkId { get; set; }
    [Phone]
    [Required]
    public string? UserPhone { get; set; }
    [EnumDataTypeArray(typeof(Locale))]
    [Required]
    public IEnumerable<string>? UserFluentLanguages { get; set; }
    [EnumDataType(typeof(UserRole))]
    [Required]
    public string? UserRole { get; set; }
}

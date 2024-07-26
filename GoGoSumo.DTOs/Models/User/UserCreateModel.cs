using GoGoSumo.DTOs.Enums;
using GoGoSumo.DTOs.Helpers.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Models.User;

public class UserCreateModel
{
    [Length(32, 32)]
    [Required]
    public required string ClerkId { get; set; }
    [Phone]
    [Required]
    public required string UserPhone { get; set; }
    [EnumDataTypeArray(typeof(Locale))]
    [Required]
    public required IEnumerable<string> UserFluentLanguages { get; set; }
    [EnumDataType(typeof(UserRole))]
    [Required]
    public required string RoleName { get; set; }
}

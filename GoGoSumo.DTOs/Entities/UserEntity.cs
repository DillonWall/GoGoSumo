using GoGoSumo.DTOs.Enums;
using GoGoSumo.DTOs.Helpers.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Entities;

public class UserEntity
{
    [Length(32, 32)]
    [Column("clerk_id")]
    public string? ClerkId { get; set; }
    [Phone]
    [Column("user_phone")]
    public string? UserPhone { get; set; }
    [EnumDataTypeArray(typeof(Locale))]
    [Column("user_fluent_languages")]
    public IEnumerable<string>? UserFluentLanguages { get; set; }
    [EnumDataType(typeof(UserRole))]
    [Column("role_name")]
    public string? RoleName { get; set; }
}

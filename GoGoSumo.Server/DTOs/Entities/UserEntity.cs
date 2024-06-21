using GoGoSumo.Server.Helpers.Annotations;
using GoGoSumo.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.DTOs.Entities;

public class UserEntity
{
    [Length(32, 32)]
    public string? ClerkId { get; set; }
    [Phone]
    public string? UserPhone { get; set; }
    [EnumDataTypeArray(typeof(Locale))]
    public IEnumerable<string>? UserFluentLanguages { get; set; }
    [EnumDataType(typeof(UserRole))]
    public string? UserRole { get; set; }
}

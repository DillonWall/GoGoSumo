using GoGoSumo.Server.Helpers.Annotations;
using GoGoSumo.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.Models;

public class UserEntity
{
    public string? ClerkId { get; set; }
    [Phone]
    public string? Phone { get; set; }
    [EnumDataTypeArray(typeof(Locale))]
    public IEnumerable<string>? FluentLanguages { get; set; }
    [EnumDataType(typeof(UserRole))]
    public string? Role { get; set; }
}

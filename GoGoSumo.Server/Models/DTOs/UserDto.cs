using GoGoSumo.Server.Helpers.Annotations;
using GoGoSumo.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.Models;

public class UserDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? Phone { get; set; }
    public string? PasswordHash { get; set; }
    [EnumDataTypeArray(typeof(Locale))]
    public IEnumerable<string>? FluentLanguages { get; set; }
    [EnumDataType(typeof(UserRole))]
    public string? Role { get; set; }
}

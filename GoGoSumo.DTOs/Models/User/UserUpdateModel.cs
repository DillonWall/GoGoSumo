using GoGoSumo.DTOs.Enums;
using GoGoSumo.DTOs.Helpers.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.DTOs.Models.User;

public class UserUpdateModel
{
    [Phone]
    public string? UserPhone { get; set; }
    [EnumDataTypeArray(typeof(Locale))]
    public IEnumerable<string>? UserFluentLanguages { get; set; }
    [EnumDataType(typeof(UserRole))]
    public string? RoleName { get; set; }
}

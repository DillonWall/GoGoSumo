using GoGoSumo.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.Models.ApiModels;

public class UserModel
{
    [Required]
    public string? ClerkId { get; set; }
    [Required]
    [Phone]
    public string? Phone { get; set; }
    [Required]
    //[EnumDataTypeArray(typeof(Locale))]
    public IEnumerable<string>? FluentLanguages { get; set; }
    [Required]
    [EnumDataType(typeof(UserRole))]
    public string? Role { get; set; }
}

using GoGoSumo.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoGoSumo.Server.Models.ApiModels;

public class UserModel
{
    public int? Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [Phone]
    public string? Phone { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    //[EnumDataTypeArray(typeof(Locale))]
    public IEnumerable<string>? FluentLanguages { get; set; }
    [Required]
    [EnumDataType(typeof(UserRole))]
    public string? Role { get; set; }
}

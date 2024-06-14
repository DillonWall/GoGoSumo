using AutoMapper;
using GoGoSumo.Server.Models;
using GoGoSumo.Server.Models.ApiModels;

namespace GoGoSumo.Server.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // UserModel -> UserDto
        CreateMap<UserModel, UserDto>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    // ignore empty array of fluent languages
                    if (x.DestinationMember.Name == nameof(UserDto.FluentLanguages) && src.FluentLanguages?.Any() == false) return false;

                    // ignore null role
                    if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                    return true;
                }
            ));
    }
}
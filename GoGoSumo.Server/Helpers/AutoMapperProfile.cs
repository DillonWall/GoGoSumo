using AutoMapper;
using GoGoSumo.Server.DTOs.Models.Event;
using GoGoSumo.Server.DTOs.Models.User;
using GoGoSumo.Server.Models;

namespace GoGoSumo.Server.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserCreateModel, UserEntity>();
        CreateMap<UserUpdateModel, UserEntity>();
        CreateMap<EventModel, EventEntity>();
    }
}

// How to deal with custom mapping strategies
//
//.ForAllMembers(x => x.Condition(
//    (src, dest, prop) =>
//    {
//        // ignore both null & empty string properties
//        if (prop == null) return false;
//        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

//        // ignore empty array of fluent languages
//        if (x.DestinationMember.Name == nameof(UserEntity.FluentLanguages) && src.FluentLanguages?.Any() == false) return false;

//        // ignore null role
//        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

//        return true;
//    }
//));
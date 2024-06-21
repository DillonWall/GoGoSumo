using AutoMapper;
using Dapper;
using GoGoSumo.Server.DTOs.Entities;
using GoGoSumo.Server.DTOs.Models.Event;
using GoGoSumo.Server.DTOs.Models.User;
using GoGoSumo.Server.DTOs.Models.Wedding;

namespace GoGoSumo.Server.Helpers.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserCreateModel, UserEntity>();
        CreateMap<UserUpdateModel, UserEntity>();
        CreateMap<EventCreateModel, EventEntity>()
            .ForMember(
                dest => dest.EventDate,
                opt => opt.MapFrom(
                    src => src.EventDate.GetValueOrDefault().ToDateTime(TimeOnly.MinValue)
                )
            );
        CreateMap<EventUpdateModel, EventEntity>()
            .ForMember(
                dest => dest.EventDate,
                opt => opt.MapFrom(
                    src => src.EventDate.GetValueOrDefault().ToDateTime(TimeOnly.MinValue)
                )
            );
        CreateMap<WeddingCreateModel, WeddingEntity>();
        CreateMap<WeddingUpdateModel, WeddingEntity>();

        DapperSetTypeMap<UserEntity>();
        DapperSetTypeMap<EventEntity>();
        DapperSetTypeMap<WeddingEntity>();
    }

    private void DapperSetTypeMap<T>()
    {
        SqlMapper.SetTypeMap(
            typeof(T),
            new ColumnAttributeTypeMapper<T>());
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
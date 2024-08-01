using AutoMapper;
using GoGoSumo.DTOs.Entities;
using GoGoSumo.Server.Helpers;

namespace GoGoSumo.Server.UnitTests.Helpers;
public class AutoMapperProfileTests
{
    [Fact]
    public void AutoMapperProfile_IsValid()
    {
        var config = GetMapperConfig();

        config.AssertConfigurationIsValid();
    }

    private static MapperConfiguration GetMapperConfig()
    {
        return new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
    }

    [Fact]
    public void MapModelToEntity_UserCreate_PopulatesAllFields()
    {
        var mapper = GetMapperConfig().CreateMapper();
        var userCreateModel = UnitTestDTOHelpers.CreateUserCreateModel();
        var expected = UnitTestDTOHelpers.CreateUserEntity();

        var actual = mapper.Map<UserEntity>(userCreateModel);

        Assert.Equivalent(expected, actual, strict: true);
    }

    [Fact]
    public void MapModelToEntity_UserUpdate_PopulatesAllFields()
    {
        var mapper = GetMapperConfig().CreateMapper();
        var userUpdateModel = UnitTestDTOHelpers.CreateUserUpdateModel();
        var expected = UnitTestDTOHelpers.CreateUserEntity();

        var actual = mapper.Map<UserEntity>(userUpdateModel);

        Assert.Equivalent(expected, actual, strict: true);
    }

    [Fact]
    public void MapModelToEntity_EventCreate_PopulatesAllFields()
    {
        var mapper = GetMapperConfig().CreateMapper();
        var eventCreateModel = UnitTestDTOHelpers.CreateEventCreateModel();
        var expected = UnitTestDTOHelpers.CreateEventEntity();
        expected.EventId = null;

        var actual = mapper.Map<EventEntity>(eventCreateModel);

        Assert.Equivalent(expected, actual, strict: true);
    }

    [Fact]
    public void MapModelToEntity_EventUpdate_PopulatesAllFields()
    {
        var mapper = GetMapperConfig().CreateMapper();
        var eventUpdateModel = UnitTestDTOHelpers.CreateEventUpdateModel();
        var expected = UnitTestDTOHelpers.CreateEventEntity();

        var actual = mapper.Map<EventEntity>(eventUpdateModel);

        Assert.Equivalent(expected, actual, strict: true);
    }

    [Fact]
    public void MapModelToEntity_WeddingCreate_PopulatesAllFields()
    {
        var mapper = GetMapperConfig().CreateMapper();
        var weddingCreateModel = UnitTestDTOHelpers.CreateWeddingCreateModel();
        var expected = UnitTestDTOHelpers.CreateWeddingEntity();
        expected.WeddingId = null;
        expected.Event!.EventId = null;

        var actual = mapper.Map<WeddingEntity>(weddingCreateModel);

        Assert.Equivalent(expected, actual, strict: true);
    }

    [Fact]
    public void MapModelToEntity_WeddingUpdate_PopulatesAllFields()
    {
        var mapper = GetMapperConfig().CreateMapper();
        var weddingUpdateModel = UnitTestDTOHelpers.CreateWeddingUpdateModel();
        var expected = UnitTestDTOHelpers.CreateWeddingEntity();

        var actual = mapper.Map<WeddingEntity>(weddingUpdateModel);

        Assert.Equivalent(expected, actual, strict: true);
    }
}
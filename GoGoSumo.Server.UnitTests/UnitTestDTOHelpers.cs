using GoGoSumo.DTOs.Entities;
using GoGoSumo.DTOs.Models.Event;
using GoGoSumo.DTOs.Models.User;
using GoGoSumo.DTOs.Models.Wedding;

namespace GoGoSumo.Server.UnitTests;

public static class UnitTestDTOHelpers
{
    public const int EXAMPLE_INTEGER_ID = 1;
    public const string EXAMPLE_CLERK_ID = "11112222333344441111222233334444";
    public const string EXAMPLE_PHONE_NUMBER = "123-456-7890";
    public const string EXAMPLE_LOCALE = "EN";
    public const string EXAMPLE_USER_ROLE = "Customer";
    public const string EXAMPLE_PERSON_NAME = "Joe";
    public const string EXAMPLE_EVENT_NAME = "Some wedding";
    public const string EXAMPLE_EVENT_LOCATION = "Somewhere";
    public const decimal EXAMPLE_CURRENCY = 100.01m;
    public static readonly DateTime EXAMPLE_DATETIME = new DateTime();
    public static readonly DateOnly EXAMPLE_DATEONLY = new DateOnly();

    public static UserEntity CreateUserEntity()
    {
        return new UserEntity
        {
            ClerkId = EXAMPLE_CLERK_ID,
            UserPhone = EXAMPLE_PHONE_NUMBER,
            UserFluentLanguages = [EXAMPLE_LOCALE],
            RoleName = EXAMPLE_USER_ROLE,
        };
    }

    public static UserCreateModel CreateUserCreateModel()
    {
        return new UserCreateModel
        {
            ClerkId = EXAMPLE_CLERK_ID,
            UserPhone = EXAMPLE_PHONE_NUMBER,
            UserFluentLanguages = [EXAMPLE_LOCALE],
            RoleName = EXAMPLE_USER_ROLE,
        };
    }

    public static UserUpdateModel CreateUserUpdateModel()
    {
        return new UserUpdateModel
        {
            ClerkId = EXAMPLE_CLERK_ID,
            UserPhone = EXAMPLE_PHONE_NUMBER,
            UserFluentLanguages = [EXAMPLE_LOCALE],
            RoleName = EXAMPLE_USER_ROLE,
        };
    }

    public static WeddingEntity CreateWeddingEntity()
    {
        return new WeddingEntity
        {
            WeddingId = EXAMPLE_INTEGER_ID,
            Event = CreateEventEntity(),
            WeddingBrideName = EXAMPLE_PERSON_NAME,
            WeddingGroomName = EXAMPLE_PERSON_NAME,
            WeddingBudget = EXAMPLE_CURRENCY,
            WeddingPlannerId = EXAMPLE_CLERK_ID,
        };
    }

    public static WeddingCreateModel CreateWeddingCreateModel()
    {
        return new WeddingCreateModel
        {
            Event = CreateEventCreateModel(),
            WeddingBrideName = EXAMPLE_PERSON_NAME,
            WeddingGroomName = EXAMPLE_PERSON_NAME,
            WeddingBudget = EXAMPLE_CURRENCY,
            WeddingPlannerId = EXAMPLE_CLERK_ID,
        };
    }

    public static WeddingUpdateModel CreateWeddingUpdateModel()
    {
        return new WeddingUpdateModel
        {
            WeddingId = EXAMPLE_INTEGER_ID,
            Event = CreateEventUpdateModel(),
            WeddingBrideName = EXAMPLE_PERSON_NAME,
            WeddingGroomName = EXAMPLE_PERSON_NAME,
            WeddingBudget = EXAMPLE_CURRENCY,
            WeddingPlannerId = EXAMPLE_CLERK_ID,
        };
    }

    public static EventEntity CreateEventEntity()
    {
        return new EventEntity
        {
            EventId = EXAMPLE_INTEGER_ID,
            EventName = EXAMPLE_EVENT_NAME,
            EventDate = EXAMPLE_DATETIME,
            EventLocation = EXAMPLE_EVENT_LOCATION,
            EventGoGoPrice = EXAMPLE_CURRENCY,
        };
    }

    public static EventCreateModel CreateEventCreateModel()
    {
        return new EventCreateModel
        {
            EventName = EXAMPLE_EVENT_NAME,
            EventDate = EXAMPLE_DATEONLY,
            EventLocation = EXAMPLE_EVENT_LOCATION,
            EventGoGoPrice = EXAMPLE_CURRENCY,
        };
    }

    public static EventUpdateModel CreateEventUpdateModel()
    {
        return new EventUpdateModel
        {
            EventId = EXAMPLE_INTEGER_ID,
            EventName = EXAMPLE_EVENT_NAME,
            EventDate = EXAMPLE_DATEONLY,
            EventLocation = EXAMPLE_EVENT_LOCATION,
            EventGoGoPrice = EXAMPLE_CURRENCY,
        };
    }
}

using GoGoSumo.DTOs.Entities;
using GoGoSumo.DTOs.Models.User;
using GoGoSumo.Server.Controllers;
using GoGoSumo.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GoGoSumo.Server.UnitTests.Controllers;
public class UserControllerTests
{
    private const string EXAMPLE_CLERK_ID = "11112222333344441111222233334444";
    private const string EXAMPLE_PHONE_NUMBER = "123-456-7890";
    private const string EXAMPLE_LOCALE = "EN";
    private const string EXAMPLE_ROLE = "Customer";

    [Fact]
    public async Task GetAll_ReturnsListOfUserEntity()
    {
        // Arrange
        var mockUserService = GetMockUserService_WhereGetAllReturns3Users();
        var userController = new UserController(mockUserService.Object);

        // Act
        var okObjectResult = await userController.GetAll() as OkObjectResult;
        var users = okObjectResult!.Value as List<UserEntity>;

        // Assert
        Assert.Equal(3, users!.Count);
    }

    private Mock<IUserService> GetMockUserService_WhereGetAllReturns3Users()
    {
        Mock<IUserService> mockUserService = new Mock<IUserService>();

        mockUserService
            .Setup(session => session.GetAll())
            .ReturnsAsync(CreateListOf3Users());

        return mockUserService;
    }

    private List<UserEntity> CreateListOf3Users()
    {
        var users = new List<UserEntity>
        {
            CreateUser(),
            CreateUser(),
            CreateUser(),
        };

        return users;
    }

    private UserEntity CreateUser()
    {
        return new UserEntity
        {
            ClerkId = EXAMPLE_CLERK_ID,
            UserPhone = EXAMPLE_PHONE_NUMBER,
            UserFluentLanguages = new string[] { EXAMPLE_LOCALE },
            RoleName = EXAMPLE_ROLE,
        };
    }

    [Fact]
    public async Task GetById_WhenGivenId_ReturnsSingleEntity()
    {
        // Arrange
        var mockUserService = GetMockUserService_WhereGetByIdReturnsUser();
        var userController = new UserController(mockUserService.Object);
        var id = EXAMPLE_CLERK_ID;

        // Act
        var okObjectResult = await userController.GetById(id) as OkObjectResult;
        var user = okObjectResult!.Value as UserEntity;

        // Assert
        Assert.NotNull(user);
    }

    private Mock<IUserService> GetMockUserService_WhereGetByIdReturnsUser()
    {
        Mock<IUserService> mockUserService = new Mock<IUserService>();

        mockUserService
            .Setup(session => session.GetById(It.IsAny<string>()))
            .ReturnsAsync(CreateUser());

        return mockUserService;
    }

    [Fact]
    public async Task Create_WhenGivenModel_ReturnsUserCreated()
    {
        // Arrange
        var userCreateModel = CreateUserCreateModel();
        var mockUserService = GetMockUserService_WhereCreateDoesNothing();
        var userController = new UserController(mockUserService.Object);

        // Act
        var okObjectResult = await userController.Create(userCreateModel) as OkObjectResult;
        var resultObject = okObjectResult!.Value!;
        var message = resultObject.GetPropertyByName<string>("message");

        // Assert
        Assert.Equal("User created", message);
    }

    [Fact]
    public async Task Create_WhenGivenModel_CallsUserServiceCreateOnce()
    {
        // Arrange
        var userCreateModel = CreateUserCreateModel();
        var mockUserService = GetMockUserService_WhereCreateDoesNothing();
        var userController = new UserController(mockUserService.Object);

        // Act
        await userController.Create(userCreateModel);

        // Assert
        mockUserService.Verify(service => service.Create(userCreateModel), Times.Once());
    }

    private UserCreateModel CreateUserCreateModel()
    {
        return new UserCreateModel
        {
            ClerkId = EXAMPLE_CLERK_ID,
            UserPhone = EXAMPLE_PHONE_NUMBER,
            UserFluentLanguages = new string[] { EXAMPLE_LOCALE },
            RoleName = EXAMPLE_ROLE,
        };
    }

    private Mock<IUserService> GetMockUserService_WhereCreateDoesNothing()
    {
        Mock<IUserService> mockUserService = new Mock<IUserService>();

        mockUserService
            .Setup(session => session.Create(It.IsAny<UserCreateModel>()))
            .Verifiable();

        return mockUserService;
    }

    [Fact]
    public async Task Update_WhenGivenModel_ReturnsUserUpdated()
    {
        // Arrange
        var userUpdateModel = CreateUserUpdateModel();
        var mockUserService = GetMockUserService_WhereUpdateDoesNothing();
        var userController = new UserController(mockUserService.Object);

        // Act
        var okObjectResult = await userController.Update(userUpdateModel) as OkObjectResult;
        var resultObject = okObjectResult!.Value!;
        var message = resultObject.GetPropertyByName<string>("message");

        // Assert
        Assert.Equal("User updated", message);
    }

    [Fact]
    public async Task Update_WhenGivenModel_CallsUserServiceUpdateOnce()
    {
        // Arrange
        var userUpdateModel = CreateUserUpdateModel();
        var mockUserService = GetMockUserService_WhereUpdateDoesNothing();
        var userController = new UserController(mockUserService.Object);

        // Act
        await userController.Update(userUpdateModel);

        // Assert
        mockUserService.Verify(service => service.Update(userUpdateModel), Times.Once());
    }

    private UserUpdateModel CreateUserUpdateModel()
    {
        return new UserUpdateModel
        {
            ClerkId = EXAMPLE_CLERK_ID,
            UserPhone = EXAMPLE_PHONE_NUMBER,
            UserFluentLanguages = new string[] { EXAMPLE_LOCALE },
            RoleName = EXAMPLE_ROLE,
        };
    }

    private Mock<IUserService> GetMockUserService_WhereUpdateDoesNothing()
    {
        Mock<IUserService> mockUserService = new Mock<IUserService>();

        mockUserService
            .Setup(session => session.Update(It.IsAny<UserUpdateModel>()))
            .Verifiable();

        return mockUserService;
    }

    [Fact]
    public async Task Delete_WhenGivenModel_ReturnsUserDeleted()
    {
        // Arrange
        var mockUserService = GetMockUserService_WhereDeleteDoesNothing();
        var userController = new UserController(mockUserService.Object);
        var id = EXAMPLE_CLERK_ID;

        // Act
        var okObjectResult = await userController.Delete(id) as OkObjectResult;
        var resultObject = okObjectResult!.Value!;
        var message = resultObject.GetPropertyByName<string>("message");


        // Assert
        Assert.Equal("User deleted", message);
    }

    [Fact]
    public async Task Delete_WhenGivenModel_CallsUserServiceDeleteOnce()
    {
        // Arrange
        var mockUserService = GetMockUserService_WhereDeleteDoesNothing();
        var userController = new UserController(mockUserService.Object);
        var id = EXAMPLE_CLERK_ID;

        // Act
        await userController.Delete(id);

        // Assert
        mockUserService.Verify(service => service.Delete(id), Times.Once());
    }

    private Mock<IUserService> GetMockUserService_WhereDeleteDoesNothing()
    {
        Mock<IUserService> mockUserService = new Mock<IUserService>();

        mockUserService
            .Setup(session => session.Delete(It.IsAny<string>()))
            .Verifiable();

        return mockUserService;
    }
}

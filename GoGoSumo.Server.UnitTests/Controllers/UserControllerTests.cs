using GoGoSumo.DTOs.Entities;
using GoGoSumo.DTOs.Models.User;
using GoGoSumo.Server.Controllers;
using GoGoSumo.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GoGoSumo.Server.UnitTests.Controllers;
public class UserControllerTests
{

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
            UnitTestDTOHelpers.CreateUserEntity(),
            UnitTestDTOHelpers.CreateUserEntity(),
            UnitTestDTOHelpers.CreateUserEntity(),
        };

        return users;
    }

    [Fact]
    public async Task GetById_WhenGivenId_ReturnsSingleEntity()
    {
        // Arrange
        var mockUserService = GetMockUserService_WhereGetByIdReturnsUser();
        var userController = new UserController(mockUserService.Object);
        var id = UnitTestDTOHelpers.EXAMPLE_CLERK_ID;

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
            .ReturnsAsync(UnitTestDTOHelpers.CreateUserEntity());

        return mockUserService;
    }

    [Fact]
    public async Task Create_WhenGivenModel_ReturnsUserCreated()
    {
        // Arrange
        var userCreateModel = UnitTestDTOHelpers.CreateUserCreateModel();
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
        var userCreateModel = UnitTestDTOHelpers.CreateUserCreateModel();
        var mockUserService = GetMockUserService_WhereCreateDoesNothing();
        var userController = new UserController(mockUserService.Object);

        // Act
        await userController.Create(userCreateModel);

        // Assert
        mockUserService.Verify(service => service.Create(userCreateModel), Times.Once());
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
        var userUpdateModel = UnitTestDTOHelpers.CreateUserUpdateModel();
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
        var userUpdateModel = UnitTestDTOHelpers.CreateUserUpdateModel();
        var mockUserService = GetMockUserService_WhereUpdateDoesNothing();
        var userController = new UserController(mockUserService.Object);

        // Act
        await userController.Update(userUpdateModel);

        // Assert
        mockUserService.Verify(service => service.Update(userUpdateModel), Times.Once());
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
        var id = UnitTestDTOHelpers.EXAMPLE_CLERK_ID;

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
        var id = UnitTestDTOHelpers.EXAMPLE_CLERK_ID;

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

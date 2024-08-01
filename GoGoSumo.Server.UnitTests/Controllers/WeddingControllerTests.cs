using GoGoSumo.DTOs.Entities;
using GoGoSumo.DTOs.Models.Wedding;
using GoGoSumo.Server.Controllers;
using GoGoSumo.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GoGoSumo.Server.UnitTests.Controllers;
public class WeddingControllerTests
{

    [Fact]
    public async Task GetAll_ReturnsListOfWeddingEntity()
    {
        // Arrange
        var mockWeddingService = GetMockWeddingService_WhereGetAllReturns3Weddings();
        var weddingController = new WeddingController(mockWeddingService.Object);

        // Act
        var okObjectResult = await weddingController.GetAll() as OkObjectResult;
        var weddings = okObjectResult!.Value as List<WeddingEntity>;

        // Assert
        Assert.Equal(3, weddings!.Count);
    }

    private Mock<IWeddingService> GetMockWeddingService_WhereGetAllReturns3Weddings()
    {
        Mock<IWeddingService> mockWeddingService = new Mock<IWeddingService>();

        mockWeddingService
            .Setup(session => session.GetAll())
            .ReturnsAsync(CreateListOf3Weddings());

        return mockWeddingService;
    }

    private List<WeddingEntity> CreateListOf3Weddings()
    {
        var weddings = new List<WeddingEntity>
        {
            UnitTestDTOHelpers.CreateWeddingEntity(),
            UnitTestDTOHelpers.CreateWeddingEntity(),
            UnitTestDTOHelpers.CreateWeddingEntity(),
        };

        return weddings;
    }

    [Fact]
    public async Task GetById_WhenGivenId_ReturnsSingleEntity()
    {
        // Arrange
        var mockWeddingService = GetMockWeddingService_WhereGetByIdReturnsWedding();
        var weddingController = new WeddingController(mockWeddingService.Object);
        var id = UnitTestDTOHelpers.EXAMPLE_INTEGER_ID;

        // Act
        var okObjectResult = await weddingController.GetById(id) as OkObjectResult;
        var wedding = okObjectResult!.Value as WeddingEntity;

        // Assert
        Assert.NotNull(wedding);
    }

    private Mock<IWeddingService> GetMockWeddingService_WhereGetByIdReturnsWedding()
    {
        Mock<IWeddingService> mockWeddingService = new Mock<IWeddingService>();

        mockWeddingService
            .Setup(session => session.GetById(It.IsAny<int>()))
            .ReturnsAsync(UnitTestDTOHelpers.CreateWeddingEntity());

        return mockWeddingService;
    }

    [Fact]
    public async Task Create_WhenGivenModel_ReturnsWeddingCreated()
    {
        // Arrange
        var weddingCreateModel = UnitTestDTOHelpers.CreateWeddingCreateModel();
        var mockWeddingService = GetMockWeddingService_WhereCreateDoesNothing();
        var weddingController = new WeddingController(mockWeddingService.Object);

        // Act
        var okObjectResult = await weddingController.Create(weddingCreateModel) as OkObjectResult;
        var resultObject = okObjectResult!.Value!;
        var message = resultObject.GetPropertyByName<string>("message");

        // Assert
        Assert.Equal("Wedding created", message);
    }

    [Fact]
    public async Task Create_WhenGivenModel_CallsWeddingServiceCreateOnce()
    {
        // Arrange
        var weddingCreateModel = UnitTestDTOHelpers.CreateWeddingCreateModel();
        var mockWeddingService = GetMockWeddingService_WhereCreateDoesNothing();
        var weddingController = new WeddingController(mockWeddingService.Object);

        // Act
        await weddingController.Create(weddingCreateModel);

        // Assert
        mockWeddingService.Verify(service => service.Create(weddingCreateModel), Times.Once());
    }

    private Mock<IWeddingService> GetMockWeddingService_WhereCreateDoesNothing()
    {
        Mock<IWeddingService> mockWeddingService = new Mock<IWeddingService>();

        mockWeddingService
            .Setup(session => session.Create(It.IsAny<WeddingCreateModel>()))
            .Verifiable();

        return mockWeddingService;
    }

    [Fact]
    public async Task Update_WhenGivenModel_ReturnsWeddingUpdated()
    {
        // Arrange
        var weddingUpdateModel = UnitTestDTOHelpers.CreateWeddingUpdateModel();
        var mockWeddingService = GetMockWeddingService_WhereUpdateDoesNothing();
        var weddingController = new WeddingController(mockWeddingService.Object);

        // Act
        var okObjectResult = await weddingController.Update(weddingUpdateModel) as OkObjectResult;
        var resultObject = okObjectResult!.Value!;
        var message = resultObject.GetPropertyByName<string>("message");

        // Assert
        Assert.Equal("Wedding updated", message);
    }

    [Fact]
    public async Task Update_WhenGivenModel_CallsWeddingServiceUpdateOnce()
    {
        // Arrange
        var weddingUpdateModel = UnitTestDTOHelpers.CreateWeddingUpdateModel();
        var mockWeddingService = GetMockWeddingService_WhereUpdateDoesNothing();
        var weddingController = new WeddingController(mockWeddingService.Object);

        // Act
        await weddingController.Update(weddingUpdateModel);

        // Assert
        mockWeddingService.Verify(service => service.Update(weddingUpdateModel), Times.Once());
    }

    private Mock<IWeddingService> GetMockWeddingService_WhereUpdateDoesNothing()
    {
        Mock<IWeddingService> mockWeddingService = new Mock<IWeddingService>();

        mockWeddingService
            .Setup(session => session.Update(It.IsAny<WeddingUpdateModel>()))
            .Verifiable();

        return mockWeddingService;
    }

    [Fact]
    public async Task Delete_WhenGivenModel_ReturnsWeddingDeleted()
    {
        // Arrange
        var mockWeddingService = GetMockWeddingService_WhereDeleteDoesNothing();
        var weddingController = new WeddingController(mockWeddingService.Object);
        var id = UnitTestDTOHelpers.EXAMPLE_INTEGER_ID;

        // Act
        var okObjectResult = await weddingController.Delete(id) as OkObjectResult;
        var resultObject = okObjectResult!.Value!;
        var message = resultObject.GetPropertyByName<string>("message");


        // Assert
        Assert.Equal("Wedding deleted", message);
    }

    [Fact]
    public async Task Delete_WhenGivenModel_CallsWeddingServiceDeleteOnce()
    {
        // Arrange
        var mockWeddingService = GetMockWeddingService_WhereDeleteDoesNothing();
        var weddingController = new WeddingController(mockWeddingService.Object);
        var id = UnitTestDTOHelpers.EXAMPLE_INTEGER_ID;

        // Act
        await weddingController.Delete(id);

        // Assert
        mockWeddingService.Verify(service => service.Delete(id), Times.Once());
    }

    private Mock<IWeddingService> GetMockWeddingService_WhereDeleteDoesNothing()
    {
        Mock<IWeddingService> mockWeddingService = new Mock<IWeddingService>();

        mockWeddingService
            .Setup(session => session.Delete(It.IsAny<int>()))
            .Verifiable();

        return mockWeddingService;
    }
}

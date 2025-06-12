using Azure;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Application.RequestDtos;
using Thunders.TechTest.ApiService.Application.Service.Interfaces;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.Tests.Controller;
public class TollPassageControllerTest
{
    private readonly AutoMocker _autoMock;

    public TollPassageControllerTest()
    {
        _autoMock = new AutoMocker();
    }

    [Fact]
    public async Task GenerateTollPassageAsync_ShoulReturn_Status202Accepted()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<CreateTollPassagePostRequest>()
            .RuleFor(t => t.Plaza, f => $"PLAZA-{f.Random.Number(0001, 9999)}")
            .RuleFor(t => t.City, f => f.Address.City())
            .RuleFor(t => t.State, f => f.Address.StateAbbr())
            .RuleFor(t => t.AmountPaid, f => f.Random.Decimal(2.50m, 25.00m))
            .RuleFor(t => t.VehicleType, f => f.PickRandom<VehicleType>());

        _autoMock.GetMock<ITollPassageService>()
            .Setup(m => m.RecordTollUsageAsync(It.IsAny<TollPassageDto>()))
            .Returns(Task.CompletedTask);

        //Act 
        var result = await controller.GenerateTollPassageAsync(fakeRequest);

        //Assert
        Assert.Multiple(() =>
        {
            var Response = Assert.IsType<AcceptedResult>(result);
            Assert.Equal(StatusCodes.Status202Accepted, Response.StatusCode);
        });
    }

    [Fact]
    public async Task GenerateTollPassageAsync_ShoulReturn_BadRequest()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<CreateTollPassagePostRequest>()
            .RuleFor(t => t.City, f => f.Address.City())
            .RuleFor(t => t.State, f => f.Address.StateAbbr())
            .RuleFor(t => t.AmountPaid, f => f.Random.Decimal(2.50m, 25.00m))
            .RuleFor(t => t.VehicleType, f => f.PickRandom<VehicleType>());

        _autoMock.GetMock<ITollPassageService>()
            .Setup(m => m.RecordTollUsageAsync(It.IsAny<TollPassageDto>()))
            .ThrowsAsync(new ArgumentException(It.IsAny<string>()));

        //Act 
        var result = await controller.GenerateTollPassageAsync(fakeRequest);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GenerateTollPassageAsync_ShoulReturn_ReturnsInternalServerError()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<CreateTollPassagePostRequest>()
            .RuleFor(t => t.Plaza, f => $"PLAZA-{f.Random.Number(0001, 9999)}")
            .RuleFor(t => t.City, f => f.Address.City())
            .RuleFor(t => t.State, f => f.Address.StateAbbr())
            .RuleFor(t => t.AmountPaid, f => f.Random.Decimal(2.50m, 25.00m))
            .RuleFor(t => t.VehicleType, f => f.PickRandom<VehicleType>());

        _autoMock.GetMock<ITollPassageService>()
            .Setup(m => m.RecordTollUsageAsync(It.IsAny<TollPassageDto>()))
            .ThrowsAsync(new Exception());

        //Act 
        var result = await controller.GenerateTollPassageAsync(fakeRequest);

        //Assert
        Assert.Multiple(() =>
        {
            var Response = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, Response.StatusCode);
        });
    }

    private TollPassageController CreateController()
    {
        var controller = new TollPassageController(
                _autoMock.GetMock<ITollPassageService>().Object);
        return controller;
    }
}

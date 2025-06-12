using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Application.InputModels;
using Thunders.TechTest.ApiService.Application.RequestDtos;
using Thunders.TechTest.ApiService.Application.Result;
using Thunders.TechTest.ApiService.Application.Service.Interfaces;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.Tests.Controller;
public class ReportsControllerTest
{
    private readonly AutoMocker _autoMock;

    public ReportsControllerTest()
    {
        _autoMock = new AutoMocker();
    }

    [Fact]
    public async Task GenerateHourlySalesByCityReport_ShoulReturn_Status202Accepted()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<ProcessReporHourlySalesByCityPostRequest>()
            .RuleFor(t => t.StartDate, f => f.Date.Recent(30))
            .RuleFor(t => t.EndDate, f => f.Date.Recent(2));

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GenerateHourlySalesByCityReport(It.IsAny<ProcessReportHourlySalesByCityDto>()))
            .Returns(Task.CompletedTask);

        //Act 
        var result = await controller.GenerateHourlySalesByCityReport(fakeRequest);

        //Assert
        Assert.Multiple(() =>
        {
            var response = Assert.IsType<AcceptedResult>(result);
            Assert.Equal(StatusCodes.Status202Accepted, response.StatusCode);
        });
    }

    [Fact]
    public async Task GenerateHourlySalesByCityReport_ShoulReturn_BadRequest()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<ProcessReporHourlySalesByCityPostRequest>()
            .RuleFor(t => t.EndDate, f => f.Date.Recent(2));

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GenerateHourlySalesByCityReport(It.IsAny<ProcessReportHourlySalesByCityDto>()))
            .ThrowsAsync(new ArgumentException(It.IsAny<string>()));

        //Act 
        var result = await controller.GenerateHourlySalesByCityReport(fakeRequest);


        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GenerateHourlySalesByCityReport_ShoulReturn_ReturnsInternalServerError()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<ProcessReporHourlySalesByCityPostRequest>()
            .RuleFor(t => t.StartDate, f => f.Date.Recent(30))
            .RuleFor(t => t.EndDate, f => f.Date.Recent(2));

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GenerateHourlySalesByCityReport(It.IsAny<ProcessReportHourlySalesByCityDto>()))
            .ThrowsAsync(new Exception());

        //Act 
        var result = await controller.GenerateHourlySalesByCityReport(fakeRequest);


        //Assert
        Assert.Multiple(() =>
        {
            var Response = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, Response.StatusCode);
        });
    }

    [Fact]
    public async Task GenerateTopCitiesByRevenueReport_ShoulReturn_Status202Accepted()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<ProcessReporTopCitiesByRevenuePostRequest>()
            .RuleFor(t => t.Month, f => f.Random.Number(1, 10))
            .RuleFor(t => t.Year, f => f.Random.Number(1, 10))
            .RuleFor(t => t.TopCount, f => f.Random.Number(1, 10));

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GenerateTopCitiesByRevenueReport(It.IsAny<ProcessReportTopCitiesByRevenueDto>()))
            .Returns(Task.CompletedTask);

        //Act 
        var result = await controller.GenerateTopCitiesByRevenueReport(fakeRequest);

        //Assert
        Assert.Multiple(() =>
        {
            var response = Assert.IsType<AcceptedResult>(result);
            Assert.Equal(StatusCodes.Status202Accepted, response.StatusCode);
        });
    }

    [Fact]
    public async Task GenerateTopCitiesByRevenueReport_ShoulReturn_BadRequest()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<ProcessReporTopCitiesByRevenuePostRequest>()
           .RuleFor(t => t.Month, f => f.Random.Number(1, 10))
           .RuleFor(t => t.TopCount, f => f.Random.Number(1, 10));

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GenerateTopCitiesByRevenueReport(It.IsAny<ProcessReportTopCitiesByRevenueDto>()))
            .ThrowsAsync(new ArgumentException(It.IsAny<string>()));

        //Act 
        var result = await controller.GenerateTopCitiesByRevenueReport(fakeRequest);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GenerateTopCitiesByRevenueReport_ShoulReturn_ReturnsInternalServerError()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<ProcessReporTopCitiesByRevenuePostRequest>()
            .RuleFor(t => t.Month, f => f.Random.Number(1, 10))
            .RuleFor(t => t.Year, f => f.Random.Number(1, 10))
            .RuleFor(t => t.TopCount, f => f.Random.Number(1, 10));

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GenerateTopCitiesByRevenueReport(It.IsAny<ProcessReportTopCitiesByRevenueDto>()))
            .ThrowsAsync(new Exception());

        //Act 
        var result = await controller.GenerateTopCitiesByRevenueReport(fakeRequest);


        //Assert
        Assert.Multiple(() =>
        {
            var Response = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, Response.StatusCode);
        });
    }

    [Fact]
    public async Task GenerateVehicleTypesByPlazaReport_ShoulReturn_Status202Accepted()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<ProcessReporVehicleTypesByPlazaPostRequest>()
            .RuleFor(t => t.Plaza, f => $"PLAZA-{f.Random.Number(0001, 9999)}");

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GenerateVehicleTypesByPlazaReport(It.IsAny<ProcessRepostVehicleTypesByPlazaDto>()))
            .Returns(Task.CompletedTask);

        //Act 
        var result = await controller.GenerateVehicleTypesByPlazaReport(fakeRequest);

        //Assert
        Assert.Multiple(() =>
        {
            var response = Assert.IsType<AcceptedResult>(result);
            Assert.Equal(StatusCodes.Status202Accepted, response.StatusCode);
        });
    }

    [Fact]
    public async Task GenerateVehicleTypesByPlazaReport_ShoulReturn_BadRequest()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<ProcessReporVehicleTypesByPlazaPostRequest>()
            .Generate();

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GenerateVehicleTypesByPlazaReport(It.IsAny<ProcessRepostVehicleTypesByPlazaDto>()))
            .ThrowsAsync(new ArgumentException(It.IsAny<string>()));

        //Act 
        var result = await controller.GenerateVehicleTypesByPlazaReport(fakeRequest);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GenerateVehicleTypesByPlazaReport_ShoulReturn_ReturnsInternalServerError()
    {
        //Arrange
        var controller = CreateController();
        var fakeRequest = new Faker<ProcessReporVehicleTypesByPlazaPostRequest>()
            .RuleFor(t => t.Plaza, f => $"PLAZA-{f.Random.Number(0001, 9999)}");

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GenerateVehicleTypesByPlazaReport(It.IsAny<ProcessRepostVehicleTypesByPlazaDto>()))
            .ThrowsAsync(new Exception());

        //Act 
        var result = await controller.GenerateVehicleTypesByPlazaReport(fakeRequest);


        //Assert
        Assert.Multiple(() =>
        {
            var Response = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, Response.StatusCode);
        });
    }

    [Fact]
    public async Task GetReportByTypeAsync_ShoulReturn_Status200Ok()
    {
        //Arrange
        var controller = CreateController();
        var fakeReportType = (int)ReportType.HourlySalesByCity;
        var result = new Faker<ProcessReportResult>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(t => t.Data, f => $"Fake data")
            .RuleFor(t => t.ReportType, f => f.PickRandom<ReportType>())
            .RuleFor(t => t.CreateDate, f => f.Date.Recent());

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GetReportResultByTypeAsync(It.IsAny<ReportType>()))
            .ReturnsAsync(result);

        //Act 
        var response = await controller.GetReportByTypeAsync(fakeReportType);

        //Assert
        Assert.Multiple(() =>
        {
            var result = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(result.Value);
        });
    }

    [Fact]
    public async Task GetReportByTypeAsync_ShoulReturn_Status204NoContent()
    {
        //Arrange
        var controller = CreateController();
        var fakeReportType = (int)ReportType.HourlySalesByCity;

        _autoMock.GetMock<IProcessReportService>()
            .Setup(m => m.GetReportResultByTypeAsync(It.IsAny<ReportType>()))
         .ReturnsAsync((ProcessReportResult?)null);

        //Act 
        var response = await controller.GetReportByTypeAsync(fakeReportType);

        //Assert
        Assert.Multiple(() =>
        {
            var result = Assert.IsType<NoContentResult>(response);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        });
    }

    private ReportsController CreateController()
    {
        var controller = new ReportsController(
                _autoMock.GetMock<IProcessReportService>().Object);
        return controller;
    }
}

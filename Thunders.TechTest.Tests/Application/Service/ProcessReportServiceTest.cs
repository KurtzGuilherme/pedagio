using Microsoft.Extensions.Logging;
using Moq;
using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Application.Messaging.Messages;
using Thunders.TechTest.ApiService.Application.Service;
using Thunders.TechTest.ApiService.Infra.Repository.Interface;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.Tests.Application.Service;
public class ProcessReportServiceTest
{
    private readonly Mock<IMessageSender> _mockMessageSender;
    private readonly Mock<ILogger<ProcessReportService>> _mockLogger;
    private readonly Mock<IProcessReportRepository> _mockProcessReportRepository;

    public ProcessReportServiceTest()
    {
        _mockMessageSender = new Mock<IMessageSender>();
        _mockLogger = new Mock<ILogger<ProcessReportService>>();
        _mockProcessReportRepository = new Mock<IProcessReportRepository>();
    }

    [Fact]
    public async Task GenerateHourlySalesByCityReport_ShouldCall_SendLocal()
    {
        //Arrange
        var service = CreateService();
        var fakeDto  = new ProcessReportHourlySalesByCityDto(DateTime.Now.AddDays(-10), DateTime.Now);

        //Act
        await service.GenerateHourlySalesByCityReport(fakeDto);

        //Assert
        _mockMessageSender.Verify(v => v.SendLocal(It.IsAny<ProcessReportMessage>()), Times.Once);
    }

    [Fact]
    public async Task GenerateTopCitiesByRevenueReport_ShouldCall_SendLocal()
    {
        //Arrange
        var service = CreateService();
        var fakeDto = new ProcessReportTopCitiesByRevenueDto(5, 2025, 10);

        //Act
        await service.GenerateTopCitiesByRevenueReport(fakeDto);

        //Assert
        _mockMessageSender.Verify(v => v.SendLocal(It.IsAny<ProcessReportMessage>()), Times.Once);
    }

    [Fact]
    public async Task GenerateVehicleTypesByPlazaReport_ShouldCall_SendLocal()
    {
        //Arrange
        var service = CreateService();
        var fakeDto = new ProcessRepostVehicleTypesByPlazaDto("fakePlaza");

        //Act
        await service.GenerateVehicleTypesByPlazaReport(fakeDto);

        //Assert
        _mockMessageSender.Verify(v => v.SendLocal(It.IsAny<ProcessReportMessage>()), Times.Once);
    }

    public ProcessReportService CreateService()
        => new ProcessReportService(
            _mockMessageSender.Object, 
            _mockLogger.Object, 
            _mockProcessReportRepository.Object);


}

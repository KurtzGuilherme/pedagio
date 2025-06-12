using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Application.Messaging.Messages;
using Thunders.TechTest.ApiService.Application.RequestDtos;
using Thunders.TechTest.ApiService.Application.Service;
using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.Tests.Application.Service;
public class TollPassageServiceTest
{
    private readonly Mock<IMessageSender> _mockMessageSender;
    private readonly Mock<ILogger<TollPassageService>> _logger;
    public TollPassageServiceTest()
    {
        _mockMessageSender = new Mock<IMessageSender>();
        _logger = new Mock<ILogger<TollPassageService>>();
    }

    [Fact]
    public async Task RecordTollUsageAsync_ShouldCall_SendLocal()
    {
        //Arrange
        var service = CreateService();
        var fakeDto = new TollPassageDto("fakePlaza", "fakeCity", "fakeState", 12.50m, VehicleType.Car);
            
        //Act
        await service.RecordTollUsageAsync(fakeDto);

        //Assert
        _mockMessageSender.Verify(v => v.SendLocal(It.IsAny<TollPassageMessage>()), Times.Once);
    }

    public TollPassageService CreateService()
        => new TollPassageService(
            _mockMessageSender.Object, 
            _logger.Object);
}

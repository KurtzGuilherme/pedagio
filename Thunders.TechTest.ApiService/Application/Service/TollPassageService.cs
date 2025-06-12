using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Application.Messaging.Messages;
using Thunders.TechTest.ApiService.Application.Service.Interfaces;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Application.Service;

public class TollPassageService : ITollPassageService
{
    private readonly IMessageSender _messageSender;
    private readonly ILogger<TollPassageService> _logger;

    public TollPassageService(IMessageSender messageSender, ILogger<TollPassageService> logger)
    {
        _messageSender = messageSender;
        _logger = logger;
    }

    public async Task RecordTollUsageAsync(TollPassageDto dto)
    {
        var message = new TollPassageMessage()
        {
            AmountPaid = dto.AmountPaid,
            City = dto.City,
            PassageDateTime = dto.PassageDateTime,
            Plaza = dto.Plaza,
            State = dto.State,
            VehicleType = dto.VehicleType
        };
     

    await _messageSender.SendLocal(message);

        _logger.LogInformation(ApplicationMessages.MessageSentPlaza, dto.Plaza);
    }

}
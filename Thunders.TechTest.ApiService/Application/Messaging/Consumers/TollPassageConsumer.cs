using Rebus.Handlers;
using System;
using Thunders.TechTest.ApiService.Application.Messaging.Messages;
using Thunders.TechTest.ApiService.Application.Service.Interfaces;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Repository.Interface;

namespace Thunders.TechTest.ApiService.Application.Messaging.Consumers;

public class TollPassageConsumer : IHandleMessages<TollPassageMessage>
{
    private readonly ITollPassageRepository _tollPassageRepository;
    private readonly ILogger<TollPassageMessage> _logger;

    public TollPassageConsumer(ITollPassageRepository tollPassageRepository, ILogger<TollPassageMessage> logger)
    {
        _logger = logger;
        _tollPassageRepository = tollPassageRepository;
    }

    public async Task Handle(TollPassageMessage message)
    {
        try
        {
            var tollPassage = new TollPassage(
                message.PassageDateTime,
                message.Plaza,
                message.City,
                message.State,
                message.AmountPaid,
                message.VehicleType);

            await _tollPassageRepository.AddAsync(tollPassage);
            
            _logger.LogInformation(ApplicationMessages.TollRecordedPlaza, message.Plaza);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ApplicationMessages.ErrorProcessingMessage);
            throw;
        }
    }
}
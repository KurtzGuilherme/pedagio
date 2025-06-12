using Rebus.Handlers;
using Rebus.Messages;
using System.Text.Json;
using Thunders.TechTest.ApiService.Application.Messaging.Messages;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.ApiService.Domain.Exceptions;
using Thunders.TechTest.ApiService.Infra.Repository.Interface;

namespace Thunders.TechTest.ApiService.Application.Messaging.Consumers;

public class ProcessReportConsumer : IHandleMessages<ProcessReportMessage>
{

    private readonly ITollPassageRepository _tollPassageRepository;
    private readonly IProcessReportRepository _processReportRepository;
    private readonly ILogger<ProcessReportConsumer> _logger;

    public ProcessReportConsumer(ITollPassageRepository tollPassageRepository, IProcessReportRepository processReportRepository, ILogger<ProcessReportConsumer> logger)
    {
        _tollPassageRepository = tollPassageRepository;
        _processReportRepository = processReportRepository;
        _logger = logger;
    }

    public async Task Handle(ProcessReportMessage message)
    {
        try
        {
            switch (message.ReportType)
            {
                case ReportType.HourlySalesByCity:
                    await ProcessHourlySalesByCityAsync(message.StartDate, message.EndDate);
                    break;

                case ReportType.TopCitiesByRevenue:
                    await ProcessTopCitiesByRevenueAsync(message.Month, message.Year, message.TopPlazas);
                    break;

                case ReportType.VehicleTypesByPlaza:

                    if (string.IsNullOrWhiteSpace(message.Plaza))
                        throw new ArgumentNullOrEmptyException(nameof(message.Plaza));

                    await ProcessVehicleTypesByPlazaAsync(message.Plaza);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            _logger.LogInformation(ApplicationMessages.ReportProcessedSuccessfully, message.ReportType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ApplicationMessages.ErrorProcessingMessage);
            throw;
        }
    }

    private async Task ProcessHourlySalesByCityAsync(DateTime startDate, DateTime endDate)
    {
        var result = await _tollPassageRepository.GetTollPassageHourlySalesByCityAsync(startDate, endDate);

        if(result == null)
            return;

        var reportResult = new ProcessReport(
            ReportType.HourlySalesByCity,
            JsonSerializer.Serialize(result));

        await _processReportRepository.AddAsync(reportResult);
    }

    private async Task ProcessTopCitiesByRevenueAsync(int month, int year, int? topPlazas)
    {
        var result = await _tollPassageRepository.GetTopCitiesByRevenueAsync(
            month,
            year,
            topPlazas == null ? 10 : topPlazas.Value);


        if (result == null)
            return;

        var reportResult = new ProcessReport(
            ReportType.TopCitiesByRevenue,
            JsonSerializer.Serialize(result));

        await _processReportRepository.AddAsync(reportResult);
    }

    private async Task ProcessVehicleTypesByPlazaAsync(string plaza)
    {
        if (string.IsNullOrWhiteSpace(plaza))
            throw new ArgumentNullOrEmptyException(nameof(plaza));

        var result = await _tollPassageRepository.GetVehicleTypesByPlazaAsync(plaza);

        if (result == null)
            return;

        var reportResult = new ProcessReport(
            ReportType.VehicleTypesByPlaza,
            JsonSerializer.Serialize(result));

        await _processReportRepository.AddAsync(reportResult);
    }
}

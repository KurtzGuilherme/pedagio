using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Application.Messaging.Messages;
using Thunders.TechTest.ApiService.Application.Result;
using Thunders.TechTest.ApiService.Application.Service.Interfaces;
using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.ApiService.Domain.Exceptions;
using Thunders.TechTest.ApiService.Infra.Repository.Interface;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Application.Service;

public class ProcessReportService : IProcessReportService
{
    private readonly IMessageSender _messageSender;
    private readonly ILogger<ProcessReportService> _logger;
    private readonly IProcessReportRepository _processReportRepository;

    public ProcessReportService(
        IMessageSender messageSender,
        ILogger<ProcessReportService> logger,
        IProcessReportRepository processReportRepository)
    {
        _processReportRepository = processReportRepository;
        _messageSender = messageSender;
        _logger = logger;
    }

    public async Task GenerateHourlySalesByCityReport(ProcessReportHourlySalesByCityDto dto)
    {
        var message = new ProcessReportMessage()
        {
            EndDate = dto.EndDate,
            StartDate = dto.StartDate,
            ReportType = dto.ReportType,
        };
            
        await _messageSender.SendLocal(message);

        _logger.LogInformation(ApplicationMessages.StartedHourlyCityReportGeneration);

    }

    public async Task GenerateTopCitiesByRevenueReport(ProcessReportTopCitiesByRevenueDto dto)
    {
        var message = new ProcessReportMessage()
        {
            Month = dto.Month,
            Year = dto.Year,
            ReportType = dto.ReportType,
            TopPlazas = dto.TopCount,
        };

        await _messageSender.SendLocal(message);

        _logger.LogInformation(ApplicationMessages.StartedPlazaReportGeneration);
    }

    public async Task GenerateVehicleTypesByPlazaReport(ProcessRepostVehicleTypesByPlazaDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Plaza))
            throw new ArgumentNullOrEmptyException(nameof(dto.Plaza));

        var message = new ProcessReportMessage()
        {
            ReportType = dto.ReportType,
            Plaza = dto.Plaza
        };

        await _messageSender.SendLocal(message);

        _logger.LogInformation(ApplicationMessages.StartedVehicleCountReportGeneration);
    }

    public async Task<ProcessReportResult> GetReportResultByTypeAsync(ReportType reportType)
    {
        var result = await _processReportRepository.GetReportResultByTypeAsync(reportType);

        if (result == null)
            throw new KeyNotFoundException(ApplicationMessages.ReportResultNotFound);

        return result;
    }

}


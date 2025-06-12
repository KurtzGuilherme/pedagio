
using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Application.Result;
using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Application.Service.Interfaces;

public interface IProcessReportService
{
    Task GenerateHourlySalesByCityReport(ProcessReportHourlySalesByCityDto dto);
    Task GenerateTopCitiesByRevenueReport(ProcessReportTopCitiesByRevenueDto dto);
    Task GenerateVehicleTypesByPlazaReport(ProcessRepostVehicleTypesByPlazaDto dto);
    Task<ProcessReportResult> GetReportResultByTypeAsync(ReportType reportType);
}
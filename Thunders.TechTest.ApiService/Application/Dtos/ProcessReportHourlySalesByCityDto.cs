using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Application.Dtos;

public class ProcessReportHourlySalesByCityDto
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public ReportType ReportType { get; }

    public ProcessReportHourlySalesByCityDto(
        DateTime? startDate,
        DateTime? endDate,
        ReportType reportType)
    {
        if (!startDate.HasValue)
            throw new ArgumentNullException(nameof(startDate));

        if (!endDate.HasValue)
            throw new ArgumentNullException(nameof(endDate));

        ReportType = reportType;
        StartDate = startDate.Value;
        EndDate = endDate.Value;
    }

    public ProcessReportHourlySalesByCityDto(
        DateTime? startDate,
        DateTime? endDate)
        :this(startDate, endDate, ReportType.HourlySalesByCity)
    {
            
    }
}

using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Application.Dtos;

public class ProcessReportTopCitiesByRevenueDto
{
    public int Month { get; }
    public int Year { get; }
    public int? TopCount { get; }
    public ReportType ReportType { get; }

    public ProcessReportTopCitiesByRevenueDto(
        int? month,
        int? year,
        int? topCount,
        ReportType reportType)
    {
        if (!month.HasValue)
            throw new ArgumentNullException(nameof(month));

        if (!year.HasValue)
            throw new ArgumentNullException(nameof(year));

        ReportType = reportType;
        Month = month.Value;
        Year = year.Value;
        TopCount = (topCount == null || topCount == 0) ? 10 : topCount;
    }

    public ProcessReportTopCitiesByRevenueDto(
        int? month,
        int? year,
        int? topCount)
        :this(month, year, topCount, ReportType.TopCitiesByRevenue)
    {
            
    }
}

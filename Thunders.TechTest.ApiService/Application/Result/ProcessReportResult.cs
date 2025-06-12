using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Application.Result;

public class ProcessReportResult
{
    public Guid Id { get; set; }
    public ReportType ReportType { get; set; }
    public DateTime CreateDate { get; set; }
    public string Data { get; set; }

}

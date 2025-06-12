using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Application.Messaging.Messages;

public class ProcessReportMessage
{
    public ReportType ReportType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int? TopPlazas { get; set; }
    public string Plaza { get; set; }

}

namespace Thunders.TechTest.ApiService.Domain.Request;

public class ReportRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? TopCount { get; set; }
}

namespace Thunders.TechTest.ApiService.Application.RequestDtos;

public class ProcessReporTopCitiesByRevenuePostRequest
{
    public int? Month { get;  set; }
    public int? Year { get; set; }
    public int? TopCount { get; set; }
}

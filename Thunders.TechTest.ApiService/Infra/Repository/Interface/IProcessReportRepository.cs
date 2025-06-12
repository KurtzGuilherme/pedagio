using Thunders.TechTest.ApiService.Application.Result;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.ApiService.Infra.Repository.Interface.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repository.Interface;

public interface IProcessReportRepository : IBaseRepository<ProcessReport>
{
    Task<ProcessReportResult?> GetReportResultByTypeAsync(ReportType reportType);
}

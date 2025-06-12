using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Application.Result;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.ApiService.Infra.Data.SqlServer;
using Thunders.TechTest.ApiService.Infra.Repository.Interface;
using Thunders.TechTest.ApiService.Infra.Repository.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repository;

public class ProcessReportRepository : BaseRepository<ProcessReport>, IProcessReportRepository
{
    public ProcessReportRepository(SqlServerContext context) : base(context) { }

    public async Task<ProcessReportResult?> GetReportResultByTypeAsync(ReportType reportType)
    {
        var result = await DbSet
          .Where(p => p.ReportType == reportType)
          .Select(g => new ProcessReportResult
          {
              Id = g.Id,
              CreateDate = g.CreateDate,
              Data = g.Data,
              ReportType = g.ReportType,
          }).FirstOrDefaultAsync();

        return result;
    }

}

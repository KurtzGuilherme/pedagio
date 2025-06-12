using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.ApiService.Domain.Exceptions;

namespace Thunders.TechTest.ApiService.Domain.Entities;

public sealed class ProcessReport
{
    public Guid Id { get; private set; }
    public ReportType ReportType { get; private set; }
    public DateTime CreateDate { get; private set; }
    public string Data { get; private set; }

    public ProcessReport(
        Guid id,
        ReportType reportType,
        DateTime createDate,
        string data)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullOrEmptyException(nameof(id));

        Id = id;
        ReportType = reportType;
        Data = data;
        CreateDate = createDate;
    }

    public ProcessReport(
        ReportType reportType,
        string data)
        :this(Guid.NewGuid(), reportType, DateTime.Now, data)
    {
            
    }
}

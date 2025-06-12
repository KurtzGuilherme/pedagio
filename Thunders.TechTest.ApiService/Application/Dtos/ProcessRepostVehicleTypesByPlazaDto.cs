using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Application.Dtos;

public class ProcessRepostVehicleTypesByPlazaDto
{
    public string Plaza { get; }
    public ReportType ReportType { get; }

    public ProcessRepostVehicleTypesByPlazaDto(
        string? plaza,
        ReportType reportType)
    {
        
        if (string.IsNullOrWhiteSpace(plaza))
            throw new ArgumentNullException(nameof(plaza));

        ReportType = reportType;
        Plaza = plaza;
    }

    public ProcessRepostVehicleTypesByPlazaDto(
        string? plaza)
        :this(plaza, ReportType.VehicleTypesByPlaza)
    {
            
    }
}

using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.ApiService.Domain.Exceptions;

namespace Thunders.TechTest.Tests.Domain.Entities;
public class ProcessReportTest
{
    [Fact]
    public void Create_Construtor_CreateInstanceCorrect()
    {
        //Arrange
        var reportType = ReportType.VehicleTypesByPlaza;
        var data = "fakeData";
        
        //Act
        var fakeProcessReport = new ProcessReport(reportType, data);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(fakeProcessReport);
            Assert.Equal(data, fakeProcessReport.Data);
            Assert.Equal(reportType, fakeProcessReport.ReportType);
        });
    }

    [Fact]
    public void Construtor_IdGuidEmpty_ThrowsArgumentNullOrEmptyException()
    {
        //Arrange
        var createDate = DateTime.Now;
        var reportType = ReportType.VehicleTypesByPlaza;
        var data = "fakeData";

        //Act
        var result = Assert.Throws<ArgumentNullOrEmptyException>(() =>
            new ProcessReport(Guid.Empty, reportType, createDate, data));

        //Assert
        Assert.IsType<ArgumentNullOrEmptyException>(result);
    }
}

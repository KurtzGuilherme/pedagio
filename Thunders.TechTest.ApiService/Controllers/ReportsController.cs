using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Application.InputModels;
using Thunders.TechTest.ApiService.Application.RequestDtos;
using Thunders.TechTest.ApiService.Application.Service.Interfaces;
using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IProcessReportService _processReportService;
    
    public ReportsController(IProcessReportService processReportService)
    {
        _processReportService = processReportService;
    }

    [HttpPost("hourly-sales-by-city")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public async Task<IActionResult> GenerateHourlySalesByCityReport([FromBody] ProcessReporHourlySalesByCityPostRequest request)
    {
        try
        {
            var dto = new ProcessReportHourlySalesByCityDto(request.StartDate, request.EndDate);
            await _processReportService.GenerateHourlySalesByCityReport(dto);

            return Accepted();
        }
        catch (ArgumentException ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("top-cities-by-revenue")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public async Task<IActionResult> GenerateTopCitiesByRevenueReport([FromBody] ProcessReporTopCitiesByRevenuePostRequest request)
    {
        try
        {
            var dto = new ProcessReportTopCitiesByRevenueDto(request.Month, request.Year, request.TopCount);
            await _processReportService.GenerateTopCitiesByRevenueReport(dto);

            return Accepted();
        }
        catch (ArgumentException ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("vehicle-types-by-plaza")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public async Task<IActionResult> GenerateVehicleTypesByPlazaReport([FromBody] ProcessReporVehicleTypesByPlazaPostRequest request)
    {
        try
        {
            var dto = new ProcessRepostVehicleTypesByPlazaDto(request.Plaza);
            await _processReportService.GenerateVehicleTypesByPlazaReport(dto);

            return Accepted();
        }
        catch (ArgumentException ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{reportType:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetReportByTypeAsync(int reportType)
    {
        try
        {
            var enumValue = (ReportType)reportType;
            var result = await _processReportService.GetReportResultByTypeAsync(enumValue);

            if (result == null)
                return NoContent();

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}

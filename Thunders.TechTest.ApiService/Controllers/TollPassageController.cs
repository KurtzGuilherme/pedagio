using Microsoft.AspNetCore.Mvc;
using System.Net;
using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Application.RequestDtos;
using Thunders.TechTest.ApiService.Application.Service.Interfaces;

namespace Thunders.TechTest.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TollPassageController : ControllerBase
{
    private readonly ITollPassageService _tollPassageService;

    public TollPassageController(ITollPassageService tollPassageService)
    {
        _tollPassageService = tollPassageService;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public async Task<IActionResult> GenerateTollPassageAsync([FromBody] CreateTollPassagePostRequest request)
    {
        try
        {
            var dto = new TollPassageDto(request.Plaza, request.City, request.State, request.AmountPaid, request.VehicleType);
            await _tollPassageService.RecordTollUsageAsync(dto);

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

    [HttpPost("batch")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public async Task<IActionResult> RecordUsageBatchAsync([FromBody] List<CreateTollPassagePostRequest> requestList)
    {
        try
        {
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
}

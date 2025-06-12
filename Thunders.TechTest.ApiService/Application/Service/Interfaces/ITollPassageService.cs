using Thunders.TechTest.ApiService.Application.Dtos;
using Thunders.TechTest.ApiService.Domain.Entities;

namespace Thunders.TechTest.ApiService.Application.Service.Interfaces;

public interface ITollPassageService
{
    Task RecordTollUsageAsync(TollPassageDto dto);
}

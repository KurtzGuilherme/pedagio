using Thunders.TechTest.ApiService.Application.Result;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Repository.Interface.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repository.Interface;

public interface ITollPassageRepository : IBaseRepository<TollPassage>
{
    Task<List<TollPassageHourlySalesByCityResult>> GetTollPassageHourlySalesByCityAsync(DateTime startDate , DateTime endDate);
    Task<List<TopCitiesByRevenueResult>> GetTopCitiesByRevenueAsync(int month, int year, int topCount);
    Task<List<VehicleTypesByPlazaResult>> GetVehicleTypesByPlazaAsync(string plaza);
}

using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Application.Result;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Data.SqlServer;
using Thunders.TechTest.ApiService.Infra.Repository.Interface;
using Thunders.TechTest.ApiService.Infra.Repository.Shared;

namespace Thunders.TechTest.ApiService.Infra.Repository;

public class TollPassageRepository : BaseRepository<TollPassage>, ITollPassageRepository
{
    public TollPassageRepository(SqlServerContext context) : base(context) { }

    public async Task<List<TollPassageHourlySalesByCityResult>> GetTollPassageHourlySalesByCityAsync(DateTime startDate, DateTime endDate)
    {
        var result = await DbSet
            .Where(p => p.PassageDateTime >= startDate && p.PassageDateTime <= endDate)
            .GroupBy(p => new
            {
                p.PassageDateTime,
                p.City
            })
            .Select(g => new TollPassageHourlySalesByCityResult
            {
                DateHour = g.Key.PassageDateTime,
                City = g.Key.City,
                ValuePaid = g.Sum(p => p.AmountPaid)
            })
            .OrderBy(r => r.DateHour)
            .ThenBy(r => r.City)
            .ToListAsync();

        return result;
    }

    public async Task<List<TopCitiesByRevenueResult>> GetTopCitiesByRevenueAsync(int month, int year, int quantity)
    {
        var result = await DbSet
          .Where(p => p.PassageDateTime.Month == month && p.PassageDateTime.Year == year)
          .GroupBy(d => new
          {
              d.Plaza,
              d.PassageDateTime.Month
          })
          .Select(g => new TopCitiesByRevenueResult
          {
              Year = year,
              Month = g.Key.Month,
              Plaza = g.Key.Plaza,
              ValuePaid = g.Sum(p => p.AmountPaid)
          })
          .OrderByDescending(r => r.ValuePaid)
          .Take(quantity)
          .ToListAsync();

        return result;
    }

    public async Task<List<VehicleTypesByPlazaResult>> GetVehicleTypesByPlazaAsync(string plaza)
    {
        var result = await DbSet
          .Where(p => p.Plaza == plaza)
           .GroupBy(p => p.VehicleType)
          .Select(g => new VehicleTypesByPlazaResult
          {
              VehicleType = g.Key.ToString(),
              Quantity = g.Count(),
              Plaza = plaza
          }).ToListAsync();

        return result;
    }
}

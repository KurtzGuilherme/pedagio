using Thunders.TechTest.ApiService.Application.Service;
using Thunders.TechTest.ApiService.Application.Service.Interfaces;
using Thunders.TechTest.ApiService.Infra.Repository;
using Thunders.TechTest.ApiService.Infra.Repository.Interface;

namespace Thunders.TechTest.ApiService.Config;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services.AddScoped<ITollPassageRepository, TollPassageRepository>();
        services.AddScoped<IProcessReportRepository, ProcessReportRepository>();

        services.AddScoped<IProcessReportService, ProcessReportService>();
        services.AddScoped<ITollPassageService, TollPassageService>();
    }
}

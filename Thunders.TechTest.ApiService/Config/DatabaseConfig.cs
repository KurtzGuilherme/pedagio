using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Infra.Data.SqlServer;

namespace Thunders.TechTest.ApiService.Config;

public static class DatabaseConfig
{
    public static void AddDatabase(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<SqlServerContext>(opt =>
            opt.UseSqlServer(config.GetValue<string>("ConnectionStrings:ThundersTechTestDb")));
    }
}

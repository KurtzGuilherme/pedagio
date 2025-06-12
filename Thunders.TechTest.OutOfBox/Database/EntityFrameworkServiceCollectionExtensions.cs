using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Thunders.TechTest.OutOfBox.Database
{
    public static class EntityFrameworkServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerDbContext<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {
            var connectionString = $"Server=GKURTZ;Initial Catalog=ThundersTechTest;MultipleActiveResultSets=true; MultipleActiveResultSets=true; Trusted_Connection=True;TrustServerCertificate=True;";

            services.AddDbContext<TContext>((options) =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}

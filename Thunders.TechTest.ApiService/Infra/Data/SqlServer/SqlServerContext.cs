using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Infra.Maps;

namespace Thunders.TechTest.ApiService.Infra.Data.SqlServer;

public class SqlServerContext : DbContext
{

    public SqlServerContext()
    {
            
    }

    public SqlServerContext(DbContextOptions<SqlServerContext> options) 
        : base(options) { }

    public virtual DbSet<TollPassage> Tolls { get; set; }
    public virtual DbSet<ProcessReport> ProcessReports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SqlServerContext).Assembly);

        modelBuilder.ApplyConfiguration(new TollPassageMap());
        modelBuilder.ApplyConfiguration(new ProcessReportMap());

        base.OnModelCreating(modelBuilder);
    }
}

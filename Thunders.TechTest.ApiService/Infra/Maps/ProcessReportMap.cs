using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Infra.Maps;

public class ProcessReportMap : IEntityTypeConfiguration<ProcessReport>
{
    public void Configure(EntityTypeBuilder<ProcessReport> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
              .ValueGeneratedOnAdd()
              .HasColumnName("ID");

        builder.Property(m => m.CreateDate)
            .HasColumnName("CREATION_DATE");

        builder.Property(m => m.Data)
            .HasColumnName("DATA");

        builder.Property(m => m.ReportType)
            .HasConversion(
            v => (int)v,
            v => Enum.IsDefined(typeof(ReportType), v) ? (ReportType)v : ReportType.Unknown)
            .HasColumnName("REPORT_TYPE");

        builder.ToTable("PROCESS_REPORT");
    }
}

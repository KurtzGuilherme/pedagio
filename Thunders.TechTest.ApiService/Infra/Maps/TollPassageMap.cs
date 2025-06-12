using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Infra.Maps;

public class TollPassageMap : IEntityTypeConfiguration<TollPassage>
{
    public void Configure(EntityTypeBuilder<TollPassage> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
              .ValueGeneratedOnAdd()
              .HasColumnName("ID");

        builder.Property(m => m.PassageDateTime)
             .HasColumnName("PASSAGE_DATETIME");

        builder.Property(m => m.Plaza)
            .HasColumnName("PLAZA_NAME");

        builder.Property(m => m.City)
            .HasColumnName("CITY");

        builder.Property(m => m.State)
            .HasColumnName("STATE");

        builder.Property(m => m.AmountPaid)
            .HasColumnName("AMOUNT_PAID");

        builder.Property(m => m.VehicleType)
            .HasConversion(
            v => (int)v,
            v => Enum.IsDefined(typeof(VehicleType), v) ? (VehicleType)v : VehicleType.Unknown)
            .HasColumnName("VEHICLE_TYPE");


        builder.ToTable("TOLL_PASSAGE");
    }
}

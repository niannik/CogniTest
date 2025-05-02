using Domain.Entities.CityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CityConfigurations : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(x => x.Province)
            .WithMany(x => x.Cities)
            .HasForeignKey(x => x.ProvinceId);

        builder.HasMany(x => x.Schools)
            .WithOne(x => x.City)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.CityId);
    }
}

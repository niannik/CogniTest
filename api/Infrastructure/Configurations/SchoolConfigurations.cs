using Domain.Entities.SchoolAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SchoolConfigurations : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.TelNumber)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.PostalCode)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Level)
            .HasDefaultValue(SchoolLevel.Elementary)
            .IsRequired();

        builder.HasOne(x => x.City)
            .WithMany(x => x.Schools)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.CityId);

        builder.HasOne(x => x.Principal)
            .WithOne(x => x.School)
            .HasForeignKey<SchoolPrincipal>(x => x.SchoolId);

        builder.HasMany(x => x.Students)
            .WithOne(x => x.School)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.SchoolId);
    }
}

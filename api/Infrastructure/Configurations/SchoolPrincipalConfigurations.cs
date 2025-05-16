using Domain.Entities.SchoolAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SchoolPrincipalConfigurations : IEntityTypeConfiguration<SchoolPrincipal>
{
    public void Configure(EntityTypeBuilder<SchoolPrincipal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.LastName)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.NationalCode)
            .HasMaxLength(10)
            .IsRequired(false);

        builder.HasMany(x => x.UserDevices)
            .WithOne(x => x.SchoolPrincipal)
            .HasForeignKey(x => x.SchoolPrincipalId);

        builder.HasOne(x => x.School)
            .WithOne(x => x.Principal)
            .HasForeignKey<SchoolPrincipal>(x => x.SchoolId);
    }
}

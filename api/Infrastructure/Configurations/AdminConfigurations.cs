using Domain.Entities.AdminAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AdminConfigurations : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.FullName)
            .IsRequired();

        builder.HasMany(x => x.AdminRoles)
            .WithOne(x => x.Admin)
            .HasForeignKey(x => x.AdminId);
    }
}

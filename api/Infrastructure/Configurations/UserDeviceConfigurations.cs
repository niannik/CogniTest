using Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserDeviceConfigurations : IEntityTypeConfiguration<UserDevice>
{
    public void Configure(EntityTypeBuilder<UserDevice> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.HashedRefreshToken)
            .IsRequired();

        builder.Property(x => x.RefreshTokenExpiresAt)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserDevices)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Admin)
            .WithMany(x => x.UserDevices)
            .HasForeignKey(x => x.AdminId);

        builder.HasOne(x => x.SchoolPrincipal)
            .WithMany(x => x.UserDevices)
            .HasForeignKey(x => x.SchoolPrincipalId);
    }
}

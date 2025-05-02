using Domain.Entities.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AdminRoleConfigurations : IEntityTypeConfiguration<AdminRole>
{
    public void Configure(EntityTypeBuilder<AdminRole> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Admin)
            .WithMany(x => x.AdminRoles)
            .HasForeignKey(x => x.AdminId);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.AdminRoles)
            .HasForeignKey(x => x.RoleId);
    }
}

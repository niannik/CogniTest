using Domain.Entities.RoleAggregate;
using Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserTestSessionConfigurations : IEntityTypeConfiguration<UserTestSession>
{
    public void Configure(EntityTypeBuilder<UserTestSession> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CompletedAt)
            .IsRequired(false);
        builder.Property(x => x.Status)
            .IsRequired(true);

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserTestSessions)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.WorkingMemoryTest)
            .WithMany(x => x.UserTestSessions)
            .HasForeignKey(x => x.WorkingMemoryTestId);

        builder.HasMany(x => x.WorkingMemoryResponses)
            .WithOne(x => x.UserTestSession)
            .HasForeignKey(x => x.UserTestSessionId);
    }
}

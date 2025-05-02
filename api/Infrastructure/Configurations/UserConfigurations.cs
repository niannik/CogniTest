using Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Age)
            .HasDefaultValue(6)
            .IsRequired();

        builder.Property(x => x.Gender)
            .IsRequired();

        builder.Property(x => x.IsRightHanded)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasMany(x => x.UserDevices)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.School)
            .WithMany(x => x.Students)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.SchoolId);

        builder.HasMany(x => x.WorkingMemoryResponses)
            .WithOne(x => x.Student)
            .HasForeignKey(x => x.StudentId);
    }
}

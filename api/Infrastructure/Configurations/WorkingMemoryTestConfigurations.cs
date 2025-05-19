using Domain.Entities.UserAggregate;
using Domain.Entities.WorkingMemoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class WorkingMemoryTestConfigurations : IEntityTypeConfiguration<WorkingMemoryTest>
{
    public void Configure(EntityTypeBuilder<WorkingMemoryTest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .IsRequired();
        builder.Property(x => x.Order)
            .IsRequired();
        builder.Property(x => x.Description)
            .IsRequired();

        builder.HasMany(x => x.WorkingMemoryTerms)
            .WithOne(x => x.WorkingMemoryTest)
            .HasForeignKey(x => x.WorkingMemoryTestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Audio)
            .WithMany(x => x.WorkingMemoryTests)
            .HasForeignKey(x => x.AudioId);
    }
}

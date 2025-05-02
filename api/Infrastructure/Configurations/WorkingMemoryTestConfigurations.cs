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

        builder.HasMany(x => x.WorkingMemoryTerms)
            .WithOne(x => x.WorkingMemoryTest)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.WorkingMemoryTestId);
    }
}

using Domain.Entities.UserAggregate;
using Domain.Entities.WorkingMemoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class WorkingMemoryTermConfigurations : IEntityTypeConfiguration<WorkingMemoryTerm>
{
    public void Configure(EntityTypeBuilder<WorkingMemoryTerm> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Picture)
            .WithMany(x => x.WorkingMemoryTerms)
            .HasForeignKey(x => x.PictureId);

        builder.HasOne(x => x.WorkingMemoryTest)
            .WithMany(x => x.WorkingMemoryTerms)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.WorkingMemoryTestId);

        builder.HasMany(x => x.WorkingMemoryResponses)
            .WithOne(x => x.WorkingMemoryTerm)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.WorkingMemoryTermId);
    }
}

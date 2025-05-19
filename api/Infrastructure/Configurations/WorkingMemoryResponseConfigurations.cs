using Domain.Entities.UserAggregate;
using Domain.Entities.WorkingMemoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class WorkingMemoryResponseConfigurations : IEntityTypeConfiguration<WorkingMemoryResponse>
{
    public void Configure(EntityTypeBuilder<WorkingMemoryResponse> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsTarget)
            .IsRequired(false);

        builder.Property(x => x.ResponseTime)
            .IsRequired(true);

        builder.HasOne(x => x.WorkingMemoryTerm)
            .WithMany(x => x.WorkingMemoryResponses)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.WorkingMemoryTermId);

        builder.HasOne(x => x.Student)
            .WithMany(x => x.WorkingMemoryResponses)
            .HasForeignKey(x => x.StudentId);
    }
}

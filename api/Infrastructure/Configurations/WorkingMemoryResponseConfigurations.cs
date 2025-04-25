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

        builder.HasOne(x => x.WorkingMemoryTerm)
            .WithMany(x => x.WorkingMemoryResponses)
            .HasForeignKey(x => x.WorkingMemoryTermId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.WorkingMemoryResponses)
            .HasForeignKey(x => x.UserId);
    }
}

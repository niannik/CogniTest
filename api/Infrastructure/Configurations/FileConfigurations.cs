using Domain.Entities.FileAggregate;
using Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Domain.Entities.FileAggregate.File;

namespace Infrastructure.Configurations;

public class FileConfigurations : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.FileAggregate.File> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.WorkingMemoryTerms)
            .WithOne(x => x.Picture)
            .HasForeignKey(x => x.PictureId);
    }
}

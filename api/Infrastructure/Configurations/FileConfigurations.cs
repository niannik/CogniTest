using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Domain.Entities.FileAggregate.File;

namespace Infrastructure.Configurations;

public class FileConfigurations : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.WorkingMemoryTerms)
            .WithOne(x => x.Picture)
            .HasForeignKey(x => x.PictureId);

        builder.HasMany(x => x.WorkingMemoryTests)
            .WithOne(x => x.Audio)
            .HasForeignKey(x => x.AudioId);
    }
}

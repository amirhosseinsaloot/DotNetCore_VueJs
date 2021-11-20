using Core.Interfaces;
using Data.Entities.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Files;

public class FileModel : IBaseEntity, ICreatedOn
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string FileType { get; set; }

    public string Extension { get; set; }

    public string Description { get; set; }

    public DateTime CreatedOn { get; set; }

    // Navigation properties
    public User User { get; set; }

    public ICollection<EmailsLogFileModel> EmailsLogFileModels { get; set; }
}

public class FileModelConfiguration : IEntityTypeConfiguration<FileModel>
{
    public void Configure(EntityTypeBuilder<FileModel> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

        builder.Property(p => p.FileType).IsRequired().HasMaxLength(30);

        builder.Property(p => p.Extension).IsRequired().HasMaxLength(5);

        builder.Property(p => p.Description).HasMaxLength(20);
    }
}

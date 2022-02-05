using Core.Entities.Identity;
using Core.Entities.Logging;

namespace Core.Entities.Files;

public class FileModel : IBaseEntity, ICreatedOn
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedOn { get; set; }

    // Navigation properties
    public User? User { get; set; }

    public ICollection<EmailsLogFileModel>? EmailsLogFileModels { get; set; }
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

using Data.Entities.Identity;
using Data.Entities.Logging;

namespace Data.Entities.Files;

public class FileModel : IBaseEntity, ICreatedOn
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string FileType { get; set; } = default!;

    public string Extension { get; set; } = default!;

    public string Description { get; set; } = default!;

    public DateTime CreatedOn { get; set; }

    // Navigation properties
    public User? User { get; set; }

    public ICollection<EmailsLogFileModel> EmailsLogFileModels { get; set; } = default!;
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

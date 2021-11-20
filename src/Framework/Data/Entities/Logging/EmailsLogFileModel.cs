using Core.Interfaces;
using Data.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Logging;

public class EmailsLogFileModel : IEntity
{
    public int EmailsLogId { get; set; }

    public int FileModelId { get; set; }

    // Navigation properties
    public EmailsLog EmailsLog { get; set; }

    public FileModel FileModel { get; set; }
}

public class EmailsLogFileModelConfiguration : IEntityTypeConfiguration<EmailsLogFileModel>
{
    public void Configure(EntityTypeBuilder<EmailsLogFileModel> builder)
    {
        builder.HasKey(p => new { p.EmailsLogId, p.FileModelId });

        builder.HasIndex(p => p.FileModelId).IsUnique();

        builder.HasOne(p => p.EmailsLog)
               .WithMany(p => p.EmailsLogFileModels)
               .HasForeignKey(p => p.EmailsLogId);

        builder.HasOne(p => p.FileModel)
               .WithMany(p => p.EmailsLogFileModels)
               .HasForeignKey(p => p.FileModelId);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Files;

public class FileOnDatabase : FileModel
{
    public byte[] Data { get; set; }
}

public class FileOnDatabaseConfiguration : IEntityTypeConfiguration<FileOnDatabase>
{
    public void Configure(EntityTypeBuilder<FileOnDatabase> builder)
    {
    }
}

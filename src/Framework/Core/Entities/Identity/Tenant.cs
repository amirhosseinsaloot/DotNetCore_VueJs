using Core.Entities.Teams;

namespace Core.Entities.Identity;

public class Tenant : IBaseEntity, ICreatedOn
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    // Navigation properties
    public ICollection<Team>? Teams { get; set; }
}

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.HasIndex(p => p.Name).IsUnique();
    }
}

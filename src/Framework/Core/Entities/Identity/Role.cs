using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class Role : IdentityRole<int>, IBaseEntity, ICreatedOn
{
    public string Description { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    // Navigation Properties
    public ICollection<UserRole>? UserRoles { get; set; }
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(15);

        builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
    }
}

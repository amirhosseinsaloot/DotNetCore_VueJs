using Microsoft.AspNetCore.Identity;

namespace Data.Entities.Identity;

public class UserRole : IdentityUserRole<int>, IEntity, ICreatedOn
{
    public DateTime CreatedOn { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;

    public Role Role { get; set; } = null!;
}

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasOne(p => p.User)
               .WithMany(p => p.UserRoles)
               .HasForeignKey(p => p.UserId);

        builder.HasOne(p => p.Role)
               .WithMany(p => p.UserRoles)
               .HasForeignKey(p => p.RoleId);
    }
}

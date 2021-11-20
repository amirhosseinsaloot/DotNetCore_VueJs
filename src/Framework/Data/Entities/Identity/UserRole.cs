using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public class UserRole : IdentityUserRole<int>, IEntity, ICreatedOn
{
    public DateTime CreatedOn { get; set; }

    // Navigation Properties
    public User User { get; set; }

    public Role Role { get; set; }
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

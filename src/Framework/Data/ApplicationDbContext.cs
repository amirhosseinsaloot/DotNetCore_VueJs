using Data.Entities.Identity;
using Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Using ModelBuilder extension methods
        modelBuilder.AddDeleteBehaviorConvention(DeleteBehavior.NoAction);
        modelBuilder.AddCheckConstraints(Database);
        if (Database.IsNpgsql())
        {
            modelBuilder.NamesToSnakeCase();
        }
    }

    public override int SaveChanges()
    {
        var added = ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Added)
                    .Select(t => t.Entity)
                    .ToArray();

        foreach (var entity in added)
        {
            if (entity is ICreatedOn)
            {
                var track = (ICreatedOn)entity;
                track.CreatedOn = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var added = ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Added)
                    .Select(t => t.Entity)
                    .ToArray();

        foreach (var entity in added)
        {
            if (entity is ICreatedOn)
            {
                var track = (ICreatedOn)entity;
                track.CreatedOn = DateTime.UtcNow;
            }
        }

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var added = ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Added)
                    .Select(t => t.Entity)
                    .ToArray();

        foreach (var entity in added)
        {
            if (entity is ICreatedOn)
            {
                var track = (ICreatedOn)entity;
                track.CreatedOn = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var added = ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Added)
                    .Select(t => t.Entity)
                    .ToArray();

        foreach (var entity in added)
        {
            if (entity is ICreatedOn)
            {
                var track = (ICreatedOn)entity;
                track.CreatedOn = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}

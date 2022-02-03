using Core.Utilities;
using Data.Entities.Logging;
using Data.Entities.Teams;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Add delete behavior to entities with reflection.
    /// </summary>
    /// <param name="modelBuilder">For access to entities.</param>
    /// <param name="deleteBehavior">Intended DeleteBehavior enum.</param>
    public static void AddDeleteBehaviorConvention(this ModelBuilder modelBuilder, DeleteBehavior deleteBehavior)
    {
        IEnumerable<IMutableForeignKey> cascadeFKs =
            modelBuilder
            .Model.GetEntityTypes()
            .SelectMany(p => p.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (IMutableForeignKey fk in cascadeFKs)
        {
            fk.DeleteBehavior = deleteBehavior;
        }
    }

    /// <summary>
    /// Add check constraints for current database 
    /// </summary>
    /// <param name="modelBuilder">For adding check constraints to entities.</param>
    /// <param name="databaseFacade">For access to database provider.</param>
    public static void AddCheckConstraints(this ModelBuilder modelBuilder, DatabaseFacade databaseFacade)
    {
        // Postgres
        if (databaseFacade.IsNpgsql())
        {
            modelBuilder
                .Entity<Team>()
                .HasCheckConstraint("chk_tenant", $"(( {nameof(Team.ParentId).ToSnakeCase()} IS NULL AND {nameof(Team.TenantId).ToSnakeCase()} IS NOT NULL) OR ({nameof(Team.TenantId).ToSnakeCase()} IS NULL AND {nameof(Team.ParentId).ToSnakeCase()} IS NOT NULL))");

            modelBuilder
                .Entity<EmailsLog>()
                .HasCheckConstraint("chk_emails_log", $"(( {nameof(EmailsLog.ToUserId).ToSnakeCase()} IS NULL AND {nameof(EmailsLog.ToEmail).ToSnakeCase()} IS NOT NULL) OR ({nameof(EmailsLog.ToEmail).ToSnakeCase()} IS NULL AND {nameof(EmailsLog.ToUserId).ToSnakeCase()} IS NOT NULL))");
        }

        // SqlServer
        else
        {
            modelBuilder
                .Entity<Team>()
                .HasCheckConstraint("CHK_Tenant", $"(({nameof(Team.ParentId)} IS NULL AND {nameof(Team.TenantId)} IS NOT NULL) OR ({nameof(Team.TenantId)} IS NULL AND {nameof(Team.ParentId)} IS NOT NULL))");

            modelBuilder
                .Entity<EmailsLog>()
                .HasCheckConstraint("CHK_EmailsLog", $"(({nameof(EmailsLog.ToUserId)} IS NULL AND {nameof(EmailsLog.ToEmail)} IS NOT NULL) OR ({nameof(EmailsLog.ToEmail)} IS NULL AND {nameof(EmailsLog.ToUserId)} IS NOT NULL))");
        }
    }

    /// <summary>
    /// Change naming conventions to snake case for
    /// all tables,columns,primary key constraints,
    /// foreign key constraints and indexes.
    /// </summary>
    /// <param name="modelBuilder">For access to tables and columns and changing them.</param>
    public static void NamesToSnakeCase(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Change table names
            var tableName = entity.GetTableName();
            var schema = entity.GetSchema();

            if (tableName is not null)
            {
                entity.SetTableName(tableName.ToSnakeCase());
            }

            // Change column names            
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.Name.ToSnakeCase());
            }

            // Change primary keys constraint names
            foreach (var pk in entity.GetKeys())
            {
                var pkName = pk.GetName();
                if (pkName is not null)
                {
                    pk.SetName(pkName.ToSnakeCase());
                }
            }

            // Change foreign keys constraint names
            foreach (var fk in entity.GetForeignKeys())
            {
                var fkName = fk.GetConstraintName();
                if (fkName is not null)
                {
                    fk.SetConstraintName(fkName.ToSnakeCase());
                }
            }

            // Change index names
            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
            }
        }
    }
}

using Core.Interfaces;
using Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public class Team : IBaseEntity, ICreatedOn
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int? ParentId { get; set; }

    public int? TenantId { get; set; }

    public DateTime CreatedOn { get; set; }


    // Navigation properties
    public Team ParentTeam { get; set; }

    public Tenant Tenant { get; set; }

    public ICollection<Team> ChildTeams { get; set; }

    public ICollection<Ticket> Tickets { get; set; }

    public ICollection<TicketProcess> TicketProcesses { get; set; }

    public ICollection<User> Users { get; set; }
}

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

        builder.Property(p => p.Description).IsRequired().HasMaxLength(100);

        builder.HasOne(p => p.ParentTeam)
               .WithMany(p => p.ChildTeams)
               .HasForeignKey(p => p.ParentId);

        builder.HasOne(p => p.Tenant)
               .WithMany(p => p.Teams)
               .HasForeignKey(p => p.TenantId);
    }
}

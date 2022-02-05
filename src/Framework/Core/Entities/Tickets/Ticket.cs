using Core.Entities.Identity;
using Core.Entities.Teams;

namespace Core.Entities.Tickets;

public class Ticket : IBaseEntity, ICreatedOn
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public TicketStatus TicketStatus { get; set; }

    public int TicketTypeId { get; set; }

    public int TeamId { get; set; }

    public int IssuerUserId { get; set; }

    public DateTime CreatedOn { get; set; }

    // Navigation properties
    public TicketType TicketType { get; set; } = null!;

    public Team Team { get; set; } = null!;

    public User IssuerUser { get; set; } = null!;

    public ICollection<TicketProcess>? TicketProcesses { get; set; }
}

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasOne(p => p.Team)
               .WithMany(p => p.Tickets)
               .HasForeignKey(p => p.TeamId);

        builder.HasOne(p => p.TicketType)
               .WithMany(p => p.Tickets)
               .HasForeignKey(p => p.TicketTypeId);

        builder.HasOne(p => p.IssuerUser)
               .WithMany(p => p.Tickets)
               .HasForeignKey(p => p.IssuerUserId);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(30);

        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);

        builder.Property(p => p.TicketStatus).IsRequired();
    }
}

using Data.Entities.Identity;
using Data.Entities.Teams;

namespace Data.Entities.Tickets;

public class TicketProcess : IBaseEntity, ICreatedOn
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public int TicketId { get; set; }

    public int TeamId { get; set; }

    public int? AssignedUserId { get; set; }

    public int? ParentTicketProcessId { get; set; }

    public DateTime CreatedOn { get; set; }

    // Navigation properties
    public TicketProcess? ParentTicketProcess { get; set; }

    public ICollection<TicketProcess>? ChildTicketProcesses { get; set; }

    public Ticket Ticket { get; set; } = default!;

    public Team Team { get; set; } = default!;

    public User? User { get; set; }
}

public class TicketProcessConfiguration : IEntityTypeConfiguration<TicketProcess>
{
    public void Configure(EntityTypeBuilder<TicketProcess> builder)
    {
        builder.HasOne(p => p.ParentTicketProcess)
               .WithMany(p => p.ChildTicketProcesses)
               .HasForeignKey(p => p.ParentTicketProcessId);

        builder.HasOne(p => p.Ticket)
               .WithMany(p => p.TicketProcesses)
               .HasForeignKey(p => p.TicketId);

        builder.HasOne(p => p.Team)
               .WithMany(p => p.TicketProcesses)
               .HasForeignKey(p => p.TeamId);

        builder.HasOne(p => p.User)
               .WithMany(p => p.TicketProcesses)
               .HasForeignKey(p => p.AssignedUserId);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(15);

        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
    }
}

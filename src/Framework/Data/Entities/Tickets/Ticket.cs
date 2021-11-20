using Core.Enums;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public class Ticket : IBaseEntity, ICreatedOn
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public TicketStatus TicketStatus { get; set; }

    public int TicketTypeId { get; set; }

    public int TeamId { get; set; }

    public int IssuerUserId { get; set; }

    public DateTime CreatedOn { get; set; }


    // Navigation properties
    public TicketType TicketType { get; set; }

    public Team Team { get; set; }

    public User User { get; set; }

    public ICollection<TicketProcess> TicketProcesses { get; set; }
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

        builder.HasOne(p => p.User)
               .WithMany(p => p.Tickets)
               .HasForeignKey(p => p.IssuerUserId);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(30);

        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);

        builder.Property(p => p.TicketStatus).IsRequired();
    }
}

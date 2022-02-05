namespace Core.Entities.Tickets;

public class TicketType : IBaseEntity, ICreatedOn
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    // Navigation properties
    public ICollection<Ticket>? Tickets { get; set; }
}

public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.Property(p => p.Type).IsRequired().HasMaxLength(30);
    }
}

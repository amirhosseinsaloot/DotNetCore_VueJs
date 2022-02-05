namespace Service.DomainDto.Ticket;

public record class TicketTypeListDto : IDtoList
{
    public int Id { get; init; }

    public string Type { get; init; } = null!;

    public DateTime CreatedOn { get; init; }
}

namespace Service.Domain.Tickets.Models;

public record class TicketTypeViewModel : IViewModel
{
    public int Id { get; init; }

    public string Type { get; init; }
}

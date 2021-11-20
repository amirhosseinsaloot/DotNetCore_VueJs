using Core.Interfaces;

namespace Services.Domain;

public record class TicketTypeViewModel : IViewModel
{
    public int Id { get; init; }

    public string Type { get; init; }
}

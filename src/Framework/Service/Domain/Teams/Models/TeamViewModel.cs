namespace Service.Domain.Teams.Models;

public record class TeamViewModel : IViewModel
{
    public int Id { get; init; }

    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public int? ParentId { get; init; }
}

namespace Service.Domain.Teams.Models;

public record class TeamViewModel : IViewModel
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public int? ParentId { get; init; }
}

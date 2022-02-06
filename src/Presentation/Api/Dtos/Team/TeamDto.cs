namespace Api.Dtos.Team;

public record class TeamDto : IDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public int? ParentId { get; init; }
}

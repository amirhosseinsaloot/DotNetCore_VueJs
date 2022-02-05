namespace Infrastructure.Dto.Team;

public record class TeamListDto : IDtoList
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public int? ParentId { get; init; }

    public int? TenantId { get; init; }

    public DateTime CreatedOn { get; init; }
}

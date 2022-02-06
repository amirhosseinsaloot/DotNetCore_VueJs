namespace Api.Dtos.Role;

public record class RoleListDto : IDtoList
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string NormalizedName { get; init; } = null!;
}

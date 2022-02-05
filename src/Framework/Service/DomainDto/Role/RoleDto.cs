namespace Service.DomainDto.Role;

public record class RoleDto : IDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;
}

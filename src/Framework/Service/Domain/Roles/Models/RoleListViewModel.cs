namespace Service.Domain.Roles.Models;

public record class RoleListViewModel : IListViewModel
{
    public int Id { get; init; }

    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public string NormalizedName { get; init; } = default!;
}

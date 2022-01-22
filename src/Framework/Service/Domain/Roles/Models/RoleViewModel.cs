namespace Service.Domain.Roles.Models;

public record class RoleViewModel : IViewModel
{
    public int Id { get; init; }

    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;
}

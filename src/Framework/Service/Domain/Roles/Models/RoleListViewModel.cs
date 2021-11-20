using Core.Interfaces;

namespace Services.Domain;

public record class RoleListViewModel : IListViewModel
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public string NormalizedName { get; init; }
}

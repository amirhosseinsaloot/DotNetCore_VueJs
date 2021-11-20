using Core.Interfaces;

namespace Services.Domain;

public record class RoleViewModel : IViewModel
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }
}

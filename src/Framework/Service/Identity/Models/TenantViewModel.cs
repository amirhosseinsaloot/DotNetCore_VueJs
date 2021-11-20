using Core.Interfaces;

namespace Services.Domain;

public record class TenantViewModel : IViewModel
{
    public int Id { get; init; }

    public string Name { get; init; }

    public DateTime CreatedOn { get; init; }
}

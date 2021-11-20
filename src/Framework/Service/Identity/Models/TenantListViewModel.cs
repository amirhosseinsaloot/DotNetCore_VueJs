using Core.Interfaces;

namespace Services.Domain;

public record class TenantListViewModel : IListViewModel
{
    public int Id { get; init; }

    public string Name { get; init; }

    public DateTime CreatedOn { get; init; }
}

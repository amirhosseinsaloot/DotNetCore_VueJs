namespace Service.Identity.Models;

public record class TenantListViewModel : IListViewModel
{
    public int Id { get; init; }

    public string Name { get; init; } = default!;

    public DateTime CreatedOn { get; init; }
}

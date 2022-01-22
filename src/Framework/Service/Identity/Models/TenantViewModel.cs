namespace Service.Identity.Models;

public record class TenantViewModel : IViewModel
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public DateTime CreatedOn { get; init; }
}

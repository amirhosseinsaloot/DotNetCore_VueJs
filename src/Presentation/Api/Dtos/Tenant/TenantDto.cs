namespace Api.Dtos.Tenant;

public record class TenantDto : IDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public DateTime CreatedOn { get; init; }
}

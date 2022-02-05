namespace Service.DomainDto.Tenant;

public record class TenantListDto : IDtoList
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public DateTime CreatedOn { get; init; }
}

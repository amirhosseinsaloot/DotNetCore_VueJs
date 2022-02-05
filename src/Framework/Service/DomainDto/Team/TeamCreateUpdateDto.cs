namespace Service.DomainDto.Team;

public record class TeamCreateUpdateDto : IDtoCreate, IDtoUpdate
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public int? ParentId { get; init; }
}

public class TeamCreateUpdateDtoValidator : BaseValidator<TeamCreateUpdateDto>
{
    public TeamCreateUpdateDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(50);

        RuleFor(p => p.Description).NotEmpty().MaximumLength(100);

        When(p => p.ParentId.HasValue, () => RuleFor(p => p.ParentId).GreaterThanOrEqualTo(1));
    }
}

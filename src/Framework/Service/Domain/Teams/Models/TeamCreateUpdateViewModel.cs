namespace Service.Domain.Teams.Models;

public record class TeamCreateUpdateViewModel : ICreateViewModel, IUpdateViewModel
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public int? ParentId { get; init; }
}

public class TeamCreateUpdateViewModelValidator : BaseValidator<TeamCreateUpdateViewModel>
{
    public TeamCreateUpdateViewModelValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(50);

        RuleFor(p => p.Description).NotEmpty().MaximumLength(100);

        When(p => p.ParentId.HasValue, () => RuleFor(p => p.ParentId).GreaterThanOrEqualTo(1));
    }
}

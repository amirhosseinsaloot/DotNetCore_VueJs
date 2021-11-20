using Core.Interfaces;
using Core.Validations;
using FluentValidation;

namespace Services.Domain;

public record class RoleCreateUpdateViewModel : ICreateViewModel, IUpdateViewModel
{
    public string Name { get; init; }

    public string Description { get; init; }
}

public class RoleCreateUpdateViewModelValidator : BaseValidator<RoleCreateUpdateViewModel>
{
    public RoleCreateUpdateViewModelValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(15);

        RuleFor(p => p.Description).NotEmpty().MaximumLength(100);
    }
}

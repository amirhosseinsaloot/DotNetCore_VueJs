using Core.Interfaces;
using Core.Validations;
using FluentValidation;

namespace Services.Domain;

public record class TicketTypeCreateUpdateViewModel : ICreateViewModel, IUpdateViewModel
{
    public string Type { get; init; }
}

public class TicketTypeCreateUpdateViewModelValidator : BaseValidator<TicketTypeCreateUpdateViewModel>
{
    public TicketTypeCreateUpdateViewModelValidator()
    {
        RuleFor(p => p.Type).NotEmpty().MaximumLength(30);
    }
}

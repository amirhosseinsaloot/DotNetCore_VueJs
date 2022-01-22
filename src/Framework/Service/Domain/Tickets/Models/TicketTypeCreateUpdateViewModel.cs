namespace Service.Domain.Tickets.Models;

public record class TicketTypeCreateUpdateViewModel : ICreateViewModel, IUpdateViewModel
{
    public string Type { get; init; } = null!;
}

public class TicketTypeCreateUpdateViewModelValidator : BaseValidator<TicketTypeCreateUpdateViewModel>
{
    public TicketTypeCreateUpdateViewModelValidator()
    {
        RuleFor(p => p.Type).NotEmpty().MaximumLength(30);
    }
}

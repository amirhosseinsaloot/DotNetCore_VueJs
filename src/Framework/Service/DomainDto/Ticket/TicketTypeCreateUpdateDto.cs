namespace Service.DomainDto.Ticket;

public record class TicketTypeCreateUpdateDto : IDtoCreate, IDtoUpdate
{
    public string Type { get; init; } = null!;
}

public class TicketTypeCreateUpdateDtoValidator : BaseValidator<TicketTypeCreateUpdateDto>
{
    public TicketTypeCreateUpdateDtoValidator()
    {
        RuleFor(p => p.Type).NotEmpty().MaximumLength(30);
    }
}

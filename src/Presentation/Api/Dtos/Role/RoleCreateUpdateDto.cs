namespace Api.Dtos.Role;

public record class RoleCreateUpdateDto : IDtoCreate, IDtoUpdate
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;
}

public class RoleCreateUpdateDtoValidator : BaseValidator<RoleCreateUpdateDto>
{
    public RoleCreateUpdateDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(15);

        RuleFor(p => p.Description).NotEmpty().MaximumLength(100);
    }
}

namespace Infrastructure.Dto.User;

public record class UserCreateDto : IDtoCreate
{
    public string Username { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string Firstname { get; init; } = null!;

    public string Lastname { get; init; } = null!;

    public string Email { get; init; } = null!;

    public DateTime Birthdate { get; init; }

    public string? PhoneNumber { get; init; }

    public GenderType Gender { get; init; }

    public int TeamId { get; init; }
}

public class UserCreateDtoValidator : BaseValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(p => p.Username).NotEmpty().MaximumLength(40);

        RuleFor(p => p.Firstname).NotEmpty().MaximumLength(35);

        RuleFor(p => p.Lastname).NotEmpty().MaximumLength(35);

        RuleFor(p => p.Email).NotEmpty().EmailAddress().MaximumLength(320);

        RuleFor(p => p.Password).NotEmpty().MinimumLength(6).MaximumLength(127);

        RuleFor(p => p.PhoneNumber).MaximumLength(15).Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

        RuleFor(p => p.Gender).IsInEnum();

        RuleFor(p => p.TeamId).GreaterThanOrEqualTo(1);
    }
}

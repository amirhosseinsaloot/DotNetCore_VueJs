namespace Service.Domain.Users.Models;

public record class UserUpdateViewModel : IUpdateViewModel
{
    public string Username { get; init; } = default!;

    public string Firstname { get; init; } = default!;

    public string Lastname { get; init; } = default!;

    public string Email { get; init; } = default!;

    public DateTime Birthdate { get; init; }

    public string? PhoneNumber { get; init; }

    public GenderType Gender { get; init; }

    public int? ProfilePictureId { get; set; }

    public int? TeamId { get; init; }
}

public class UserUpdateViewModelValidator : BaseValidator<UserUpdateViewModel>
{
    public UserUpdateViewModelValidator()
    {
        RuleFor(p => p.Username).NotEmpty().MaximumLength(40);

        RuleFor(p => p.Firstname).NotEmpty().MaximumLength(35);

        RuleFor(p => p.Lastname).NotEmpty().MaximumLength(35);

        RuleFor(p => p.Email).NotEmpty().EmailAddress().MaximumLength(320);

        RuleFor(p => p.PhoneNumber).MaximumLength(15).Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

        RuleFor(p => p.Gender).IsInEnum();

        When(p => p.TeamId.HasValue, () => RuleFor(p => p.TeamId).GreaterThanOrEqualTo(1));
    }
}

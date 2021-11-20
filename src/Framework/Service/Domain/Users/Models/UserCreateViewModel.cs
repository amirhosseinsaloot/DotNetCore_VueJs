using Core.Enums;
using Core.Interfaces;
using Core.Validations;
using FluentValidation;

namespace Services.Domain;

public record class UserCreateViewModel : ICreateViewModel
{
    public string Username { get; init; }

    public string Password { get; init; }

    public string Firstname { get; init; }

    public string Lastname { get; init; }

    public string Email { get; init; }

    public DateTime Birthdate { get; init; }

    public string PhoneNumber { get; init; }

    public GenderType Gender { get; init; }

    public int TeamId { get; init; }
}

public class UserCreateViewModelValidator : BaseValidator<UserCreateViewModel>
{
    public UserCreateViewModelValidator()
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

﻿using Core.Interfaces;
using Core.Validations;
using FluentValidation;

namespace Services.Domain;

public record class TokenRequest : IDto
{
    public string GrantType { get; init; }

    public string Username { get; init; }

    public string Password { get; init; }

    public string RefreshToken { get; init; }

    public string AccessToken { get; init; }
}

public class TokenRequestValidator : BaseValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        List<string> validGrantTypes = new List<string>() { "password", "refresh_token" };

        RuleFor(p => p.GrantType)
            .NotEmpty().MaximumLength(15)
            .Must(p => validGrantTypes.Contains(p?.ToLower()))
            .WithMessage("Not valid grantType");

        RuleFor(p => p.Username).NotEmpty().MaximumLength(40);

        RuleFor(p => p.Password).NotEmpty().MinimumLength(6).MaximumLength(127);

        RuleFor(p => p.RefreshToken).MaximumLength(50);
    }
}

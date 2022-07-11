using Domain.Exceptions;
using Domain.Interfaces.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Validations;

public abstract class BaseValidator<TDto> : AbstractValidator<TDto>, IValidatorInterceptor
    where TDto : IDto
{
    public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
    {
        if (result.IsValid is false)
        {
            throw new BadRequestException("Model validation errors occured.", result.Errors);
        }

        return result;
    }

    public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
    {
        return commonContext;
    }
}

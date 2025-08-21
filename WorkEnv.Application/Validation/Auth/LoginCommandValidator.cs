using System.Data;
using System.Text.RegularExpressions;
using FluentValidation;
using WorkEnv.Application.CQRS.Auth.Login;
using WorkEnv.Application.Services;

namespace WorkEnv.Application.Validation.Auth;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(register => register.email)
            .EmailAddress()
            .NotEmpty()
            .WithMessage("Email must be not empty.")
            .NotNull()
            .WithMessage("Email cannot be null.");
        RuleFor(c => c.password)
            .NotEmpty()
            .WithMessage("Password must be not empty.")
            .NotNull()
            .WithMessage("Password cannot be null.")
            .Must(Base64Validation.BeValidBase64)
            .WithMessage("Password must be in base64 format.")
            .MaximumLength(200)
            .WithMessage("Password in base64 must not exceed 200 characters.");
    }
}
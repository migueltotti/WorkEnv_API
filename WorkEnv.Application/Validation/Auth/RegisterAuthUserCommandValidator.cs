using System.Text.RegularExpressions;
using FluentValidation;
using WorkEnv.Application.CQRS.Auth.Register;
using WorkEnv.Application.Extensions;

namespace WorkEnv.Application.Validation.Auth;

public class RegisterAuthUserCommandValidator : AbstractValidator<RegisterAuthUserCommand>
{
    public RegisterAuthUserCommandValidator()
    {
        RuleFor(register => register.name)
            .MinimumLength(5)
            .WithMessage("Name must have at least 5 characters")
            .MaximumLength(200)
            .WithMessage("Name must have at maximum 200 characters")
            .NotEmpty()
            .WithMessage("Name must not be empty.")
            .NotNull()
            .WithMessage("Name must not be null.");
        RuleFor(register => register.email)
            .MinimumLength(10)
            .WithMessage("Email must be more than 10 characters.")
            .MaximumLength(200)
            .WithMessage("Email must be less than 100 characters.")
            .EmailAddress()
            .NotEmpty()
            .WithMessage("Email must be not empty.")
            .NotNull()
            .WithMessage("Email cannot be null.");
        RuleFor(u => u.password)
            .MinimumLength(8)
            .WithMessage("Password must be more than 8 characters.")
            .MaximumLength(30)
            .WithMessage("Password must be less than 80 characters.")
            .Must(PasswordValidation.IsValidPassword)
            .WithMessage("Password must have one uppercase letter, one lowercase letter, one number and one special character.")
            .NotEmpty()
            .WithMessage("Password must be not empty.")
            .NotNull()
            .WithMessage("Password cannot be null.");
    }
}
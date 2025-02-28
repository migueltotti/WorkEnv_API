using FluentValidation;
using WorkEnv.Application.CQRS.User.Command.Register;

namespace WorkEnv.Application.Validation.User;

public class RegisterCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.Name)
            .MinimumLength(5)
            .WithMessage("Name must have at least 5 characters")
            .MaximumLength(80)
            .WithMessage("Name must have at maximum 80 characters")
            .NotEmpty()
            .WithMessage("Name must not be empty.")
            .NotNull()
            .WithMessage("Name must not be null.");
        RuleFor(c => c.Email)
            .MinimumLength(10)
            .WithMessage("Email must be more than 10 characters.")
            .MaximumLength(100)
            .WithMessage("Email must be less than 100 characters.")
            .NotEmpty()
            .WithMessage("Email must be not empty.")
            .NotNull()
            .WithMessage("Email cannot be null.");
        RuleFor(c => c.Password)
            .MinimumLength(8)
            .WithMessage("Password must be more than 8 characters.")
            .MaximumLength(30)
            .WithMessage("Password must be less than 80 characters.")
            .NotEmpty()
            .WithMessage("Password must be not empty.")
            .NotNull()
            .WithMessage("Password cannot be null.");
        RuleFor(c => c.DateBirth)
            .GreaterThan(DateTime.Now)
            .WithMessage("Date Birth must be greater than today's date.")
            .NotEmpty()
            .WithMessage("Date Birth must be not empty.")
            .NotNull()
            .WithMessage("Date Birth cannot be null.");
    }
}
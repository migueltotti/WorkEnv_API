using FluentValidation;
using WorkEnv.Application.CQRS.User.Command.ChangeEmail;

namespace WorkEnv.Application.Validation.User;

public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator()
    {
        RuleFor(c => c.userId)
            .NotEmpty()
            .WithMessage("User Id must be not empty.")
            .NotNull()
            .WithMessage("User Id cannot be null.");
        RuleFor(c => c.newEmail)
            .MinimumLength(10)
            .WithMessage("New Email must be more than 10 characters.")
            .MaximumLength(100)
            .WithMessage("New Email must be less than 100 characters.")
            .NotEmpty()
            .WithMessage("New Email must be not empty.")
            .NotNull()
            .WithMessage("New Email cannot be null.");
    }
}
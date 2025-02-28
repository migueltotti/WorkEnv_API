using FluentValidation;
using WorkEnv.Application.CQRS.User.Command.ChangeEmail;
using WorkEnv.Application.CQRS.User.Command.ChangeName;

namespace WorkEnv.Application.Validation.User;

public class ChangeNameCommandValidator : AbstractValidator<ChangeNameCommand>
{
    public ChangeNameCommandValidator()
    {
        RuleFor(c => c.userId)
            .NotEmpty()
            .WithMessage("User Id must be not empty.")
            .NotNull()
            .WithMessage("User Id cannot be null.");
        RuleFor(c => c.newName)
            .MinimumLength(5)
            .WithMessage("New Name must be more than 5 characters.")
            .MaximumLength(80)
            .WithMessage("New Name must be less than 80 characters.")
            .NotEmpty()
            .WithMessage("New Name must be not empty.")
            .NotNull()
            .WithMessage("New Name cannot be null.");
    }
}
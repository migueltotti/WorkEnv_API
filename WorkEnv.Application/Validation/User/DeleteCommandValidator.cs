using FluentValidation;
using WorkEnv.Application.CQRS.User.Command.Delete;

namespace WorkEnv.Application.Validation.User;

public class DeleteCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(c => c.userId)
            .NotEmpty()
            .WithMessage("User Id must be not empty.")
            .NotNull()
            .WithMessage("User Id cannot be null.");
    }
}
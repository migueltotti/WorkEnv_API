using FluentValidation;
using WorkEnv.Application.CQRS.Role.Command.Delete;

namespace WorkEnv.Application.Validation.Role;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(c => c.roleId)
            .NotEmpty()
            .WithMessage("Role Id must be not empty.")
            .NotNull()
            .WithMessage("Role Id must be not null.");
    }
}
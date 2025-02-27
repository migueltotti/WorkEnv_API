using FluentValidation;
using WorkEnv.Application.CQRS.Role.Command.Create;

namespace WorkEnv.Application.Validation.Role;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(c => c.name)
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters.")
            .NotEmpty()
            .WithMessage("Name must be not empty.")
            .NotNull()
            .WithMessage("Name must be not null.");
        RuleFor(c => c.description)
            .MaximumLength(300)
            .WithMessage("Name must not exceed 300 characters.")
            .NotEmpty()
            .WithMessage("Description must be not empty.")
            .NotNull()
            .WithMessage("Description must be not null.");
    }
}
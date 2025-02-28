using FluentValidation;
using WorkEnv.Application.CQRS.WorkSpace.Command.Create;

namespace WorkEnv.Application.Validation.WorkSpace;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(c => c.ownerId)
            .NotNull()
            .NotEmpty();
        RuleFor(c => c.masterCode)
            .MaximumLength(30)
            .NotNull()
            .NotEmpty();
    }
}
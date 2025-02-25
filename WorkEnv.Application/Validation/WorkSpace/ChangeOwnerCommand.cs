using FluentValidation;
using WorkEnv.Application.CQRS.WorkSpace.Command.ChangeOwner;

namespace WorkEnv.Application.Validation.WorkSpace;

public class ChangeOwnerCommandValidator : AbstractValidator<ChangeOwnerCommand>
{
    public ChangeOwnerCommandValidator()
    {
        RuleFor(ch => ch.workSpaceId)
            .NotNull()
            .NotEmpty();
        RuleFor(ch => ch.newOwnerId)
            .NotNull()
            .NotEmpty();
        RuleFor(ch => ch.oldOwnerId)
            .NotNull()
            .NotEmpty();
    }
}
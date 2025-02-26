using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.ChangeAccessOption;
using WorkEnv.Application.CQRS.Activity.Command.ChangeAccessPassword;

namespace WorkEnv.Application.Validation.Activity;

public class ChangeAccessOptionCommandValidator : AbstractValidator<ChangeAccessOptionsCommand>
{
    public ChangeAccessOptionCommandValidator()
    {
        RuleFor(c => c.activityId)
            .NotEmpty()
            .WithMessage("ActivityId must be not null.")
            .NotNull()
            .WithMessage("ActivityId must be not empty");
        RuleFor(c => c.adminOrOwnerId)
            .NotEmpty()
            .WithMessage("AdminOrOwnerId must be not null.")
            .NotNull()
            .WithMessage("AdminOrOwnerId must be not empty");
        RuleFor(c => c.accessOption)
            .NotEmpty()
            .WithMessage("AdminOrOwnerId must be not null.")
            .NotNull()
            .WithMessage("AdminOrOwnerId must be not empty");
    }
}
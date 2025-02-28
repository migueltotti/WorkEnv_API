using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.ChangeAccessPassword;

namespace WorkEnv.Application.Validation.Activity;

public class ChangeAccessPasswordCommandValidator : AbstractValidator<ChangeAccessPasswordCommand>
{
    public ChangeAccessPasswordCommandValidator()
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
    }
}
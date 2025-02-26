using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.UpdateStatus;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.Validation.Activity;

public class UpdateStatusValidator : AbstractValidator<UpdateStatusCommand>
{
    public UpdateStatusValidator()
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
        RuleFor(c => c.status)
            .NotEmpty()
            .WithMessage("Status must be not null.")
            .NotNull()
            .WithMessage("Status must be not empty");
    }
}
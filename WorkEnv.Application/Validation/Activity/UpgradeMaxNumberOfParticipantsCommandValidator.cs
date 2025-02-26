using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.UpgradeMaxNumberOfParticipants;

namespace WorkEnv.Application.Validation.Activity;

public class UpgradeMaxNumberOfParticipantsCommandValidator : AbstractValidator<UpgradeMaxNumberOfParticipantsCommand>
{
    public UpgradeMaxNumberOfParticipantsCommandValidator()
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
        RuleFor(c => c.numberOfParticipants)
            .GreaterThan(0)
            .WithMessage("MaxNumberOfParticipants must be greater than 0")
            .NotEmpty()
            .WithMessage("MaxNumberOfParticipants must be not null.")
            .NotNull()
            .WithMessage("MaxNumberOfParticipants must be not empty");
    }
}
using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.ChangePrivacy;

namespace WorkEnv.Application.Validation.Activity;

public class ChangePrivacyCommandValidator : AbstractValidator<ChangePrivacyCommand>
{
    public ChangePrivacyCommandValidator()
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
        RuleFor(c => c.privacy)
            .NotEmpty()
            .WithMessage("Privacy must be not null.")
            .NotNull()
            .WithMessage("Privacy must be not empty");
    }
}
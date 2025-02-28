using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.SendUserInvite;

namespace WorkEnv.Application.Validation.Activity;

public class SendUserInviteValidator : AbstractValidator<SendUserInviteCommand>
{
    public SendUserInviteValidator()
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
        RuleFor(c => c.userId)
            .NotEmpty()
            .WithMessage("UserId must be not null.")
            .NotNull()
            .WithMessage("UserId must be not empty");
    }
}
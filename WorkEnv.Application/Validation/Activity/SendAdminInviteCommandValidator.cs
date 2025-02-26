using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.SendAdminInvite;

namespace WorkEnv.Application.Validation.Activity;

public class SendAdminInviteCommandValidator : AbstractValidator<SendAdminInviteCommand>
{
    public SendAdminInviteCommandValidator()
    {
        RuleFor(c => c.activityId)
            .NotEmpty()
            .WithMessage("ActivityId must be not null.")
            .NotNull()
            .WithMessage("ActivityId must be not empty");
        RuleFor(c => c.ownerId)
            .NotEmpty()
            .WithMessage("OwnerId must be not null.")
            .NotNull()
            .WithMessage("OwnerId must be not empty");
        RuleFor(c => c.newAdminId)
            .NotEmpty()
            .WithMessage("NewAdminId must be not null.")
            .NotNull()
            .WithMessage("NewAdminId must be not empty");
    }
}
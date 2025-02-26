using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.AcceptUserInvite;

namespace WorkEnv.Application.Validation.Activity;

public class AcceptUserInviteCommandValidator : AbstractValidator<AcceptUserInviteCommand>
{
    public AcceptUserInviteCommandValidator()
    {
        RuleFor(c => c.activityId)
            .NotEmpty()
            .WithMessage("ActivityId must be not null.")
            .NotNull()
            .WithMessage("ActivityId must be not empty");
        RuleFor(c => c.userId)
            .NotEmpty()
            .WithMessage("UserId must be not null.")
            .NotNull()
            .WithMessage("UserId must be not empty");
        RuleFor(c => c.password)
            .Must(p => p.Length == 12)
            .WithMessage("Password must be 12 characters.")
            .NotEmpty()
            .WithMessage("AdminOrOwnerId must be not null.")
            .NotNull()
            .WithMessage("AdminOrOwnerId must be not empty");
    }
}
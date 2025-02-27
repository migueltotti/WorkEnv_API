using FluentValidation;
using WorkEnv.Application.CQRS.Event.Command.ChangeEventDate;

namespace WorkEnv.Application.Validation.Event;

public class ChangeEventDateCommandValidator : AbstractValidator<ChangeEventDateCommand>
{
    public ChangeEventDateCommandValidator()
    {
        RuleFor(c => c.eventId)
            .NotEmpty()
            .WithMessage("Event Id must be not empty.")
            .NotNull()
            .WithMessage("Event Id must be not null.");
        RuleFor(c => c.adminOrOwnerId)
            .NotEmpty()
            .WithMessage("Admin or Owner Id must be not empty.")
            .NotNull()
            .WithMessage("Admin or Owner Id must be not null.");
        RuleFor(c => c.newEventDate)
            .NotEmpty()
            .WithMessage("New Event Date must be not empty.")
            .NotNull()
            .WithMessage("New Event Date must be not null.");
    }
}
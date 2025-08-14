using FluentValidation;
using WorkEnv.Application.CQRS.Event.Command.Create;
using WorkEnv.Application.Services;

namespace WorkEnv.Application.Validation.Event;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(c => c.workSpaceId)
            .NotEmpty()
            .WithMessage("WorkSpace Id must be not empty.")
            .NotNull()
            .WithMessage("WorkSpace Id must be not null.");
        RuleFor(c => c.ownerId)
            .NotEmpty()
            .WithMessage("Owner Id must be not empty.")
            .NotNull()
            .WithMessage("Owner Id must be not null.");
        RuleFor(c => c.maxNumberOfParticipants)
            .GreaterThan(0)
            .WithMessage("Max number of participants must be greater than 0.")
            .NotEmpty()
            .WithMessage("Max Number Of Participants must be not empty.")
            .NotNull()
            .WithMessage("Max Number Of Participants must be not null.");
        RuleFor(c => c.name)
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.")
            .NotEmpty()
            .WithMessage("Name must be not empty.")
            .NotNull()
            .WithMessage("Name must be not null.");
        RuleFor(c => c.privacy)
            .Must(PrivacyService.CheckPrivacy)
            .WithMessage("Privacy must be private or public.")
            .NotEmpty()
            .WithMessage("Privacy must be not empty.")
            .NotNull()
            .WithMessage("Privacy must be not null.");
        RuleFor(c => c.TaskStatus)
            .Must(StatusService.CheckActivityStatus)
            .WithMessage("Status be created, pending, completed or canceled.")
            .NotEmpty()
            .WithMessage("Status must be not empty.")
            .NotNull()
            .WithMessage("Status must be not null.");
        RuleFor(c => c.EventAccessOptionOptions)
            .Must(AccessService.CheckAccessOptions)
            .WithMessage("Access Options status be created, pending, completed or canceled.")
            .NotEmpty()
            .WithMessage("Access Options must be not empty.")
            .NotNull()
            .WithMessage("Access Options must be not null.");
        RuleFor(c => c.eventDate)
            .NotEmpty()
            .WithMessage("Start Date must be not empty.")
            .NotNull()
            .WithMessage("Start Date must be not null.");
    }
}
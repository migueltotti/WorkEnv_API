using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.Delete;

namespace WorkEnv.Application.Validation.Activity;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
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
        RuleFor(c => c.masterCode)
            .MaximumLength(30)
            .WithMessage("MasterCode lenght must be greater than 30 characters.")
            .NotEmpty()
            .WithMessage("MasterCode must be not null.")
            .NotNull()
            .WithMessage("MasterCode must be not empty");
    }
}
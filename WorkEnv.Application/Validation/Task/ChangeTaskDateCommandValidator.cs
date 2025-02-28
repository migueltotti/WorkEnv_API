using FluentValidation;
using WorkEnv.Application.CQRS.Task.Command.ChangeTaskDate;

namespace WorkEnv.Application.Validation.Task;

public class ChangeTaskDateCommandValidator : AbstractValidator<ChangeTaskDateCommand>
{
    public ChangeTaskDateCommandValidator()
    {
        RuleFor(c => c.taskId)
            .NotEmpty()
            .WithMessage("Task Id must be not empty.")
            .NotNull()
            .WithMessage("Task Id must be not null.");
        RuleFor(c => c.adminOrOwnerId)
            .NotEmpty()
            .WithMessage("Admin or Owner Id must be not empty.")
            .NotNull()
            .WithMessage("Admin or Owner Id must be not null.");
        RuleFor(c => c.newStartDate)
            .NotEmpty()
            .WithMessage("New Start Date must be not empty.")
            .NotNull()
            .WithMessage("New Start Date must be not null.");
        RuleFor(c => c.newEndDate)
            .NotEmpty()
            .WithMessage("New End Date must be not empty.")
            .NotNull()
            .WithMessage("New End Date must be not null.");
    }
}
using FluentValidation;
using WorkEnv.Application.CQRS.Activity.Command.ChangeAdmin;

namespace WorkEnv.Application.Validation.Activity;

public class ChangeAdminCommandValidator : AbstractValidator<ChangeAdminCommand>
{
    public ChangeAdminCommandValidator()
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
        RuleFor(c => c.newAdminId)
            .NotEmpty()
            .WithMessage("NewAdminId must be not null.")
            .NotNull()
            .WithMessage("NewAdminId must be not empty");
    }
}
using FluentValidation;
using WorkEnv.Application.CQRS.Message.Command.Create;
using WorkEnv.Application.Services;

namespace WorkEnv.Application.Validation.Message;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(c => c.activityId)
            .NotEmpty()
            .WithMessage("Activity Id must be not empty.")
            .NotNull()
            .WithMessage("Activity Id must be not null.");
        RuleFor(c => c.ownerOrAdminId)
            .NotEmpty()
            .WithMessage("Admin or Owner Id must be not empty.")
            .NotNull()
            .WithMessage("Admin or Owner Id must be not null.");
        RuleFor(c => c.title)
            .MaximumLength(80)
            .WithMessage("Name must not exceed 80 characters.")
            .NotEmpty()
            .WithMessage("Title must be not empty.")
            .NotNull()
            .WithMessage("Title must be not null.");
        RuleFor(c => c.content)
            .MaximumLength(500)
            .WithMessage("Name must not exceed 500 characters.")
            .NotEmpty()
            .WithMessage("Content must be not empty.")
            .NotNull()
            .WithMessage("Content must be not null.");
    }
}
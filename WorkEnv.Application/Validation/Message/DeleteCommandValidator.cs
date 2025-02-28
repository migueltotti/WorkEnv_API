using FluentValidation;
using WorkEnv.Application.CQRS.Message.Command.Delete;

namespace WorkEnv.Application.Validation.Message;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(c => c.messageId)
            .NotEmpty()
            .WithMessage("Message Id must be not empty.")
            .NotNull()
            .WithMessage("Message Id must be not null.");
    }
}
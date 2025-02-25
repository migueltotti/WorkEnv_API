using FluentValidation;
using MediatR;
using WorkEnv.Application.CQRS.WorkSpace.Command.Delete;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.Validation.WorkSpace;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(d => d.workSpaceId)
            .NotNull()
            .NotEmpty();
        RuleFor(d => d.ownerId)
            .NotNull()
            .NotEmpty();
    }
}
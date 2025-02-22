using MediatR;
using WorkEnv.Application.Result;

namespace WorkEnv.Application.CQRS.Activity.Command.ChangeAccessPassword;

public record ChangeAccessPasswordCommand(Guid activityId, Guid adminOrOwnerId)
    : IRequest<Result<object>>;
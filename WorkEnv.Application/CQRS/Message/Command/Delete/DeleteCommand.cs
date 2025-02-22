using MediatR;

namespace WorkEnv.Application.CQRS.Message.Command.Delete;

public record DeleteCommand(Guid activityId, Guid adminOrOwnerId, Guid messageId) : IRequest<Result.Result>;
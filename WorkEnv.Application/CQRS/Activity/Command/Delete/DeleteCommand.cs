using MediatR;

namespace WorkEnv.Application.CQRS.Activity.Command.Delete;

public record DeleteCommand(Guid activityId, Guid ownerId, string masterCode) : IRequest<Result.Result>;
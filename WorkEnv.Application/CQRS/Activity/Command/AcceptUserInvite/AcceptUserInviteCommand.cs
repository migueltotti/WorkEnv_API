using MediatR;

namespace WorkEnv.Application.CQRS.Activity.Command.AcceptUserInvite;

public record AcceptUserInviteCommand(Guid activityId, Guid userId, string password) : IRequest<Result.Result>;
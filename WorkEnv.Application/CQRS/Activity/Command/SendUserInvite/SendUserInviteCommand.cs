using MediatR;

namespace WorkEnv.Application.CQRS.Activity.Command.SendUserInvite;

public record SendUserInviteCommand(Guid activityId, Guid adminOrOwnerId, Guid userId) : IRequest<Result.Result>;
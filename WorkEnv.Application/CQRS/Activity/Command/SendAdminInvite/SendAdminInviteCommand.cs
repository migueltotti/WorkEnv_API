using MediatR;

namespace WorkEnv.Application.CQRS.Activity.Command.SendAdminInvite;

public record SendAdminInviteCommand(Guid activityId, Guid ownerId, Guid adminId) : IRequest<Result.Result>;
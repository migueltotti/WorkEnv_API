using MediatR;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.CQRS.Activity.Command.UpdateStatus;

public record UpdateStatusCommand(Guid activityId, Guid adminOrOwnerId, ActivityStatus status) : IRequest<Result.Result>;
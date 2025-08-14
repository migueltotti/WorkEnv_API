using MediatR;
using WorkEnv.Domain.Enum;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Application.CQRS.Activity.Command.UpdateStatus;

public record UpdateStatusCommand(Guid activityId, Guid adminOrOwnerId, TaskStatus status) : IRequest<Result.Result>;
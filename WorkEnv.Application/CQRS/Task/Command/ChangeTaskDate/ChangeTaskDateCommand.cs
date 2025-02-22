using MediatR;

namespace WorkEnv.Application.CQRS.Task.Command.ChangeTaskDate;

public record ChangeTaskDateCommand(
    Guid taskId, Guid adminOrOwnerId, DateTime newStartDate, DateTime newEndDate) : IRequest<Result.Result>;
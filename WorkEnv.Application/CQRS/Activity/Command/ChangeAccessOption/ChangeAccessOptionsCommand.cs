using MediatR;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.CQRS.Activity.Command.ChangeAccessOption;

public record ChangeAccessOptionsCommand(
    Guid activityId, Guid adminOrOwnerId, EventAccessOption EventAccessOptionOption) : IRequest<Result.Result>;
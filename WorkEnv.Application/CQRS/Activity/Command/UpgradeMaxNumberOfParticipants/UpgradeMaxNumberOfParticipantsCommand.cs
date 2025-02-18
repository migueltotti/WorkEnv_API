using MediatR;

namespace WorkEnv.Application.CQRS.Activity.Command.UpgradeMaxNumberOfParticipants;

public record UpgradeMaxNumberOfParticipantsCommand(
    Guid activityId, Guid adminOrOwnerId, int numberOfParticipants) : IRequest<Result.Result>;
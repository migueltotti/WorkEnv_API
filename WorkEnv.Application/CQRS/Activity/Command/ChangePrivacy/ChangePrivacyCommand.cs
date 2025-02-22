using MediatR;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.CQRS.Activity.Command.ChangePrivacy;

public record ChangePrivacyCommand(
    Guid activityId, Guid adminOrOwnerId, Privacy privacy) : IRequest<Result.Result>;
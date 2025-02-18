using MediatR;

namespace WorkEnv.Application.CQRS.Event.Command.ChangeEventDate;

public record ChangeEventDateCommand(
    Guid eventId, Guid adminOrOwnerId, DateTime newEventDate) : IRequest<Result.Result>;
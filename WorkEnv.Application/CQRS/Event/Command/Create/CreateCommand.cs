using MediatR;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.CQRS.Event.Command.Create;

public record CreateCommand(
    Guid workSpaceId,
    Guid ownerId,
    int maxNumberOfParticipants,
    string name,
    Privacy privacy,
    ActivityStatus activityStatus,
    Access accessOptions,
    DateTime eventDate,
    Guid? adminId = null
) : IRequest<Result<EventDTO>>;
using MediatR;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.CQRS.Task.Command.Create;

public record CreateCommand(
    Guid workSpaceId,
    int maxNumberOfParticipants,
    Privacy privacy,
    ActivityStatus activityStatus,
    Access accessOptions,
    DateTime startDate,
    DateTime endDate,
    Guid? adminId = null    
) : IRequest<Result<TaskDTO>>;
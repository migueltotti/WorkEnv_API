using MediatR;
using WorkEnv.Application.DTO.Task;
using WorkEnv.Application.Result;
using WorkEnv.Domain.Enum;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Application.CQRS.Task.Command.Create;

public record CreateCommand(
    Guid workSpaceId,
    Guid ownerId,
    int maxNumberOfParticipants,
    string name,
    Privacy privacy,
    TaskStatus TaskStatus,
    EventAccessOption EventAccessOptionOptions,
    DateTime startDate,
    DateTime endDate,
    Guid? adminId = null    
) : IRequest<Result<TaskDTO>>;
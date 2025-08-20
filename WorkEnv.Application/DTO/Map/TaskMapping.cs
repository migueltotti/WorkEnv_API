using WorkEnv.Application.DTO.Task;
using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;
using Task = WorkEnv.Domain.Entities.Task;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Application.DTO.Map;

public static class TaskMapping
{
    public static TaskDTO ToTaskDto(this Domain.Entities.Task @event)
    {
        return new TaskDTO(
            @event.Id,
            Guid.NewGuid(),
            @event.WorkSpaceId,
            0,
            0,
            Privacy.Private,
            TaskStatus.Completed,
            "",
            EventAccessOption.PasswordRequired,
            new AdminInvite("", DateTime.Now, ""),
            @event.StartDate,
            @event.EndDate
            );
    }
}
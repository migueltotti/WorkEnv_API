using WorkEnv.Application.DTO.Task;
using Task = WorkEnv.Domain.Entities.Task;

namespace WorkEnv.Application.DTO.Map;

public static class TaskMapping
{
    public static TaskDTO ToTaskDto(this Domain.Entities.Task @event)
    {
        return new TaskDTO(
            @event.Id,
            @event.AdminId,
            @event.WorkSpaceId,
            @event.NumberOfParticipants,
            @event.MaxNumberOfParticipants,
            @event.Privacy,
            @event.ActivityStatus,
            @event.AccessPassword,
            @event.AccessOptions,
            @event.AdminInviteCode,
            @event.StartDate,
            @event.EndDate
            );
    }
}
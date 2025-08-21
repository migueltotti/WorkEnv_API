using WorkEnv.Application.DTO.Task;
using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Application.Map;

public static class EventMapping
{
    public static EventDTO ToEventDto(this Event @event)
    {
        return new EventDTO(
            @event.Id,
            @event.AdminId,
            @event.WorkSpaceId,
            @event.NumberOfParticipants,
            @event.MaxNumberOfParticipants,
            @event.Privacy,
            TaskStatus.Canceled,
            "",
            EventAccessOption.PasswordRequired,
            new AdminInvite("", DateTime.Now, ""),
            DateTime.Now
            );
    }
}
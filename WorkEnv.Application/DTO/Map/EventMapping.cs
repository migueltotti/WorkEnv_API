using WorkEnv.Application.DTO.Task;
using WorkEnv.Domain.Entities;

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
            @event.ActivityStatus,
            @event.AccessPassword,
            @event.AccessOptions,
            @event.AdminInviteCode,
            @event.EventDate
            );
    }
}
using WorkEnv.Application.DTO.Activity;
using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Application.DTO.Map;

public static class ActivityMapping
{
    public static ActivityDTO ToActivityDTO(this Domain.Entities.Activity activity)
    {
        return new ActivityDTO(
            activity.Id,
            Guid.NewGuid(), 
            activity.WorkSpaceId,
            0,
            0,
            Privacy.Private,
            TaskStatus.Created,
            "",
            EventAccessOption.OpenToAll,
            new AdminInvite("", DateTime.Now,"")
        );
    }
}
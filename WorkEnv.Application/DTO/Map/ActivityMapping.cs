using WorkEnv.Application.DTO.Activity;

namespace WorkEnv.Application.DTO.Map;

public static class ActivityMapping
{
    public static ActivityDTO ToActivityDTO(this Domain.Entities.Activity activity)
    {
        return new ActivityDTO(
            activity.Id,
            activity.AdminId,
            activity.WorkSpaceId,
            activity.NumberOfParticipants,
            activity.MaxNumberOfParticipants,
            activity.Privacy,
            activity.ActivityStatus,
            activity.AccessPassword,
            activity.AccessOptions,
            activity.AdminInviteCode
        );
    }
}
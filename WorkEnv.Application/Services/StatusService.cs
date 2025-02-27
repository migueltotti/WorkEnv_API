using WorkEnv.Domain.Enum;

namespace WorkEnv.Application.Services;

public static class StatusService
{
    public static bool CheckActivityStatus(ActivityStatus status)
    {
        return status is ActivityStatus.Created or
            ActivityStatus.Pending or
            ActivityStatus.Completed or
            ActivityStatus.Canceled;
    }
}
using WorkEnv.Domain.Enum;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Application.Services;

public static class StatusService
{
    public static bool CheckActivityStatus(TaskStatus status)
    {
        return status is TaskStatus.Created or
            TaskStatus.Completed or
            TaskStatus.Canceled or 
            TaskStatus.Expired;
    }
}
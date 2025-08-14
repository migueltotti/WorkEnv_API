using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace WorkEnv.Domain.Entities;

public class Task : Activity
{
    public TaskStatus Status { get; private set; }
    public TaskPriority TaskPriority { get; private set; }

    // Task 1 - 0..* TaskAssignment
    public List<TaskAssignment> ResponsibleUsers { get; private set; } = [];

    private Task()
    {
    }

    public Task(TaskPriority taskPriority)
    {
        TaskPriority = taskPriority;
        Status = TaskStatus.Created;
    }

    public void CompleteTask()
    {
        Status = TaskStatus.Completed;
    }
    
    public void CancelTask()
    {
        Status = TaskStatus.Canceled;
    }
}
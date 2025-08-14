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

    public Task(Guid id, string? title, string? description, DateTime startDate, DateTime endDate, Guid workSpaceId, TaskPriority taskPriority) : base(id, title, description, startDate, endDate, workSpaceId)
    {
        TaskPriority = taskPriority;
        Status = TaskStatus.Created;
    }

    public Task(string? title, string? description, DateTime startDate, DateTime endDate, Guid workSpaceId, TaskPriority taskPriority) : base(title, description, startDate, endDate, workSpaceId)
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
    
    public void IncludeResponsible(TaskAssignment responsible)
    {
        if (Status is not (TaskStatus.Completed or TaskStatus.Canceled))
            throw new InvalidOperationException("Cannot include responsible user when Task Status is Completed or Cancelled");
        
        if(ResponsibleUsers.Exists(p => p.Id == responsible.Id) is true)
            throw new InvalidOperationException("Cannot include responsible user that is already in this task");
        
        ResponsibleUsers.Add(responsible);
    }
    
    public void ExcludeResponsible(TaskAssignment responsible)
    {
        if (Status is not (TaskStatus.Completed or TaskStatus.Canceled))
            throw new InvalidOperationException("Cannot exclude responsible user when Task Status is Completed or Cancelled");
        
        if(ResponsibleUsers.Exists(p => p.Id == responsible.Id) is true)
            throw new InvalidOperationException("Cannot exclude responsible user that is not in the task");
        
        ResponsibleUsers.Remove(responsible);
    }
}
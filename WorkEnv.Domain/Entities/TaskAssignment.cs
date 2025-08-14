namespace WorkEnv.Domain.Entities;

public class TaskAssignment
{
    public DateTime AssignedAt { get; private set; }
    
    // TaskAssignment 0..* - 1 User
    public Guid ResponsibleUserId { get; private set; }
    public User? ResponsibleUser { get; private set; }
    
    // TaskAssignment 0..* - 1 Task
    public Guid TaskId { get; private set; }
    public Task? Task { get; private set; }

    public TaskAssignment()
    {
    }

    public TaskAssignment(DateTime assignedAt, Guid responsibleUserId, Guid taskId)
    {
        AssignedAt = assignedAt;
        ResponsibleUserId = responsibleUserId;
        TaskId = taskId;
    }
}
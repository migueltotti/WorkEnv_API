namespace WorkEnv.Domain.Entities;

public class TaskAssignment
{
    public DateTime AssignedAt { get; private set; }
    public DateTime? ResignedAt { get; private set; }
    
    // TaskAssignment 0..* - 1 User
    public Guid AssignedUserId { get; private set; }
    public Collaborator? AssignedUser { get; private set; }
    
    // TaskAssignment 0..* - 1 Task
    public Guid TaskId { get; private set; }
    public Task? Task { get; private set; }

    private TaskAssignment()
    {
    }

    public TaskAssignment(DateTime assignedAt, Guid assignedUserId, Guid taskId)
    {
        AssignedAt = assignedAt;
        ResignedAt = null;
        AssignedUserId = assignedUserId;
        TaskId = taskId;
    }

    public void Resign()
    {
        ResignedAt = DateTime.Now;
    }
}
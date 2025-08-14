namespace WorkEnv.Domain.Entities;

public class TaskAssignment
{
    public Guid Id { get; private set; }
    public DateTime AssignedAt { get; private set; }
    
    // TaskAssignment 0..* - 1 User
    public Guid ResponsibleUserId { get; private set; }
    public User? ResponsibleUser { get; private set; }
    
    // TaskAssignment 0..* - 1 Task
    public Guid TaskId { get; private set; }
    public Task? Task { get; private set; }

    private TaskAssignment()
    {
    }

    public TaskAssignment(Guid id, Guid responsibleUserId, Guid taskId)
    {
        Id = id;
        AssignedAt = DateTime.Now;
        ResponsibleUserId = responsibleUserId;
        TaskId = taskId;
    }
}
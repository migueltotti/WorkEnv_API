using WorkEnv.Domain.Enum;
using WorkEnv.Domain.Services;
using WorkEnv.Domain.ValueObjects;

namespace WorkEnv.Domain.Entities;

public abstract class Activity
{
    public Guid Id { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public List<string> Images { get; private set; } = [];
    public DateTime CreatedAt { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    
    // Activity 0..* - 1 WorkSpace -> Composition
    public Guid WorkSpaceId { get; private set; }
    public WorkSpace WorkSpace { get; private set; }
    
    // Activity 1 - 0..* Message
    public List<Message> Messages { get; private set; } = [];

    protected Activity()
    {
    }

    protected Activity(string? title, string? description, DateTime startDate, DateTime endDate, Guid workSpaceId)
    {
        if(startDate > endDate) throw new ArgumentException("Start date cannot be greater than end date");
        
        Title = title;
        Description = description;
        CreatedAt = DateTime.Now;
        StartDate = startDate;
        EndDate = endDate;
        WorkSpaceId = workSpaceId;
    }

    protected Activity(Guid id, string? title, string? description, DateTime startDate, DateTime endDate, Guid workSpaceId)
    {
        if(startDate > endDate) throw new ArgumentException("Start date cannot be greater than end date");
        
        Id = id;
        Title = title;
        Description = description;
        CreatedAt = DateTime.Now;
        StartDate = startDate;
        EndDate = endDate;
        WorkSpaceId = workSpaceId;
    }

    public void ChangeTitle(string newTitle)
    {
        if(string.IsNullOrEmpty(newTitle)) throw new ArgumentNullException("Name cannot be empty or null!");
        
        Title = newTitle;
    }
    
    public void AddMessage(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (!message.ActivityId.Equals(Id))
            throw new ArgumentException("Message does not belong to this Activity.");
        
        Messages.Add(message);
    }
    
    public void DeleteMessage(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);
        
        if(Messages.Count is 0)
            throw new ArgumentException("There's no activities to delete.");

        if (!message.ActivityId.Equals(Id))
            throw new ArgumentException("Message does not belong to this Activity.");
        
        Messages.Remove(message);
    }

    public void ChangeDate(DateTime newStartDate, DateTime newEndDate)
    {
        if(newStartDate > newEndDate) throw new ArgumentException("Start date cannot be greater than end date.");
        
        if(CreatedAt < StartDate || CreatedAt > EndDate) throw new ArgumentException("Created date cannot be less than start date or greater than end date.");
        
        StartDate = newStartDate;
        EndDate = newEndDate;
    }
}
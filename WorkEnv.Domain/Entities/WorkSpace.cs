namespace WorkEnv.Domain.Entities;

public class WorkSpace
{
    public Guid Id { get; private set; }
    public string? Title { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public int NumberOfActivities { get; private set; }
    public int NumberOfCollaborators { get; private set; }
    public string SecretKey { get; private set; }

    // WorkSpace 0..* - 1 User (Owner)
    public Guid OwnerId { get; private set; }
    public User? Owner { get; private set; }
    
    // WorkSpace 1 - 0..* Activitie
    public List<Activity> Activities { get; private set; } = [];
    
    // WorkSpace 1 - 0..* Message
    public List<Message> Messages { get; private set; } = [];
    
    // WorkSpace 1 - 0..* Role
    public List<Role> Roles { get; private set; } = [];
    
    // WorkSpace 1 - 0..* Role
    public List<Collaboration> Collaborators { get; private set; } = [];
    
    
    private WorkSpace()
    {
    }

    public WorkSpace(Guid id, string? title, string secretKey, Guid ownerId)
    {
        Id = id;
        Title = title;
        NumberOfActivities = 0;
        NumberOfCollaborators = 0;
        SecretKey = secretKey;
        OwnerId = ownerId;
    }
    
    public WorkSpace(string? title, string secretKey, Guid ownerId)
    {
        Title = title;
        NumberOfActivities = 0;
        NumberOfCollaborators = 0;
        SecretKey = secretKey;
        OwnerId = ownerId;
    }

    public void ChangeOwner(Guid oldOwnerId, Guid newOwnerId)
    {
        if (!oldOwnerId.Equals(OwnerId))
            throw new InvalidOperationException("Old owner id mismatch!");
        if (!oldOwnerId.Equals(newOwnerId))
            throw new InvalidOperationException("Old owner must be different from new owner!");
        
        OwnerId = newOwnerId;
    }
    
    public void IncreaseNumberOfActivities()
    {
        NumberOfActivities++;
    }
    
    public void DecreaseNumberOfActivities()
    {
        if (NumberOfActivities == 0)
            throw new ArgumentOutOfRangeException("The number of activities cannot be negative.");

        NumberOfActivities--;
    }
    
    public void IncreaseNumberOfCollaborators()
    {
        NumberOfCollaborators++;
    }
    
    public void DecreaseNumberOfCollaborators()
    {
        if (NumberOfCollaborators == 0)
            throw new InvalidOperationException("The number of collaborators cannot be negative.");

        NumberOfCollaborators--;
    }
    
    public string? GetMasterCode(Guid ownerId)
    {
        return ownerId.Equals(OwnerId) ? SecretKey : null; 
    }
}
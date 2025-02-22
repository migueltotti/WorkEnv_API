namespace WorkEnv.Domain.Entities;

public class WorkSpace
{
    public Guid WorkSpaceId { get; private set; }
    public Guid OwnerId { get; private set; }
    public int NumberOfActivities { get; private set; }
    private string? _masterCode;

    public User Owner { get; private set; }
    public ICollection<Activity> Activities { get; private set; }

    private WorkSpace()
    {
    }

    public WorkSpace(Guid workSpaceId, Guid ownerId, string? masterCode)
    {
        WorkSpaceId = workSpaceId;
        _masterCode = masterCode;
        OwnerId = ownerId;
        NumberOfActivities = 0;
    }

    public void ChangeOwner(Guid oldOwnerId, Guid newOwnerId)
    {
        if (!oldOwnerId.Equals(OwnerId))
            throw new ArgumentNullException("oldOwnerId does not own this workSpace");
        
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
    
    public string? GetMasterCode(Guid ownerId)
    {
        return ownerId.Equals(OwnerId) ? _masterCode : null; 
    }
    
    public void AddActivity(Activity activity)
    {
        ArgumentNullException.ThrowIfNull(activity);
        
        if (!activity.WorkSpaceId.Equals(WorkSpaceId))
            throw new ArgumentException("The activity does not belong to indicated workspace.");
            
        Activities.Add(activity);
    }
    
    public void RemoveActivity(Activity activity)
    {
        ArgumentNullException.ThrowIfNull(activity);
        
        if (!activity.WorkSpaceId.Equals(WorkSpaceId))
            throw new ArgumentException("The activity does not belong to indicated workspace.");
            
        Activities.Remove(activity);
    }
}
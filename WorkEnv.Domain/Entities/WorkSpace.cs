namespace WorkEnv.Domain.Entities;

public class WorkSpace
{
    public Guid WorkSpaceId { get; private set; }
    public Guid OwnerId { get; private set; }
    public int NumberOfActivities { get; private set; }
    private string? MasterCode;

    public User Owner { get; private set; }
    public ICollection<Activity> Activities { get; set; }

    private WorkSpace()
    {
    }

    public WorkSpace(string? masterCode, Guid ownerId, int numberOfActivities)
    {
        WorkSpaceId = Guid.NewGuid();
        MasterCode = masterCode;
        OwnerId = ownerId;
        NumberOfActivities = numberOfActivities;
    }

    public bool ChangeOwner(Guid oldOwnerId, Guid newOwnerId)
    {
        if (!oldOwnerId.Equals(OwnerId))
            return false;
        
        OwnerId = newOwnerId;
        
        return true;
    }
    
    public void IncreaseNumberOfActivities()
    {
        NumberOfActivities++;
    }
    
    public void DecreaseNumberOfActivities()
    {
        if (NumberOfActivities < 0)
            throw new ArgumentOutOfRangeException("The number of activities cannot be negative.");

        NumberOfActivities--;
    }
    
    public string? GetMasterCode(Guid ownerId)
    {
        return ownerId.Equals(OwnerId) ? MasterCode : null; 
    }
    
    public void AddActivity(Activity activity)
    {
        ArgumentNullException.ThrowIfNull(activity);
        
        if (!activity.WorkSpaceId.Equals(WorkSpaceId))
            throw new ArgumentException("The activity does not belong to indicated workspace.");
            
        Activities.Add(activity);
    }
}
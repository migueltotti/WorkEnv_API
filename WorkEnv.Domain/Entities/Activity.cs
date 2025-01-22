using WorkEnv.Domain.Enum;
using WorkEnv.Domain.Services;
using WorkEnv.Domain.ValueObjects;

namespace WorkEnv.Domain.Entities;

public abstract class Activity
{
    public Guid Id { get; private set; }
    public Guid AdminId { get; private set; }
    public Guid WorkSpaceId { get; private set; }
    public int NumberOfParticipants { get; private set; }
    public int _maxNumberOfParticipants { get; private set; }
    public Privacy Privacy { get; private set; } 
    public ActivityStatus ActivityStatus { get; private set; }
    public string? AccessPassword { get; private set; }
    public Access AccessOptions { get; private set; }
    public (string, DateTime) AdminInviteCode { get; private set; }
    
    public User Admin { get; private set; } 
    public WorkSpace WorkSpace { get; private set; }
    public List<UserRoleActivity> Users { get; private set; } = [];
    public List<Message> Messages { get; private set; } = [];

    private Activity()
    {
    }
    
    public Activity(int maxNumberOfParticipants, Guid id, Guid adminId, Guid workSpaceId, Privacy privacy, string? accessPassword, Access accessOptions, (string, DateTime) adminInviteCode)
    {
        _maxNumberOfParticipants = maxNumberOfParticipants;
        Id = id;
        AdminId = adminId;
        WorkSpaceId = workSpaceId;
        NumberOfParticipants = 1;
        Privacy = privacy;
        AccessPassword = accessPassword;
        AccessOptions = accessOptions;
        AdminInviteCode = adminInviteCode;
    }

    public Activity(int maxNumberOfParticipants, Guid adminId, Guid workSpaceId, Privacy privacy, string? accessPassword, Access accessOptions)
    {
        _maxNumberOfParticipants = maxNumberOfParticipants;
        AdminId = adminId;
        WorkSpaceId = workSpaceId;
        NumberOfParticipants = 1;
        Privacy = privacy;
        AccessPassword = accessPassword;
        AccessOptions = accessOptions;
    }
    
    public void ChangeAdmin(Guid ownerOrAdminId)
    {
        if(!AdminId.Equals(ownerOrAdminId) || !WorkSpace.OwnerId.Equals(ownerOrAdminId))
            throw new AccessViolationException("Invalid OwnerId or OwnerId !");
        
        AdminId = ownerOrAdminId;
    } 
    
    public void UpgradeMaxNumberOfParticipants(int newMaxNumberOfParticipants)
    {
        if (newMaxNumberOfParticipants is <= 1)
            throw new ArgumentException("MaxNumberOfParticipants must be greater than 1");
        
        _maxNumberOfParticipants = newMaxNumberOfParticipants;
    } 
    
    public string ChangeAccessPassword(Guid ownerOrAdminId)
    {
        if(!AdminId.Equals(ownerOrAdminId) || !WorkSpace.OwnerId.Equals(ownerOrAdminId))
            throw new AccessViolationException("Invalid OwnerId or OwnerId !");
            
        AccessPassword = PasswordGenerator.GeneratePassword();

        return AccessPassword;
    } 
    
    public void ChangeAccessOptions(Guid ownerOrAdminId, Access accessOptions)
    {
        if(!AdminId.Equals(ownerOrAdminId) || !WorkSpace.OwnerId.Equals(ownerOrAdminId))
            throw new AccessViolationException("Invalid OwnerId or OwnerId !");
        
        AccessOptions = accessOptions;
    } 
    
    public void ChangPrivacy(Guid ownerOrAdminId, Privacy privacy)
    {
        if(!AdminId.Equals(ownerOrAdminId) || !WorkSpace.OwnerId.Equals(ownerOrAdminId))
            throw new AccessViolationException("Invalid OwnerId or OwnerId !");

        Privacy = privacy;
    } 
    
    public void UpdateStatus(Guid ownerOrAdminId, ActivityStatus activityStatus)
    {
        if(!AdminId.Equals(ownerOrAdminId) || !WorkSpace.OwnerId.Equals(ownerOrAdminId))
            throw new AccessViolationException("Invalid OwnerId or OwnerId !");
        
        ActivityStatus = activityStatus;
    } 
    
    public (int, DateTime) GenerateAdminInviteCode(Guid ownerId, int validationTimeInDays = 3)
    {
        if(!WorkSpace.OwnerId.Equals(ownerId))
            throw new AccessViolationException("Invalid OwnerId!");
        
        return new ValueTuple<int, DateTime>(new Random().Next(111111, 999999), DateTime.Now.AddDays(validationTimeInDays));
    } 
    
    public void AddUser(UserRoleActivity userRoleActivity)
    {
        if(userRoleActivity is null)
            throw new ArgumentNullException("UserRoleActivity cannot be null.");
        
        if(!userRoleActivity.ActivityId.Equals(Id))
            throw new ArgumentNullException("UserRoleActivity ActiviryId mismatch.");
        
        Users.Add(userRoleActivity);
    }
    
    public void RemoveUser(UserRoleActivity userRoleActivity)
    {
        if(userRoleActivity is null)
            throw new ArgumentNullException("UserRoleActivity cannot be null.");
        
        if(!userRoleActivity.ActivityId.Equals(Id))
            throw new ArgumentNullException("UserRoleActivity ActiviryId mismatch.");
        
        if(Users.Count is 0)
            throw new ArgumentNullException("There's no users to delete.");
        
        Users.Remove(userRoleActivity);
    }
    
    public void AddMessage(Message message)
    {
        if(message is null)
            throw new ArgumentNullException("Message cannot be null.");
        
        if(!message.ActivityId.Equals(Id))
            throw new ArgumentNullException("Message does not belong to this Activity.");
        
        Messages.Add(message);
    }
    
    public void DeleteMessage(Message message)
    {
        if(message is null)
            throw new ArgumentNullException("Message cannot be null.");
        
        if(!message.ActivityId.Equals(Id))
            throw new ArgumentNullException("Message does not belong to this Activity.");
        
        if(Messages.Count is 0)
            throw new ArgumentNullException("There's no activities to delete.");
        
        Messages.Remove(message);
    }
}
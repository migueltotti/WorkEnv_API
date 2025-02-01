using System.Runtime.InteropServices.JavaScript;
using WorkEnv.Domain.Enum;
using WorkEnv.Domain.Services;
using WorkEnv.Domain.ValueObjects;

namespace WorkEnv.Domain.Entities;

public abstract class Activity
{
    public Guid Id { get; private set; }
    public Guid? AdminId { get; private set; }
    public Guid WorkSpaceId { get; private set; }
    public int NumberOfParticipants { get; private set; }
    public int MaxNumberOfParticipants { get; private set; }
    public Privacy Privacy { get; private set; } 
    public ActivityStatus ActivityStatus { get; private set; }
    public string? AccessPassword { get; private set; }
    public Access AccessOptions { get; private set; }
    public AdminInvite AdminInviteCode { get; private set; }
    
    public User? Admin { get; private set; } 
    public WorkSpace WorkSpace { get; private set; }
    public ICollection<UserActivity> UserActivities { get; private set; } = [];
    public ICollection<Message> Messages { get; private set; } = [];

    protected Activity()
    {
    }

    protected Activity(Guid id, Guid workSpaceId, int maxNumberOfParticipants, Privacy privacy, ActivityStatus activityStatus, Access accessOptions, Guid? adminId = null)
    {
        Id = id;
        AdminId = adminId;
        WorkSpaceId = workSpaceId;
        MaxNumberOfParticipants = maxNumberOfParticipants;
        Privacy = privacy;
        NumberOfParticipants = 1;
        ActivityStatus = activityStatus;
        AccessOptions = accessOptions;
        AccessPassword = PasswordGenerator.GeneratePassword();
    }
    
    public void ChangeAdmin(Guid oldOwnerOrAdminId, Guid newAdminId)
    {
        if(!AdminId.Equals(oldOwnerOrAdminId) || !WorkSpace.OwnerId.Equals(oldOwnerOrAdminId))
            throw new AccessViolationException("Invalid OwnerId or AdminId!");
        
        AdminId = newAdminId;
    } 
    
    public void UpgradeMaxNumberOfParticipants(int newMaxNumberOfParticipants)
    {
        if (newMaxNumberOfParticipants is <= 1)
            throw new ArgumentException("MaxNumberOfParticipants must be greater than 1");
        
        MaxNumberOfParticipants = newMaxNumberOfParticipants;
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
    
    public AdminInvite GenerateAdminInviteCode(Guid ownerId, int validationTimeInDays = 3)
    {
        if(!WorkSpace.OwnerId.Equals(ownerId))
            throw new AccessViolationException("Invalid OwnerId!");
        
        return new AdminInvite(new Random().Next(111111, 999999), DateTime.Now.AddDays(validationTimeInDays));
    } 
    
    public void AddUser(UserActivity userActivity)
    {
        if(userActivity is null)
            throw new ArgumentNullException("UserRoleActivity cannot be null.");
        
        if(!userActivity.ActivityId.Equals(Id))
            throw new ArgumentException("UserRoleActivity ActiviryId mismatch.");
        
        UserActivities.Add(userActivity);
        NumberOfParticipants++;
    }
    
    public void RemoveUser(UserActivity userActivity)
    {
        if(userActivity is null)
            throw new ArgumentNullException("UserRoleActivity cannot be null.");
        
        if(!userActivity.ActivityId.Equals(Id))
            throw new ArgumentNullException("UserRoleActivity ActiviryId mismatch.");
        
        if(UserActivities.Count is 0)
            throw new ArgumentException("There's no users to delete.");
        
        UserActivities.Remove(userActivity);
        NumberOfParticipants--;
    }
    
    public void AddMessage(Message message)
    {
        if(message is null)
            throw new ArgumentNullException("Message cannot be null.");
        
        if(!message.ActivityId.Equals(Id))
            throw new ArgumentException("Message does not belong to this Activity.");
        
        Messages.Add(message);
    }
    
    public void DeleteMessage(Message message)
    {
        if(message is null)
            throw new ArgumentNullException("Message cannot be null.");
        
        if(!message.ActivityId.Equals(Id))
            throw new ArgumentException("Message does not belong to this Activity.");
        
        if(Messages.Count is 0)
            throw new ArgumentException("There's no activities to delete.");
        
        Messages.Remove(message);
    }
}
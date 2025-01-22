using WorkEnv.Domain.Enum;

namespace WorkEnv.Domain.Entities;

public class Event : Activity
{
    public DateTime EventDate { get; private set; }

    public Event(int maxNumberOfParticipants, 
        Guid id, 
        Guid adminId, 
        Guid workSpaceId, 
        Privacy privacy, 
        string? accessPassword, 
        Access accessOptions, 
        (string, DateTime) adminInviteCode, 
        DateTime eventDate) 
        : base(maxNumberOfParticipants, id, adminId, workSpaceId, privacy, accessPassword, accessOptions, adminInviteCode)
    {
        if(eventDate < DateTime.Now)
            throw new ArgumentException("Event date cannot be earlier than today.");
            
        EventDate = eventDate;
    }

    public Event(int maxNumberOfParticipants, 
        Guid adminId, 
        Guid workSpaceId, 
        Privacy privacy, 
        string? accessPassword, 
        Access accessOptions, 
        DateTime eventDate) 
        : base(maxNumberOfParticipants, adminId, workSpaceId, privacy, accessPassword, accessOptions)
    {
        if(eventDate < DateTime.Now)
            throw new ArgumentException("Event date cannot be earlier than today.");
        
        EventDate = eventDate;
    }
    
    public void ChangeEventDate(Guid ownerOrAdminId, DateTime newEventDate)
    {
        if(!AdminId.Equals(ownerOrAdminId) || !WorkSpace.OwnerId.Equals(ownerOrAdminId))
            throw new AccessViolationException("Invalid OwnerId or OwnerId !");
        
        if(newEventDate < DateTime.Now)
            throw new ArgumentException("Event date cannot be earlier than today.");
        
        EventDate = newEventDate;
    }
}
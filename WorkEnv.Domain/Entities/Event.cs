using WorkEnv.Domain.Enum;
using WorkEnv.Domain.ValueObjects;

namespace WorkEnv.Domain.Entities;

public class Event : Activity
{
    public DateTime EventDate { get; private set; }

    private Event()
    {
    }

    public Event(
        Guid workSpaceId,
        int maxNumberOfParticipants,
        Privacy privacy,
        ActivityStatus activityStatus,
        Access accessOptions,
        DateTime eventDate) 
        : base(workSpaceId, maxNumberOfParticipants, privacy, activityStatus, accessOptions)
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
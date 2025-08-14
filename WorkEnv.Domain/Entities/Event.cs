using WorkEnv.Domain.Enum;
using WorkEnv.Domain.Services;
using WorkEnv.Domain.ValueObjects;

namespace WorkEnv.Domain.Entities;

public class Event : Activity
{
    public string? Address { get; private set; }
    public int NumberOfParticipants { get; private set; }
    public int MaxNumberOfParticipants { get; private set; }
    public AdminInvite? AdminInvite { get; private set; }
    public Privacy Privacy { get; private set; }
    public EventStatus Status { get; private set; }
    public EventAccessOption AccessOption { get; private set; }
    public string? AccessKey { get; private set; }
    public DateTime AdminAdmittedAt { get; private set; }
    
    // Event 0..* - 1 User
    public Guid AdminId { get; private set; }
    public User? Admin { get; private set; }
    
    // Event 0..* - 1 EventParticipant
    public List<EventParticipant> Participants { get; private set; } = [];

    private Event()
    {
    }

    public Event(string? address, int maxNumberOfParticipants, Privacy privacy, EventAccessOption accessOption, Guid adminId)
    {
        Address = address;
        NumberOfParticipants = 0;
        MaxNumberOfParticipants = maxNumberOfParticipants;
        AdminInvite = null;
        Privacy = privacy;
        Status = EventStatus.Scheduled;
        AccessOption = accessOption;
        AccessKey = CodeGenerator.GenerateCode();
        AdminId = adminId;
    }

    public AdminInvite InviteAdmin()
    {
        AdminInvite = new AdminInvite(
            Code: CodeGenerator.GenerateCode(),
            ExpirationDate: DateTime.Now.AddMinutes(30)
        );
        
        return AdminInvite;
    }

    public void IncludeParticipant(EventParticipant participant)
    {
        if (Status is not EventStatus.Finished or EventStatus.Cancelled)
            throw new InvalidOperationException("Cannot include participant when Event Status is Finished or Cancelled");
        
        if(Participants.FirstOrDefault(p => p.Id == participant.Id) is not null)
            throw new InvalidOperationException("Cannot include participant that is already in this event");
        
        Participants.Add(participant);
    }
    
    public void ExcludeParticipant(EventParticipant participant)
    {
        if (Status is not EventStatus.Finished or EventStatus.Cancelled)
            throw new InvalidOperationException("Cannot exclude participant when Event Status is Finished or Cancelled");
        
        if(Participants.FirstOrDefault(p => p.Id == participant.Id) is not null)
            throw new InvalidOperationException("Cannot exclude participant that is not in the event");
        
        Participants.Remove(participant);
    }

    public void GenerateAccessKey()
    {
        AccessKey = CodeGenerator.GenerateCode();
    }

    public void InitiateEvent()
    {
        Status = EventStatus.InExecution;
    }

    public void FinishEvent()
    {
        Status = EventStatus.Finished;
    }

    public void CancelEvent()
    {
        Status = EventStatus.Cancelled;
    }
}
namespace WorkEnv.Domain.Entities;

public class EventParticipant
{
    public Guid Id { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    // EventParticipant 0..* - 1 User -> Composition
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    
    // EventParticipant 0..* - 1 Event -> Composition
    public Guid EventId { get; private set; }
    public Event? Event { get; private set; }

    // EventParticipant 0..* -- 0..1 Role
    public Guid? RoleId { get; private set; }
    public Role? Role { get; private set; }

    public EventParticipant()
    {
    }

    public EventParticipant(Guid userId, Guid eventId, Guid? roleId)
    {
        RegisteredAt = DateTime.Now;
        UserId = userId;
        EventId = eventId;
        RoleId = roleId;
    }

    public EventParticipant(Guid id, Guid userId, Guid eventId, Guid? roleId)
    {
        Id = id;
        RegisteredAt = DateTime.Now;
        UserId = userId;
        EventId = eventId;
        RoleId = roleId;
    }

    public void AssignRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        
        Role = role;
        RoleId = role.Id;
    }

    public void RemoveRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        
        if(!Role.Id.Equals(RoleId)) throw new ArgumentException("Role Id does not match");
        
        Role = null;
        RoleId = null;
    }
}
namespace WorkEnv.Domain.Entities;

public class EventParticipant
{
    public Guid Id { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    // EventParticipant 0..* - 1 User
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    
    // EventParticipant 0..* - 1 Event
    public Guid EventId { get; private set; }
    public Event? Event { get; private set; }

    // EventParticipant 0..* -- 0..1 Role
    public Guid? RoleId { get; private set; }
    public Role? Role { get; private set; }

    public EventParticipant()
    {
    }

    public EventParticipant(DateTime registeredAt, Guid userId, Guid eventId, Guid? roleId)
    {
        RegisteredAt = registeredAt;
        UserId = userId;
        EventId = eventId;
        RoleId = roleId;
    }

    public EventParticipant(Guid id, DateTime registeredAt, Guid userId, Guid eventId, Guid? roleId)
    {
        Id = id;
        RegisteredAt = registeredAt;
        UserId = userId;
        EventId = eventId;
        RoleId = roleId;
    }

    public void AssignRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        
        Role = role;
        RoleId = role.RoleId;
    }

    public void RemoveRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        
        if(!Role.RoleId.Equals(RoleId)) throw new ArgumentException("Role Id does not match");
        
        Role = null;
        RoleId = null;
    }
}
using WorkEnv.Domain.Enum;

namespace WorkEnv.Domain.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public List<Permission> Permissions { get; private set; } = [];
    
    // Role 0..* - 1 WorkSpace -> Composition
    public Guid WorkSpaceId { get; private set; }
    public WorkSpace? WorkSpace { get; private set; }

    // Role 0..1 - 0..* EventParticipant -> Aggregation
    public Guid? EventParticipantId { get; private set; }
    public EventParticipant? EventParticipant { get; private set; }
    
    // Role 0..1 - 0..* Collaborator -> Aggregation
    public Guid? CollaboratorId { get; private set; }
    public Collaboration? Collaborator { get; private set; }

    private Role()
    {
    }

    public Role(Guid Id, string? name, string? description, Guid workSpaceId, Guid? eventParticipantId)
    {
        Id = Id;
        Name = name;
        Description = description;
        WorkSpaceId = workSpaceId;
        EventParticipantId = eventParticipantId;
    }

    public Role(string? name, string? description, Guid workSpaceId, Guid? eventParticipantId)
    {
        Name = name;
        Description = description;
        WorkSpaceId = workSpaceId;
        EventParticipantId = eventParticipantId;
    }

    public void ChangeName(string newName)
    {
        ArgumentNullException.ThrowIfNull(newName);
        
        Name = newName;
    }
    public void ChangeDescription(string newDescription)
    {
        ArgumentNullException.ThrowIfNull(newDescription);
        
        Description = newDescription;
    }

    public void AddPermission(Permission permission)
    {
        ArgumentNullException.ThrowIfNull(permission);

        if(Permissions.Exists(p => p.Equals(permission)) is true)
            throw new InvalidOperationException("Permission already exists.");
        
        Permissions.Add(permission);
    }
    
    public void RemovePermission(Permission permission)
    {
        ArgumentNullException.ThrowIfNull(permission);

        if(Permissions.Exists(p => p.Equals(permission)) is true)
            throw new InvalidOperationException("Permission doesn't exists.");
        
        Permissions.Remove(permission);
    }
}
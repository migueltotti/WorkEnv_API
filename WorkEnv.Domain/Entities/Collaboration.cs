namespace WorkEnv.Domain.Entities;

public class Collaboration
{
    public Guid Id { get; private set; }    
    public DateTime JoinedAt { get; private set; }
    
    // Collaborator 0..* - 1 User -> Composition
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    
    // Collaborator 0..* - 1 WorkSpace -> Composition
    public Guid WorkSpaceId { get; private set; }
    public WorkSpace? WorkSpace { get; private set; }
    
    // Collaborator 0..* - 0..1 Role
    public Guid? RoleId { get; private set; }
    public Role? Role { get; private set; }

    private Collaboration()
    {
    }

    public Collaboration(Guid id, DateTime joinedAt, Guid userId, Guid workSpaceId, Guid? roleId)
    {
        Id = id;
        JoinedAt = joinedAt;
        UserId = userId;
        WorkSpaceId = workSpaceId;
        RoleId = roleId;
    }

    public Collaboration(DateTime joinedAt, Guid userId, Guid workSpaceId, Guid? roleId)
    {
        JoinedAt = joinedAt;
        UserId = userId;
        WorkSpaceId = workSpaceId;
        RoleId = roleId;
    }

    public void AddRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        
        RoleId = role.Id;
        Role = role;
    }
    
    public void RemoveRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        
        RoleId = null;
        Role = null;
    }
}
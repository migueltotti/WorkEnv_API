namespace WorkEnv.Domain.Entities;

public class Role
{
    public Guid RoleId { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }

    public ICollection<UserActivity> UserActivities { get; private set; } = [];

    private Role()
    {
    }

    public Role(string? name, string? description)
    {
        RoleId = Guid.NewGuid();
        Name = name;
        Description = description;
    }
    
    public void ChangeRoleName(string newName)
    {
        if (String.IsNullOrEmpty(newName))
            throw new ArgumentNullException("Role name should be not null or empty!");
        
        Name = newName;
    }
    public void ChangeDescription(string newDescription)
    {
        if (String.IsNullOrEmpty(newDescription))
            throw new ArgumentNullException("Role description should be not null or empty!");
        
        Description = newDescription;
    }
}
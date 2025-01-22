namespace WorkEnv.Domain.Entities;

public class UserRoleActivity
{
    public Guid UserId { get; private set; }
    public Guid ActivityId { get; private set; }
    public Guid? RoleId { get; private set; }
    
    public User User { get; private set; }
    public Activity Activity { get; private set; }
    public Role? Role { get; private set; }

    public UserRoleActivity(Guid userId, Guid activityId, Guid roleId)
    {
        UserId = userId;
        ActivityId = activityId;
        RoleId = roleId;
    }
    
    public UserRoleActivity(Guid userId, Guid activityId)
    {
        UserId = userId;
        ActivityId = activityId;
    }
}
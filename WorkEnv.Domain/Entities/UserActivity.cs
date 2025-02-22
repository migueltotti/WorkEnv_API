namespace WorkEnv.Domain.Entities;

public class UserActivity
{
    public Guid UserId { get; private set; }
    public Guid ActivityId { get; private set; }
    public Guid? RoleId { get; private set; }
    
    public User User { get; private set; }
    public Activity Activity { get; private set; }
    public Role? Role { get; private set; }

    private UserActivity() { }
    
    public UserActivity(Guid userId, Guid activityId, Guid roleId)
    {
        UserId = userId;
        ActivityId = activityId;
        RoleId = roleId;
    }
    
    public UserActivity(Guid userId, Guid activityId)
    {
        UserId = userId;
        ActivityId = activityId;
    }
}
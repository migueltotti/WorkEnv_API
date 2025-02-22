using System.Collections;

namespace WorkEnv.Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string? Password { get; private set; }
    public DateTime DateBirth { get; private set; }
    
    // Authentication
    public string? _refreshToken { get; private set; }
    public DateTime _expirationTime { get; private set; }
    
    public ICollection<WorkSpace> WorkSpaces { get; private set; } = [];
    public ICollection<UserActivity> UserActivities { get; private set; } = [];

    private User()
    {
    }

    public User(Guid userId,string? name, string? email, string? password, DateTime dateBirth)
    {
        UserId = userId;
        Name = name;
        Email = email;
        Password = password;
        DateBirth = dateBirth;
    }
    
    public void ChangeName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            throw new ArgumentNullException("New Name cannot be null or empty.");
        
        Name = newName;
    }
    
    public void ChangeEmail(string newEmail)
    {
        if (string.IsNullOrEmpty(newEmail))
            throw new ArgumentNullException("New Email cannot be null or empty.");
        
        Email = newEmail;
    }
    
    public void ChangePassword(string newPassword)
    {
        if (string.IsNullOrEmpty(newPassword))
            throw new ArgumentNullException("New Password cannot be null or empty.");
        
        Password = newPassword;
    }

    public void AddWorkSpace(WorkSpace workSpace)
    {
        if(workSpace is null)
            throw new ArgumentNullException("WorkSpace cannot be null.");
        
        if(!workSpace.OwnerId.Equals(UserId))
            throw new ArgumentNullException("User does not own this workspace.");
        
        WorkSpaces.Add(workSpace);
    }
    
    public void AddUserToActivity(UserActivity userActivity)
    {
        if (userActivity is null)
            throw new ArgumentNullException("WorkSpace cannot be null.");
        
        if(!userActivity.UserId.Equals(UserId))
            throw new ArgumentNullException("UserActivity UserId mismatch.");
        
        UserActivities.Add(userActivity);
    }
}
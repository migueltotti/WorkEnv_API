using System.Collections;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string? Password { get; private set; }
    public string? CpfCnpj { get; private set; }
    public DateTime DateBirth { get; private set; }
    public string? ProfilePicture { get; private set; }
    public string? PersonalDescription { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public Privacy Privacy { get; private set; }
    public DateTime? LastLogin { get; private set; }
    
    // User 1 - 0..* WorkSpace (Owner)
    public List<WorkSpace> WorkSpaces { get; private set; } = [];
    
    // User 1 - 0..* Collaborator
    public List<Collaboration> Collaborations { get; private set; } = [];
    
    // User 2 - 0..* FollowRequest
    public List<FollowRequest> FollowRequests { get; private set; } = [];
    
    // User 1 - 0..* EventParticipant
    public List<EventParticipant> EventParticipants { get; private set; } = [];
    
    // User 1 - 0..* Event
    public List<Event> Events { get; private set; } = [];
    
    // User 1 - 0..* TaskAssignment
    public List<TaskAssignment> TaskAssignments { get; private set; } = [];

    private User()
    {
    }

    public User(Guid userId, string? name, string? email, string? password, string? cpfCnpj, DateTime dateBirth, string? profilePicture, string? personalDescription, Privacy privacy)
    {
        UserId = userId;
        Name = name;
        Email = email;
        Password = password;
        CpfCnpj = cpfCnpj;
        DateBirth = dateBirth;
        ProfilePicture = profilePicture;
        PersonalDescription = personalDescription;
        RegisteredAt = DateTime.Now;
        Privacy = privacy;
        LastLogin = null;
    }
    
    public User(string? name, string? email, string? password, string? cpfCnpj, DateTime dateBirth, string? profilePicture, string? personalDescription, Privacy privacy)
    {
        Name = name;
        Email = email;
        Password = password;
        CpfCnpj = cpfCnpj;
        DateBirth = dateBirth;
        ProfilePicture = profilePicture;
        PersonalDescription = personalDescription;
        RegisteredAt = DateTime.Now;
        Privacy = privacy;
        LastLogin = null;
    }

    public void RegisterLogin()
    {
        LastLogin = DateTime.Now;
    }
    
    public void ChangeName(string newName)
    {
        ArgumentException.ThrowIfNullOrEmpty(newName);
        
        Name = newName;
    }
    
    public void ChangeEmail(string newEmail)
    {
        ArgumentException.ThrowIfNullOrEmpty(newEmail);
        
        Email = newEmail;
    }
    
    public void ChangePassword(string oldPassword, string newPassword)
    {
        ArgumentException.ThrowIfNullOrEmpty(oldPassword);
        ArgumentException.ThrowIfNullOrEmpty(newPassword);
        
        if(!Password.Equals(oldPassword)) throw new ArgumentException("Old password don't match!");
        if(Password.Equals(newPassword)) throw new ArgumentException("New password must be different from old password!");
        
        Password = newPassword;
    }
}
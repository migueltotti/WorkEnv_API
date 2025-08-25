using System.Collections;
using System.Runtime.InteropServices.JavaScript;
using WorkEnv.Domain.Enum;

namespace WorkEnv.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
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
    public List<Collaborator> Collaborations { get; private set; } = [];
    
    // User 0..* - 0..* User -> Follow
    public List<Follow> Followers { get; private set; } = [];
    
    // User 0..* - 0..* User -> Follow
    public List<Follow> Following { get; private set; } = [];
    
    // User 1 - 0..* Event (Admin)
    public List<Event> AdminEvent { get; private set; } = [];
    
    // User 1 - 0..* EventParticipant
    public List<EventParticipant> EventsParticipant { get; private set; } = [];

    private User()
    {
    }

    public User(Guid id, string? name, string? email, string? password, string? cpfCnpj, DateTime dateBirth, string? profilePicture, string? personalDescription, Privacy privacy)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        CpfCnpj = cpfCnpj;
        DateBirth = dateBirth.ToUniversalTime();
        //DateBirth = DateTime.SpecifyKind(dateBirth, DateTimeKind.Utc);
        ProfilePicture = profilePicture;
        PersonalDescription = personalDescription;
        RegisteredAt = DateTime.UtcNow;
        //RegisteredAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        Privacy = privacy;
        LastLogin = null;
    }
    
    public User(string? name, string? email, string? password, string? cpfCnpj, DateTime dateBirth, string? profilePicture, string? personalDescription, Privacy privacy)
    {
        Name = name;
        Email = email;
        Password = password;
        CpfCnpj = cpfCnpj;
        DateBirth = dateBirth.ToUniversalTime();
        //DateBirth = DateTime.SpecifyKind(dateBirth, DateTimeKind.Utc);
        ProfilePicture = profilePicture;
        PersonalDescription = personalDescription;
        RegisteredAt = DateTime.UtcNow;
        //RegisteredAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        Privacy = privacy;
        LastLogin = null;
    }

    public void RegisterLogin()
    {
        LastLogin = DateTime.UtcNow;
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
    
    public void ChangePrivacy(Privacy newPrivacy)
    {
        Privacy = newPrivacy;
    }
    
    public void ChangeBirthDate(DateTime newBirthDate)
    {
        if(newBirthDate > DateTime.Today) throw new ArgumentException("Birth date cannot be in the future!");
        
        DateBirth = newBirthDate;
    }

    public void ChangePersonalDescription(string newPersonalDescription)
    {
        PersonalDescription = newPersonalDescription;
    }
}
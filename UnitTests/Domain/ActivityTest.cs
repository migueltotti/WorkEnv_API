using WorkEnv.Domain.Entities;
using WorkEnv.Domain.Enum;
using Task = WorkEnv.Domain.Entities.Task;
using TaskStatus = WorkEnv.Domain.Enum.TaskStatus;

namespace Test.Domain;

public class ActivityTest
{
    private Event eventTest;
    private Task taskTest;
    
    public ActivityTest()
    {
        var adminId = Guid.NewGuid();
        var workSpaceId = Guid.NewGuid();
        var name = "ActivityTest";
        var maxNumberOfParticipants = 100;
        var privacy = Privacy.Public;
        var activityStatus = TaskStatus.Created;
        var accessOptions = EventAccessOption.OpenToAll;
        
        eventTest = new Event(Guid.NewGuid(),
            workSpaceId,
            name,
            maxNumberOfParticipants,
            privacy,
            activityStatus,
            accessOptions,
            new DateTime(2025, 7, 15),
            adminId);
        taskTest = new Task(Guid.NewGuid(),
            workSpaceId,
            name,
            maxNumberOfParticipants,
            privacy,
            activityStatus,
            accessOptions,
            new DateTime(2025, 7, 15),
            new DateTime(2025, 7, 16),
            adminId);
    }

    /*[Fact]
    public void ChangeAdmin_Should_Throw_Exception_If_OldAdminId_Mismatch_ActivityAdminId()
    {
        // Arrange
        var adminId = Guid.NewGuid();
        var oldEventAdminId = Guid.NewGuid();
        var oldTaskAdminId = Guid.NewGuid();
        // Act
        // Assert
        Assert.Throws<AccessViolationException>(() => eventTest.ChangeAdmin(oldEventAdminId, adminId));
        Assert.Throws<AccessViolationException>(() => taskTest.ChangeAdmin(oldTaskAdminId, adminId));
    }
    
    [Fact]
    public void ChangeAdmin_Should_Change_AdminId_If_OldAdminId_Match_ActivityAdminId()
    {
        // Arrange
        var adminId = Guid.NewGuid();
        var oldEventAdminId = eventTest.AdminId!.Value;
        var oldTaskAdminId = taskTest.AdminId!.Value;
        
        // Act
        eventTest.ChangeAdmin(oldEventAdminId, adminId);
        taskTest.ChangeAdmin(oldTaskAdminId, adminId);
        
        // Assert
        Assert.Equal(eventTest.AdminId, adminId);
        Assert.Equal(taskTest.AdminId, adminId);
    }*/
    
    [Fact]
    public void ChangeName_Should_Throw_Exception_If_Name_Is_Null()
    {
        // Arrange
        var adminId = eventTest.AdminId.Value;
        string newName = null;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => eventTest.ChangeName(adminId, newName));
        Assert.Throws<ArgumentNullException>(() => taskTest.ChangeName(adminId, newName));
    }
    
    [Fact]
    public void ChangeName_Should_Throw_Exception_If_Name_Is_Empty()
    {
        // Arrange
        var adminId = eventTest.AdminId.Value;
        string newName = String.Empty;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => eventTest.ChangeName(adminId, newName));
        Assert.Throws<ArgumentNullException>(() => taskTest.ChangeName(adminId, newName));
    }
    
    [Fact]
    public void ChangeName_Should_ChangeName_If_Name_Is_Not_Null_Or_Empty()
    {
        // Arrange
        var adminId = eventTest.AdminId.Value;
        string newName = "New ActivityTest Name";
        
        // Act
        eventTest.ChangeName(adminId, newName);
        taskTest.ChangeName(adminId, newName);
        
        // Assert
        Assert.Equal(eventTest.Name, newName);
        Assert.Equal(taskTest.Name, newName);
    }

    [Fact]
    public void UpgradeMaxNumberOfParticipants_Should_Throw_Exception_If_NewMaxNumberOfParticipants_Is_Less_Or_Equal_Than_One()
    {
       // Arrange
       var newMaxNumberOfParticipants = 0;
       var adminId = eventTest.AdminId.Value;
       // Act
       // Assert
       Assert.Throws<ArgumentException>(() => eventTest.UpgradeMaxNumberOfParticipants(adminId, newMaxNumberOfParticipants));
       Assert.Throws<ArgumentException>(() => taskTest.UpgradeMaxNumberOfParticipants(adminId, newMaxNumberOfParticipants));
    }
    
    [Fact]
    public void UpgradeMaxNumberOfParticipants_Should_Change_MaxNumberOfParticipants_If_NewMaxNumberOfParticipants_Is_Greater_Than_One()
    {
        // Arrange
        var newMaxNumberOfParticipants = 2;
        var adminId = eventTest.AdminId.Value;
        
        // Act
        eventTest.UpgradeMaxNumberOfParticipants(adminId, newMaxNumberOfParticipants);
        taskTest.UpgradeMaxNumberOfParticipants(adminId, newMaxNumberOfParticipants);
        
        // Assert
        Assert.Equal(eventTest.MaxNumberOfParticipants, newMaxNumberOfParticipants);
        Assert.Equal(taskTest.MaxNumberOfParticipants, newMaxNumberOfParticipants);
    }

    [Fact]
    public void AddUser_Should_Throw_Exception_If_NewUser_Is_Null()
    {
        // Arrange
        UserActivity newUser = null;
        
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => eventTest.AddUser(newUser));
        Assert.Throws<ArgumentNullException>(() => taskTest.AddUser(newUser));
    }
    
    [Fact]
    public void AddUser_Should_Throw_Exception_If_NewUserActivityId_Mismatch_ActivityId()
    {
        // Arrange
        var newTaskUser = new UserActivity(Guid.NewGuid(), Guid.NewGuid());
        var newEventUser = new UserActivity(Guid.NewGuid(), Guid.NewGuid());
        
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => eventTest.AddUser(newEventUser));
        Assert.Throws<ArgumentException>(() => taskTest.AddUser(newTaskUser));
    }
    
    [Fact]
    public void AddUser_Should_Add_New_User_And_Increase_NumberOfParticipants()
    {
        // Arrange
        var newEventUser = new UserActivity(Guid.NewGuid(), eventTest.Id);
        var newTaskUser = new UserActivity(Guid.NewGuid(), taskTest.Id);
        
        // Act
        eventTest.AddUser(newEventUser);
        taskTest.AddUser(newTaskUser);
        
        // Assert
        Assert.Contains(newEventUser, eventTest.UserActivities);
        Assert.Contains(newTaskUser, taskTest.UserActivities);
        
        Assert.Equal(eventTest.NumberOfParticipants - 1, eventTest.UserActivities.Count);
        Assert.Equal(taskTest.NumberOfParticipants - 1, taskTest.UserActivities.Count);
    }
    
    [Fact]
    public void RemoveUser_Should_Throw_Exception_If_NewUser_Is_Null()
    {
        // Arrange
        UserActivity newUser = null;
        
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => eventTest.RemoveUser(newUser));
        Assert.Throws<ArgumentNullException>(() => taskTest.RemoveUser(newUser));
    }
    
    [Fact]
    public void RemoveUser_Should_Throw_Exception_If_NewUserActivityId_Mismatch_ActivityId()
    {
        // Arrange
        var newTaskUser = new UserActivity(Guid.NewGuid(), Guid.NewGuid());
        var newEventUser = new UserActivity(Guid.NewGuid(), Guid.NewGuid());
        
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => eventTest.RemoveUser(newEventUser));
        Assert.Throws<ArgumentNullException>(() => taskTest.RemoveUser(newTaskUser));
    }
    
    [Fact]
    public void RemoveUser_Should_Throw_Exception_If_UserActivity_Count_Is_Zero()
    {
        // Arrange
        var newEventUser = new UserActivity(Guid.NewGuid(), eventTest.Id);
        var newTaskUser = new UserActivity(Guid.NewGuid(), taskTest.Id);
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => eventTest.RemoveUser(newEventUser));
        Assert.Throws<ArgumentException>(() => taskTest.RemoveUser(newTaskUser));
    }
    
    [Fact]
    public void RemoveUser_Should_Remove_User_And_Decrease_NumberOfParticipants()
    {
        // Arrange
        var eventUser = new UserActivity(Guid.NewGuid(), eventTest.Id);
        var taskUser = new UserActivity(Guid.NewGuid(), taskTest.Id);
        
        // Act
        eventTest.AddUser(eventUser);
        taskTest.AddUser(taskUser);
        
        eventTest.RemoveUser(eventUser);
        taskTest.RemoveUser(taskUser);
        
        // Assert
        Assert.DoesNotContain(eventUser, eventTest.UserActivities);
        Assert.DoesNotContain(taskUser, taskTest.UserActivities);
        
        Assert.Empty(eventTest.UserActivities);
        Assert.Empty(taskTest.UserActivities);
    }
    
    [Fact]
    public void AddMessage_Should_Throw_Exception_If_NewMessage_Is_Null()
    {
        // Arrange
        Message newMessage = null;
        
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => eventTest.AddMessage(newMessage));
        Assert.Throws<ArgumentNullException>(() => taskTest.AddMessage(newMessage));
    }
    
    [Fact]
    public void AddMessage_Should_Throw_Exception_If_NewMessageActivityId_Mismatch_ActivityId()
    {
        // Arrange
        var newEventMessage = new Message(Guid.NewGuid(), Guid.NewGuid(), "Title", "Content", MessageType.Comment);
        var newTaskMessage = new Message(Guid.NewGuid(), Guid.NewGuid(), "Title", "Content", MessageType.Comment);
        
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => eventTest.AddMessage(newEventMessage));
        Assert.Throws<ArgumentException>(() => taskTest.AddMessage(newTaskMessage));
    }
    
    [Fact]
    public void AddMessage_Should_Add_New_Message_And_Increase_NumberOfParticipants()
    {
        // Arrange
        var newEventMessage = new Message(Guid.NewGuid(), eventTest.Id, "Title", "Content", MessageType.Comment);
        var newTaskMessage = new Message(Guid.NewGuid(), taskTest.Id, "Title", "Content", MessageType.Comment);
        
        // Act
        eventTest.AddMessage(newEventMessage);
        taskTest.AddMessage(newTaskMessage);
        
        // Assert
        Assert.Contains(newEventMessage, eventTest.Messages);
        Assert.Contains(newTaskMessage, taskTest.Messages);
        
        Assert.NotEmpty(eventTest.Messages);
        Assert.NotEmpty(taskTest.Messages);
    }
    
    [Fact]
    public void DeleteMessage_Should_Throw_Exception_If_NewMessage_Is_Null()
    {
        // Arrange
        Message newMessage = null;
        
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => eventTest.DeleteMessage(newMessage));
        Assert.Throws<ArgumentNullException>(() => taskTest.DeleteMessage(newMessage));
    }
    
    [Fact]
    public void DeleteMessage_Should_Throw_Exception_If_NewMessageActivityId_Mismatch_ActivityId()
    {
        // Arrange
        var newEventMessage = new Message(Guid.NewGuid(), eventTest.Id, "Title", "Content", MessageType.Comment);
        var newTaskMessage = new Message(Guid.NewGuid(), taskTest.Id, "Title", "Content", MessageType.Comment);

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => eventTest.DeleteMessage(newEventMessage));
        Assert.Throws<ArgumentException>(() => taskTest.DeleteMessage(newTaskMessage));
    }
    
    [Fact]
    public void DeleteMessage_Should_Throw_Exception_If_MessageActivity_Count_Is_Zero()
    {
        // Arrange
        var newEventMessage = new Message(Guid.NewGuid(), eventTest.Id, "Title", "Content", MessageType.Comment);
        var newTaskMessage = new Message(Guid.NewGuid(), taskTest.Id, "Title", "Content", MessageType.Comment);

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => eventTest.DeleteMessage(newEventMessage));
        Assert.Throws<ArgumentException>(() => taskTest.DeleteMessage(newTaskMessage));
    }
    
    [Fact]
    public void DeleteMessage_Should_Remove_Message_And_Decrease_NumberOfParticipants()
    {
        // Arrange
        var eventMessage = new Message(Guid.NewGuid(), eventTest.Id, "Title", "Content", MessageType.Comment);
        var taskMessage = new Message(Guid.NewGuid(), taskTest.Id, "Title", "Content", MessageType.Comment);
        
        // Act
        eventTest.AddMessage(eventMessage);
        taskTest.AddMessage(taskMessage);
        
        eventTest.DeleteMessage(eventMessage);
        taskTest.DeleteMessage(taskMessage);
        
        // Assert
        Assert.DoesNotContain(eventMessage, eventTest.Messages);
        Assert.DoesNotContain(taskMessage, taskTest.Messages);
        
        Assert.Empty(eventTest.Messages);
        Assert.Empty(taskTest.Messages);
    }
}
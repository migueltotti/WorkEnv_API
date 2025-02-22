using NSubstitute.ExceptionExtensions;
using WorkEnv.Domain.Entities;

namespace Test.Domain;

public class UserTest
{
    private User userTest;
    
    public UserTest()
    {
        userTest = new User(
            Guid.NewGuid(),
            "User",
            "user@test.com",
            "userpassword123",
            DateTime.Now.AddYears(-18)
            );
    }

    [Fact]
    public void ChangeName_Should_Return_Exception_If_Name_Is_Null()
    {
        // Arrange
        string nullName = null;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.ChangeName(nullName));
    }
    
    [Fact]
    public void ChangeName_Should_Return_Exception_If_Name_Is_Empty()
    {
        // Arrange
        string emptyName = string.Empty;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.ChangeName(emptyName));
    }
    
    [Fact]
    public void ChangeName_Should_Change_Name_If_Name_Is_Valid()
    {
        // Arrange
        string validName = "UserTest2";
        
        // Act
        userTest.ChangeName(validName);
        
        // Assert
        Assert.Equal(validName, userTest.Name);
    }
    
    [Fact]
    public void ChangeEmail_Should_Return_Exception_If_Email_Is_Null()
    {
        // Arrange
        string nullEmail = null;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.ChangeEmail(nullEmail));
    }
    
    [Fact]
    public void ChangeEmail_Should_Return_Exception_If_Email_Is_Empty()
    {
        // Arrange
        string emptyEmail = string.Empty;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.ChangeEmail(emptyEmail));
    }
    
    [Fact]
    public void ChangeEmail_Should_Change_Email_If_Email_Is_Valid()
    {
        // Arrange
        string validEmail = "usertest2@test.com";
        
        // Act
        userTest.ChangeEmail(validEmail);
        
        // Assert
        Assert.Equal(validEmail, userTest.Email);
    }
    
    [Fact]
    public void ChangePassword_Should_Return_Exception_If_Password_Is_Null()
    {
        // Arrange
        string nullPassword = null;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.ChangePassword(nullPassword));
    }
    
    [Fact]
    public void ChangePassword_Should_Return_Exception_If_Password_Is_Empty()
    {
        // Arrange
        string emptyPassword = string.Empty;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.ChangePassword(emptyPassword));
    }
    
    [Fact]
    public void ChangePassword_Should_Change_Password_If_Password_Is_Valid()
    {
        // Arrange
        string validPassword = "usertest321";
        
        // Act
        userTest.ChangePassword(validPassword);
        
        // Assert
        Assert.Equal(validPassword, userTest.Password);
    }
    
    [Fact]
    public void AddWorkSpace_Should_Return_Exception_If_WorkSpace_Is_Null()
    {
        // Arrange
        WorkSpace nullWorkSpace = null;
        
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.AddWorkSpace(nullWorkSpace));
    }
    
    [Fact]
    public void AddWorkSpace_Should_Return_Exception_If_WorkSpace_OwnerId_Mismatch()
    {
        // Arrange
        var workSpace = new WorkSpace(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "masterCode");
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.AddWorkSpace(workSpace));
    }
    
    [Fact]
    public void AddWorkSpace_Should_Add_WorkSpace_If_WorkSpace_Is_Valid()
    {
        // Arrange
        var workSpace = new WorkSpace(
            Guid.NewGuid(),
            userTest.UserId,
            "masterCode");
        
        // Act
        userTest.AddWorkSpace(workSpace);
        
        // Assert
        Assert.Contains(userTest.WorkSpaces, x => x == workSpace);
    }
    
    [Fact]
    public void AddUserToActivity_Should_Return_Exception_If_UserActivity_Is_Null()
    {
        // Arrange
        UserActivity nullUserActivity = null;
        
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.AddUserToActivity(nullUserActivity));
    }
    
    [Fact]
    public void AddUserToActivity_Should_Return_Exception_If_UserActivity_OwnerId_Mismatch()
    {
        // Arrange
        var userActivity = new UserActivity(
            Guid.NewGuid(),
            Guid.NewGuid());
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => userTest.AddUserToActivity(userActivity));
    }
    
    [Fact]
    public void AddUserToActivity_Should_Add_UserActivity_If_UserActivity_Is_Valid()
    {
        // Arrange
        var userActivity = new UserActivity(
            userTest.UserId,
            Guid.NewGuid());
        
        // Act
        userTest.AddUserToActivity(userActivity);
        
        // Assert
        Assert.Contains(userTest.UserActivities, x => x == userActivity);
    }
}
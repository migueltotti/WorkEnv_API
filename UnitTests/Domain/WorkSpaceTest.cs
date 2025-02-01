using NSubstitute.ExceptionExtensions;
using WorkEnv.Domain.Entities;

namespace Test.Domain;

public class WorkSpaceTest
{
    private WorkSpace wsTest;
    
    public WorkSpaceTest()
    {
        wsTest = new WorkSpace(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "MasterCode123"
            );
    }

    [Fact]
    public void ChangeOwner_Should_Return_Exception_If_OldOwnerId_Mismatch_WorkSpace_OwnerId()
    {
        // Arrange
        var oldOwnerId = Guid.NewGuid();
        var newOwnerId = Guid.NewGuid();
        
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => wsTest.ChangeOwner(oldOwnerId, newOwnerId));
    }
    
    [Fact]
    public void ChangeOwner_Should_Change_Owner_If_OldOwnerId_Is_Equal_WorkSpace_OwnerId()
    {
        // Arrange
        var oldOwnerId = wsTest.OwnerId;
        var newOwnerId = Guid.NewGuid();
        
        // Act
        wsTest.ChangeOwner(oldOwnerId, newOwnerId);
            
        // Assert
        Assert.Equal(wsTest.OwnerId, newOwnerId);
    }
    
    [Fact]
    public void DecreaseNumberOfActivities_Should_Throw_Exception_If_NumberOfActivities_Is_Zero()
    {
        // Arrange
        // Act
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => wsTest.DecreaseNumberOfActivities());
    }
    
    [Fact]
    public void GetMasterCode_Should_Return_Null_If_OwnerId_Mismatch_WorkSpace_OwnerId()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        
        // Act
        var masterCode = wsTest.GetMasterCode(ownerId);
        
        // Assert
        Assert.Equal(masterCode, null);
    }

    /*[Fact]
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
    }*/
}
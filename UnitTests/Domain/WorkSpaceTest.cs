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
}
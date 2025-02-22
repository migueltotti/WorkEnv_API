using WorkEnv.Domain.Entities;

namespace Test.Domain;

public class RoleTest
{
    private Role roleTest;

    public RoleTest()
    {
        roleTest = new Role(Guid.NewGuid(),
            "RoleTest", 
            "Simple Role Test"
        );
    }

    [Fact]
    public void ChangeName_Should_Throw_Exception_If_Name_Is_Null()
    {
        // Arrange
        string nullName = null;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => roleTest.ChangeName(nullName));
    }
    
    [Fact]
    public void ChangeName_Should_Throw_Exception_If_Name_Is_Empty()
    {
        // Arrange
        string emptyName = string.Empty;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => roleTest.ChangeName(emptyName));
    }
    
    [Fact]
    public void ChangeName_Should_Change_Name_If_Name_Is_Not_Empty_Or_Null()
    {
        // Arrange
        var newName = "New Name";
        // Act
        roleTest.ChangeName(newName);
        // Assert
        Assert.Equal(newName, roleTest.Name);
    }
    
    [Fact]
    public void ChangeDescription_Should_Throw_Exception_If_Description_Is_Null()
    {
        // Arrange
        string nullDescription = null;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => roleTest.ChangeDescription(nullDescription));
    }
    
    [Fact]
    public void ChangeDescription_Should_Throw_Exception_If_Description_Is_Empty()
    {
        // Arrange
        string emptyDescription = string.Empty;
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => roleTest.ChangeDescription(emptyDescription));
    }
    
    [Fact]
    public void ChangeDescription_Should_Change_Description_If_Description_Is_Not_Empty_Or_Null()
    {
        // Arrange
        var newDescription = "New Description";
        // Act
        roleTest.ChangeDescription(newDescription);
        // Assert
        Assert.Equal(newDescription, roleTest.Description);
    }
}
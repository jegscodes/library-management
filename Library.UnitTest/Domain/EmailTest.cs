using Library.Domain.Entities.Authors;

namespace Library.UnitTest.Domain;

/// <summary>
/// Entry point for email tests.
/// </summary>
public class EmailTest
{
    /// <summary>
    /// Entry point for email test. 
    /// </summary>
    public EmailTest() { }

    [Theory]
    [InlineData("jdoe@test.com")]
    [InlineData("janesmiht1@mailinator.com")]
    [InlineData("djones12@test.edu.org")]
    public void NewEmail_Valid_ShouldReturnSuccess(string email)
    {
        // Arrange & Act
        var newEmail = Email.Create(email);

        // Assert
        newEmail.Should().NotBeNull();
        newEmail.Value.Should().Contain("@");
    }

    [Theory]
    [InlineData("jdeo$!2@.com")]
    [InlineData("johndoe")]
    [InlineData("janesmith@")]
    [InlineData("1234567.com")]
    public void NewEmail_Invalid_ShouldThrowInvalidEmailException(string email)
    {
        // Arrange, Act & Assert
        Assert.Throws<InvalidEmailException>(() => Email.Create(email));
    }

    public void NewEmail_Empty_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => Email.Create(string.Empty));
    }
}

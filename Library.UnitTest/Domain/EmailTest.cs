using Library.Domain.Entities.Authors;

namespace Library.UnitTest.Domain;

/// <summary>
/// Contains unit tests for the <see cref="Email"/> class, validating the creation of email instances.
/// </summary>
public class EmailTest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTest"/> class.
    /// </summary>
    public EmailTest() { }

    /// <summary>
    /// Tests the creation of a valid email address.
    /// The method verifies that a valid email string creates a non-null <see cref="Email"/> object
    /// and that the email contains an "@" character.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
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

    /// <summary>
    /// Tests the creation of an email address with invalid formats.
    /// The method verifies that invalid email strings throw an <see cref="InvalidEmailException"/>.
    /// </summary>
    /// <param name="email">The invalid email address to test.</param>
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

    /// <summary>
    /// Tests the creation of an email address with an empty string.
    /// The method verifies that an empty string throws an <see cref="ArgumentNullException"/>.
    /// </summary>
    [Fact]
    public void NewEmail_Empty_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => Email.Create(string.Empty));
    }
}

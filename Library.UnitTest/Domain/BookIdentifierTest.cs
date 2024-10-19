namespace Library.UnitTest.Domain;

/// <summary>
/// Unit tests for the <see cref="BookIdentifier"/> class, following the Arrange-Act-Assert (AAA) pattern.
/// Each test is structured to clearly separate the setup of test data (Arrange), 
/// the execution of the code being tested (Act), and the validation of the results (Assert).
/// </summary>
public class BookIdentifierTest
{
    private static string InvalidISBNExceptionMessage => "Invalid ISBN format.";

    /// <summary>
    /// Initializes a new instance of the <see cref="BookIdentifierTest"/> class.
    /// </summary>
    public BookIdentifierTest() { }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with a valid ISBN does not throw an exception.
    /// </summary>
    [Fact]
    public void BookIdentifier_Valid_ShouldNotThrow()
    {
        var isbn = "978-0-306-40615-7";

        var bookIdentifier = BookIdentifier.Create(isbn);

        bookIdentifier.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with an empty string throws an <see cref="InvalidISBNException"/>.
    /// </summary>
    [Fact]
    public void BookIdentifier_Empty_ShouldThrowInvalidISBNException()
    {
        var isbn = string.Empty;

        AssertInvalidISBN(isbn);
    }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with an ISBN that does not start with 978 throws an <see cref="InvalidISBNException"/>.
    /// </summary>
    /// <param name="isbn">The ISBN to test.</param>
    [Theory]
    [InlineData("971")]
    [InlineData("973")]
    public void BookIdentifier_DoesNotStartWith978_ShouldThrowInvalidISBNException(string isbn)
    {
        AssertInvalidISBN(isbn);
    }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with an ISBN that does not start with 979 throws an <see cref="InvalidISBNException"/>.
    /// </summary>
    /// <param name="isbn">The ISBN to test.</param>
    [Theory]
    [InlineData("580")]
    [InlineData("181")]
    public void BookIdentifier_DoesNotStartWith979_ShouldThrowInvalidISBNException(string isbn)
    {
        AssertInvalidISBN(isbn);
    }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with an empty string throws an <see cref="InvalidISBNException"/> with the appropriate message.
    /// </summary>
    [Fact]
    public void BookIdentifier_Empty_ShouldThrowWithMessage()
    {
        var isbn = string.Empty;

        var exception = Assert.Throws<InvalidISBNException>(() => BookIdentifier.Create(isbn));

        exception.Message.Should().Be("ISBN cannot be empty.");
    }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with valid ISBN-13 sets the value correctly.
    /// </summary>
    /// <param name="isbn">The valid ISBN-13 to test.</param>
    [Theory]
    [InlineData("978-0-306-40615-7")]
    [InlineData("978-0-306-40613-3")]
    public void BookIdentifier_ValidISBN13_ShouldSetValueCorrectly(string isbn)
    {
        var bookIdentifier = BookIdentifier.Create(isbn);

        bookIdentifier.Value.Should().Be(isbn);
    }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with valid ISBN-10 sets the value correctly.
    /// </summary>
    /// <param name="isbn">The valid ISBN-10 to test.</param>
    [Theory]
    [InlineData("0-30640-615-2")]
    [InlineData("0-30640-613-6")]
    public void BookIdentifier_ValidISBN10_ShouldSetValueCorrectly(string isbn)
    {
        var bookIdentifier = BookIdentifier.Create(isbn);

        bookIdentifier.Value.Should().Be(isbn);
    }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with an invalid ISBN-13 throws an <see cref="InvalidISBNException"/> with the appropriate message.
    /// </summary>
    [Fact]
    public void BookIdentifier_InValidISBN13_ShouldThrowMessageContainsInvalid()
    {
        var isbn = "978-0-306-40615-8";

        var exception = Assert.Throws<InvalidISBNException>(() => BookIdentifier.Create(isbn));

        exception.Message.Should().Be(InvalidISBNExceptionMessage);
    }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with an invalid ISBN-10 throws an <see cref="InvalidISBNException"/> with the appropriate message.
    /// </summary>
    [Fact]
    public void BookIdentifier_InValidISBN10_ShouldThrowMessageContainsInvalid()
    {
        var isbn = "1-30640-615-2";

        var exception = Assert.Throws<InvalidISBNException>(() => BookIdentifier.Create(isbn));

        exception.Message.Should().Be(InvalidISBNExceptionMessage);
    }

    /// <summary>
    /// Tests that creating a <see cref="BookIdentifier"/> with an ISBN of invalid length throws an <see cref="InvalidISBNException"/> with the appropriate message.
    /// </summary>
    [Fact]
    public void BookIdentifier_InValidLength_ShouldThrowMessageContainsCharactersLength()
    {
        var isbn = "1-314-1-30640-413-2";

        var exception = Assert.Throws<InvalidISBNException>(() => BookIdentifier.Create(isbn));

        exception.Message.Should().Be("ISBN must be 10 or 13 characters long.");
    }

    /// <summary>
    /// Helper method to assert that creating a <see cref="BookIdentifier"/> with an invalid ISBN throws an <see cref="InvalidISBNException"/>.
    /// </summary>
    /// <param name="isbn">The ISBN to test.</param>
    private void AssertInvalidISBN(string isbn)
    {
        // Arrange & Act
        Action act = () => BookIdentifier.Create(isbn);

        // Assert
        act.Should().Throw<InvalidISBNException>();
    }
}

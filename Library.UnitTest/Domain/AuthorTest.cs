namespace Library.UnitTest.Domain;

/// <summary>
/// Contains unit tests for the <see cref="Author"/> class, validating author creation and behavior.
/// </summary>
public class AuthorTest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorTest"/> class.
    /// </summary>
    public AuthorTest() { }

    /// <summary>
    /// Tests the creation of a new author.
    /// This method verifies that a valid name and email address create a non-null <see cref="Author"/> object
    /// with the expected properties.
    /// </summary>
    /// <param name="name">The name of the author.</param>
    /// <param name="email">The email address of the author.</param>
    [Theory]
    [InlineData("John Doe", "jdoe@test.com")]
    [InlineData("Jane Smith", "jsmith@test.com")]
    public void NewAuthor_Should_ReturnAuthor(string name, string email)
    {
        // Arrange
        var newEmail = Email.Create(email);

        // Act
        var author = new Author(name, newEmail);

        // Assert
        author.Should().NotBeNull();
        author.Name.Should().Be(name);
        author.Email.Should().Be(newEmail);
    }

    /// <summary>
    /// Tests the creation of an author with an invalid name.
    /// This method verifies that providing an empty name throws an <see cref="ArgumentNullException"/>.
    /// </summary>
    [Fact]
    public void NewAuthor_InvalidName_ShouldThrowArgumentNullExceptionError()
    {
        // Arrange
        string name = string.Empty;
        var email = Email.Create("sampleemail@test.com");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Author(name, email));
    }

    /// <summary>
    /// Tests that a newly created author has an empty book collection.
    /// This method verifies that when an author is instantiated, their initial list of books is empty.
    /// </summary>
    [Fact]
    public void NewAuthor_Valid_ShouldReturnEmptyBooks()
    {
        // Arrange & Act
        var author = TestData.CreateDefaultAuthor();

        // Assert
        author.Books.Should().BeEmpty();
    }

    /// <summary>
    /// Tests the functionality of adding a book to an author.
    /// This method verifies that after adding a book to the author's collection,
    /// the book should be present in the author's list of books.
    /// </summary>
    [Fact]
    public void Author_AddBook_ShouldContainBook()
    {
        // Arrange
        var author = TestData.CreateDefaultAuthor();
        var book = TestData.CreateDefaultBook();

        // Act
        author.AddBook(book);

        // Assert
        author.Books.Should().Contain(book);
    }
}

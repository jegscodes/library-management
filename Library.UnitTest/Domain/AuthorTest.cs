namespace Library.UnitTest.Domain;

/// <summary>
/// Entry point for author test.
/// </summary>
public class AuthorTest
{
    /// <summary>
    /// Constructor of Author test.
    /// </summary>
    public AuthorTest() { }

    [Theory]
    [InlineData("John Doe", "jdoe@test.com")]
    [InlineData("Jane Smith", "jsmith@test.com")]
    /// <summary>
    /// Test method for new author.
    /// </summary>
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
    /// Test method for invalid name, if invalid then throw null.
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
    /// Test method for new author, if author is new then book should be empty.
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
    /// Test method for new author with new book. 
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

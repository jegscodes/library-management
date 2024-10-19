namespace Library.UnitTest.Domain;

/// <summary>
/// Unit tests for the <see cref="Book"/> class, following the Arrange-Act-Assert (AAA) pattern.
/// Each test is structured to clearly separate the setup of test data (Arrange), 
/// the execution of the code being tested (Act), and the validation of the results (Assert).
/// </summary>
public class BookTest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookTest"/> class.
    /// </summary>
    public BookTest() { }

    #region Books
    /// <summary>
    /// Tests the creation of a <see cref="Book"/> instance with different input data.
    /// </summary>
    /// <param name="authorId">The author ID.</param>
    /// <param name="title">The title of the book.</param>
    [Theory]
    [InlineData(1, "Sample Book 1", "1874-09-01", "978-0-306-40615-7")]
    [InlineData(2, "Sample Book 2", "2022-11-08", "978-1-4028-9462-6")]
    [InlineData(3, "Sample Book 3", "2004-04-02", "978-3-16-148410-0")]
    public void NewBook_ValidParams_IsValidOrSuccess(int authorId, string title, string date, string isbnThirteen)
    {
        // Arrange & Act
        var validDate = DateTime.Parse(date);
        var book = TestData.CreateBook(authorId, title, isbnThirteen, validDate);

        // Assert
        book.Should().NotBeNull();
        book.Title.Should().Be(title);
        book.AuthorId.Should().Be(authorId);
        book.ISBN.Value.Should().Be(isbnThirteen);
        book.PublishedDate.Value.Should().Be(validDate);
    }

    /// <summary>
    /// Tests that creating a new book with an empty title throws a <see cref="PublishedDateException"/>.
    /// </summary>
    [Fact]
    public void NewBook_InvalidTitle_ThrowsArgumentNullException()
    {
        // Arrange
        var emptyTitle = string.Empty;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => TestData.CreateBook(TestData.AuthorId,
                                                                        emptyTitle,
                                                                        TestData.DefaultIsbn,
                                                                        TestData.DefaultPublishedDate));
    }

    /// <summary>
    /// Tests that creating a new book with an invalid ISBN format throws a <see cref="PublishedDateException"/>.
    /// </summary>
    [Fact]
    public void NewBook_InvalidISBN_ThrowsInvalidISBNException()
    {
        // Arrange
        var invalidISBN = "940-1-86197-876-9";

        // Act & Assert
        Assert.Throws<InvalidISBNException>(() => TestData.CreateBook(TestData.AuthorId,
                                                                        TestData.DefaultTitle,
                                                                        invalidISBN,
                                                                        TestData.DefaultPublishedDate));
    }

    /// <summary>
    /// Tests that creating a new book with a future published date throws a <see cref="PublishedDateException"/>.
    /// </summary>
    [Fact]
    public void NewBook_FuturePublishedDate_ThrowsPublishDateException()
    {
        // Arrange
        var publishedDate = DateTime.Now.AddDays(2);

        // Act & Assert
        Assert.Throws<PublishedDateException>(() => TestData.CreateBook(TestData.AuthorId,
                                                                        TestData.DefaultTitle,
                                                                        TestData.DefaultIsbn,
                                                                        publishedDate));
    }
    #endregion

    #region Book ISBN
    /// <summary>
    /// Tests that the ISBN property of a newly created book is of type <see cref="BookIdentifier"/>.
    /// </summary>
    [Fact]
    public void Book_SetISBN_ShouldBeTypeOf_BookIdentifier()
    {
        // Arrange & Act
        var book = TestData.CreateDefaultBook();

        // Assert
        book.ISBN.Should().BeOfType<BookIdentifier>();
    }

    /// <summary>
    /// Tests that the ISBN value of a newly created book matches the expected default ISBN.
    /// </summary>
    [Fact]
    public void NewBook_SetISBN_IsValidOrSuccess()
    {
        // Arrange & Act
        var book = TestData.CreateDefaultBook();

        // Assert
        book.ISBN.Should().NotBeNull();
        book.ISBN.Value.Should().Be(TestData.DefaultIsbn);
    }
    #endregion

    #region Book PublishedDate
    /// <summary>
    /// Tests that the PublishedDate property of a newly created book is of type <see cref="PublishedDate"/>.
    /// </summary>
    [Fact]
    public void Book_SetPublishedDate_ShouldBeTypeOf_PublishedDate()
    {
        // Arrange & Act
        var book = TestData.CreateDefaultBook();

        // Assert
        book.PublishedDate.Should().BeOfType<PublishedDate>();
    }

    /// <summary>
    /// Tests that the PublishedDate value of a newly created book matches the expected default published date (MM/dd/yyyy).
    /// </summary>
    [Fact]
    public void NewBook_SetPublishedDate_IsValidOrSuccess()
    {
        // Arrange & Act
        var book = TestData.CreateDefaultBook();

        // Assert
        book.PublishedDate.Should().BeOfType<PublishedDate>();
        book.PublishedDate.Value.Should().Be(TestData.DefaultPublishedDate);
    }
    #endregion
}

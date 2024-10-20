using Library.Application.Books.Queries.GetBook;

namespace Library.UnitTest.Application.QueryHandlers;

/// <summary>
/// Contains unit tests for the <see cref="GetBookQueryHandler"/> class,
/// which handles the retrieval of book queries.
/// </summary>
public class GetBookQueryHandlerTest : BaseQueryHandlerTest
{
    private readonly GetBookQueryHandler _getBookQueryHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBookQueryHandlerTest"/> class.
    /// </summary>
    public GetBookQueryHandlerTest()
    {
        _getBookQueryHandler = new GetBookQueryHandler(_mockBookRepository.Object);
    }

    /// <summary>
    /// Tests that the failure result contains the correct error message when
    /// attempting to retrieve a book that does not exist.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task GetBook_NotFoundBook_ShouldHaveCorrectMessage()
    {
        // Arrange
        var bookId = 1;
        var query = new GetBookQuery(bookId);
        SetUpSingleBookMock(bookId, null);

        // Act
        var result = await _getBookQueryHandler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be($"Book with ID {bookId} could not be found.");
    }

    /// <summary>
    /// Tests that the handler returns a failure when attempting to retrieve a book
    /// with a non-existent book ID.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task GetBook_NotFoundBook_ShouldReturnFailure()
    {
        // Arrange
        var bookId = 1;
        var query = new GetBookQuery(bookId);
        SetUpSingleBookMock(bookId, null);

        // Act
        var result = await _getBookQueryHandler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }

    /// <summary>
    /// Tests that the handler successfully retrieves a book when a valid book ID is provided.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task GetBook_ValidBook_ShouldReturnSuccess()
    {
        // Arrange
        var bookId = 1;
        var query = new GetBookQuery(bookId);
        SetUpSingleBookMock(bookId, TestData.CreateDefaultBook());

        // Act
        var result = await _getBookQueryHandler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }
}

namespace Library.UnitTest.Application.QueryHandlers;

/// <summary>
/// Contains unit tests for the <see cref="GetAuthorQueryHandler"/> class,
/// which handles the retrieval of author queries.
/// </summary>
public class GetAuthorQueryHandlerTest : BaseQueryHandlerTest
{
    private readonly GetAuthorQueryHandler _getAuthorQueryHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAuthorQueryHandlerTest"/> class.
    /// </summary>
    public GetAuthorQueryHandlerTest()
    {
        _getAuthorQueryHandler = new GetAuthorQueryHandler(_mockAuthorRepository.Object);
    }

    /// <summary>
    /// Tests that the failure result contains the correct error message when
    /// attempting to retrieve an author that does not exist.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task GetBook_NotFoundBook_ShouldHaveCorrectMessage()
    {
        // Arrange
        var authorId = 1;
        var query = new GetAuthorQuery(authorId);
        SetUpSingleAuthorMock(authorId, null);

        // Act
        var result = await _getAuthorQueryHandler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be($"Author with ID {authorId} couldn't be found");
    }

    /// <summary>
    /// Tests that the handler returns a failure when attempting to retrieve an author
    /// with a non-existent author ID.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task GetBook_NotFoundBook_ShouldReturnFailure()
    {
        // Arrange
        var authorId = 1;
        var query = new GetAuthorQuery(authorId);
        SetUpSingleAuthorMock(authorId, null);

        // Act
        var result = await _getAuthorQueryHandler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }

    /// <summary>
    /// Tests that the handler successfully retrieves an author when a valid author ID is provided.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task GetBook_ValidBook_ShouldReturnSuccess()
    {
        // Arrange
        var authorId = 1;
        var query = new GetAuthorQuery(authorId);
        SetUpSingleAuthorMock(authorId, TestData.CreateDefaultAuthor());

        // Act
        var result = await _getAuthorQueryHandler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }
}

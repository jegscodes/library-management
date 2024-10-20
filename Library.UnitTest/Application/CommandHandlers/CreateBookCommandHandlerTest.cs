namespace Library.UnitTest.Application.CommandHandlers;

/// <summary>
/// Contains unit tests for the <see cref="CreateBookCommandHandler"/> class,
/// which handles the creation of book commands.
/// </summary>
public class CreateBookCommandHandlerTest : BaseCommandHandlerTest
{
    private readonly CreateBookCommandHandler _createBookCommandHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookCommandHandlerTest"/> class.
    /// </summary>
    public CreateBookCommandHandlerTest()
    {
        _createBookCommandHandler = new CreateBookCommandHandler(_mockAuthorRepository.Object);
    }

    /// <summary>
    /// Tests that the handler returns a failure when attempting to create a book
    /// with an author that does not exist.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCommandHandler_NotFoundAuthor_ShouldReturnFailure()
    {
        // Arrange
        var bookRequest = TestData.CreateDefaultBook();
        var command = new CreateBookCommand(bookRequest.AuthorId, bookRequest.Title, bookRequest.PublishedDate.Value, bookRequest.ISBN.Value);

        SetUpAuthorMocks(bookRequest.AuthorId, null, 1);

        // Act
        var result = await _createBookCommandHandler.Handle(command, default);

        // Assert
        _mockAuthorRepository.Verify(uow => uow.UnitOfWork.SaveChangesAsync(default), Times.Never);
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    /// <summary>
    /// Tests that the handler successfully creates a book when a valid command is provided
    /// and the author exists.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCommandHandler_ValidCommand_ShouldReturnSuccess()
    {
        // Arrange
        var author = TestData.CreateDefaultAuthor();
        var bookRequest = TestData.CreateDefaultBook();
        var command = new CreateBookCommand(bookRequest.AuthorId, bookRequest.Title, bookRequest.PublishedDate.Value, bookRequest.ISBN.Value);

        SetUpAuthorMocks(bookRequest.AuthorId, author, 1);

        // Act
        var result = await _createBookCommandHandler.Handle(command, default);

        // Assert
        _mockAuthorRepository.Verify(uow => uow.UnitOfWork.SaveChangesAsync(default), Times.Once);
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
    }
}

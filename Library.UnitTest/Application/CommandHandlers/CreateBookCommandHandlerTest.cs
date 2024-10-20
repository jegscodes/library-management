namespace Library.UnitTest.Application.CommandHandlers;

public class CreateBookCommandHandlerTest : BaseCommandHandlerTest
{

    private CreateBookCommandHandler _createBookCommandHandler;
    public CreateBookCommandHandlerTest()
    {
        _createBookCommandHandler = new CreateBookCommandHandler(_mockAuthorRepository.Object);
    }

    [Fact]
    public async Task CreateCommandHandler_ValidCommand_ShouldReturnSuccess()
    {
        var author = TestData.CreateDefaultAuthor();
        var bookRequest = TestData.CreateDefaultBook();
        var command = new CreateBookCommand(bookRequest.AuthorId, bookRequest.Title, bookRequest.PublishedDate.Value, bookRequest.ISBN.Value);

        SetUpAuthorMocks(bookRequest.AuthorId, author, 1);

        var result = await _createBookCommandHandler.Handle(command, default);

        _mockAuthorRepository.Verify(uow => uow.UnitOfWork.SaveChangesAsync(default), Times.Once);

        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task CreateCommandHandler_NotFoundAuthor_ShouldReturnFailure()
    {
        var bookRequest = TestData.CreateDefaultBook();
        var command = new CreateBookCommand(bookRequest.AuthorId, bookRequest.Title, bookRequest.PublishedDate.Value, bookRequest.ISBN.Value);

        SetUpAuthorMocks(bookRequest.AuthorId, null, 1);

        var result = await _createBookCommandHandler.Handle(command, default);

        _mockAuthorRepository.Verify(uow => uow.UnitOfWork.SaveChangesAsync(default), Times.Never);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }
}

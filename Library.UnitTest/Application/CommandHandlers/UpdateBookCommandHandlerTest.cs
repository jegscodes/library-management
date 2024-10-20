namespace Library.UnitTest.Application.CommandHandlers;

public class UpdateBookCommandHandlerTest : BaseCommandHandlerTest
{
    private UpdateBookCommandHandler _updateBookCommandHandler;
    public UpdateBookCommandHandlerTest()
    {
        _updateBookCommandHandler = new UpdateBookCommandHandler(_mockBookRepository.Object, _mockAuthorRepository.Object);
    }

    [Fact]
    public async Task UpdateCommandHandler_ValidCommand_AuthorUnchange_ShouldReturnSuccess()
    {
        var book = TestData.CreateDefaultBook();
        var updateCommand = new UpdateBookCommand(1, book.AuthorId, book.Title, book.ISBN.Value, book.PublishedDate.Value);
        SetUpBookMocks(1, book, 1);

        var result = await _updateBookCommandHandler.Handle(updateCommand, default);

        _mockAuthorRepository.Verify(author => author.GetByIdAsync(book.AuthorId), Times.Never);
        _mockBookRepository.Verify(uow => uow.UnitOfWork.SaveChangesAsync(default), Times.Once);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCommandHandler_ValidCommand_AuthorChanged_ShouldReturnSuccess()
    {
        var book = TestData.CreateDefaultBook();
        var author = TestData.CreateDefaultAuthor();

        var authorId = 2;
        var bookId = 2;

        var updateCommand = new UpdateBookCommand(2, authorId, book.Title, book.ISBN.Value, book.PublishedDate.Value);

        SetUpAuthorBookMocks(authorId, bookId, author, book, 1, 1);

        var result = await _updateBookCommandHandler.Handle(updateCommand, default);

        _mockAuthorRepository.Verify(author => author.GetByIdAsync(authorId), Times.Once);  
        _mockBookRepository.Verify(uow => uow.UnitOfWork.SaveChangesAsync(default), Times.Once);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCommandHandler_NotFoundBook_ShouldReturnFailure()
    {
        var book = TestData.CreateDefaultBook();
        var updateCommand = new UpdateBookCommand(1, book.AuthorId, book.Title, book.ISBN.Value, book.PublishedDate.Value);

        SetUpBookMocks(1, null, 1);

        var result = await _updateBookCommandHandler.Handle(updateCommand, default);

        _mockAuthorRepository.Verify(author => author.GetByIdAsync(book.AuthorId), Times.Never);
        _mockBookRepository.Verify(uow => uow.UnitOfWork.SaveChangesAsync(default), Times.Never);

        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("not be found.");
    }

    [Fact]
    public async Task UpdateCommandHandler_NotFoundAuthor_AuthorChanged_ShouldReturnFailure()
    {
        var book = TestData.CreateDefaultBook();
        var author = TestData.CreateDefaultAuthor();

        var authorId = 2;
        var bookId = 2;

        var updateCommand = new UpdateBookCommand(2, authorId, book.Title, book.ISBN.Value, book.PublishedDate.Value);

        SetUpAuthorBookMocks(authorId, bookId, null, book, 1, 1);

        var result = await _updateBookCommandHandler.Handle(updateCommand, default);

        _mockAuthorRepository.Verify(author => author.GetByIdAsync(authorId), Times.Once);
        _mockBookRepository.Verify(uow => uow.UnitOfWork.SaveChangesAsync(default), Times.Never);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();

        result.Error.Should().Contain("not be found.");
    }
}

using Library.Application.Books.Queries.GetBook;

namespace Library.UnitTest.Application.QueryHandlers;

public class GetBookQueryHandlerTest : BaseQueryHandlerTest
{
    private GetBookQueryHandler _getBookQueryHandler;
    public GetBookQueryHandlerTest()
    {
        _getBookQueryHandler = new GetBookQueryHandler(_mockBookRepository.Object);
    }

    [Fact]
    public async Task GetBook_ValidBook_ShouldReturnSuccess()
    {
        var bookId = 1;

        var query = new GetBookQuery(bookId);

        SetUpSingleBookMock(bookId, TestData.CreateDefaultBook());

        var result = await _getBookQueryHandler.Handle(query, default);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task GetBook_NotFoundBook_ShouldReturnFailure()
    {
        var bookId = 1;

        var query = new GetBookQuery(bookId);

        SetUpSingleBookMock(bookId, null);

        var result = await _getBookQueryHandler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task GetBook_NotFoundBook_ShouldHaveCorrectMessage()
    {
        var bookId = 1;

        var query = new GetBookQuery(bookId);

        SetUpSingleBookMock(bookId, null);

        var result = await _getBookQueryHandler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();

        result.Error.Should().Be($"Book with ID {bookId} could not be found.");
    }
}

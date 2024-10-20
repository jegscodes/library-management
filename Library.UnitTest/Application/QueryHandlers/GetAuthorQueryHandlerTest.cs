using Library.Application.Books.Queries.GetBook;

namespace Library.UnitTest.Application.QueryHandlers;

public class GetAuthorQueryHandlerTest : BaseQueryHandlerTest
{
    private GetAuthorQueryHandler _getAuthorQueryHandler;
    public GetAuthorQueryHandlerTest()
    {
        _getAuthorQueryHandler = new GetAuthorQueryHandler(_mockAuthorRepository.Object);
    }


    [Fact]
    public async Task GetBook_ValidBook_ShouldReturnSuccess()
    {
        var auhtorId = 1;

        var query = new GetAuthorQuery(auhtorId);

        SetUpSingleAuthorMock(auhtorId, TestData.CreateDefaultAuthor());

        var result = await _getAuthorQueryHandler.Handle(query, default);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task GetBook_NotFoundBook_ShouldReturnFailure()
    {
        var auhtorId = 1;

        var query = new GetAuthorQuery(auhtorId);

        SetUpSingleAuthorMock(auhtorId, null);

        var result = await _getAuthorQueryHandler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task GetBook_NotFoundBook_ShouldHaveCorrectMessage()
    {
        var auhtorId = 1;

        var query = new GetAuthorQuery(auhtorId);

        SetUpSingleAuthorMock(auhtorId, null);

        var result = await _getAuthorQueryHandler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();

        result.Error.Should().Be($"Author with ID {auhtorId} couldn't be found");
    }
}

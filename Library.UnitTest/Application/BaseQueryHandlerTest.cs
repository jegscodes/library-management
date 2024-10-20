namespace Library.UnitTest.Application;

public class BaseQueryHandlerTest
{
    protected readonly Mock<IAuthorRepository> _mockAuthorRepository;
    protected readonly Mock<IBookRepository> _mockBookRepository;

    public BaseQueryHandlerTest()
    {
        _mockAuthorRepository = new();
        _mockBookRepository = new();
    }

    protected void SetUpSingleBookMock(int id, Book? book)
    {
        _mockBookRepository.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(book);
    }

    protected void SetUpMultipleBooksMock(int pageNumber, int pageSize, PaginatedList<Book>? books)
    {
        _mockBookRepository.Setup(s => s.GetPaginatedListAsync(pageNumber, pageSize)).ReturnsAsync(books);
    }

    protected void SetUpSingleAuthorMock(int id, Author? author)
    {
        _mockAuthorRepository.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(author);
    }

    protected void SetUpMultipleAuthorsMock(int pageNumber, int pageSize, PaginatedList<Author>? authors)
    {
        _mockAuthorRepository.Setup(s => s.GetPaginatedListAsync(pageNumber, pageSize)).ReturnsAsync(authors);
    }

}

namespace Library.UnitTest.Application;
public class BaseCommandHandlerTest
{
    protected readonly Mock<IAuthorRepository> _mockAuthorRepository;
    protected readonly Mock<IBookRepository> _mockBookRepository;
    protected readonly Mock<IUnitOfWork> _mockUnitOfWork;
    protected BaseCommandHandlerTest()
    {
        _mockAuthorRepository = new();
        _mockBookRepository = new();
        _mockUnitOfWork = new();
    }

    protected void SetUpAuthorMocks(int authorId, Author? author, int saveResult)
    {
        _mockAuthorRepository.Setup(author => author.GetByIdAsync(authorId)).ReturnsAsync(author);
        _mockAuthorRepository.Setup(s => s.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(saveResult);
    }

    protected void SetUpBookMocks(int bookId, Book? book, int saveResult)
    {
        _mockBookRepository.Setup(book => book.GetByIdAsync(bookId)).ReturnsAsync(book);
        _mockBookRepository.Setup(s => s.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(saveResult);
    }

    protected void SetUpAuthorBookMocks(int authorid, int bookId, Author? author, Book? book, int authorSaveResult, int bookSaveResult)
    {
        _mockAuthorRepository.Setup(author => author.GetByIdAsync(authorid)).ReturnsAsync(author);
        _mockBookRepository.Setup(book => book.GetByIdAsync(bookId)).ReturnsAsync(book);
        _mockAuthorRepository.Setup(s => s.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(authorSaveResult);
        _mockBookRepository.Setup(s => s.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(bookSaveResult);
    }
}

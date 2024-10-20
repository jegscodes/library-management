namespace Library.Application.Books.Queries.GetBooks;

/// <summary>
/// Represents a request to retrieve a paginated list of books.
/// </summary>
public class GetBooksQuery : IRequest<IPaginated<Book>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBooksQuery"/> class.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve. Default is 1.</param>
    /// <param name="pageSize">The number of books to return per page. Default is 50.</param>
    public GetBooksQuery(int pageNumber = 1, int pageSize = 50)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    /// <summary>
    /// Gets the page number to retrieve.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Gets the number of books to return per page.
    /// </summary>
    public int PageSize { get; }
}

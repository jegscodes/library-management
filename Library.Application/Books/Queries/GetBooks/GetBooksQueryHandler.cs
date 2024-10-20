namespace Library.Application.Books.Queries.GetBooks;

/// <summary>
/// Handles the retrieval of paginated lists of books based on the specified query.
/// </summary>
public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IPaginated<Book>>
{
    private readonly IBookRepository _bookRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBooksQueryHandler"/> class.
    /// </summary>
    /// <param name="bookRepository">The repository to access book data.</param>
    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <summary>
    /// Handles the <see cref="GetBooksQuery"/> request to retrieve a paginated list of books.
    /// </summary>
    /// <param name="request">The request containing pagination information.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of books.</returns>
    public async Task<IPaginated<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetPaginatedListAsync(request.PageNumber, request.PageSize);

        if (books is null)
        {
            return new PaginatedList<Book>(new List<Book>(), 0, request.PageNumber, request.PageSize);
        }

        return books;
    }
}

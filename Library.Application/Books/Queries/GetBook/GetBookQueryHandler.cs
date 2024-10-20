namespace Library.Application.Books.Queries.GetBook;

/// <summary>
/// Handles the retrieval of a specific book by its identifier.
/// </summary>
public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Result<Book>>
{
    private readonly IBookRepository _bookRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBookQueryHandler"/> class.
    /// </summary>
    /// <param name="bookRepository">The repository used to access book data.</param>
    public GetBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <summary>
    /// Handles the execution of the <see cref="GetBookQuery"/> request.
    /// </summary>
    /// <param name="request">The query request containing the book ID.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result{T}"/> containing the requested book, or an error message if not found.</returns>
    public async Task<Result<Book>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);

        if (book is null)
        {
            return Result.Fail<Book>($"Book with ID {request.Id} could not be found.");
        }

        return Result.Ok(book);
    }
}

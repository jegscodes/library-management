namespace Library.Application.Books.Queries.GetBook;

/// <summary>
/// Represents a request to retrieve a specific book by its identifier.
/// </summary>
public class GetBookQuery : IRequest<Result<Book>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBookQuery"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the book.</param>
    public GetBookQuery(int id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the unique identifier of the book to retrieve.
    /// </summary>
    public int Id { get; }
}

namespace Library.Application.Books.Commands;

/// <summary>
/// Handles the creation of a new book by processing <see cref="CreateBookCommand"/> requests.
/// </summary>
public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<EntityCreatedResponse>>
{
    private readonly IAuthorRepository _authorRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookCommandHandler"/> class.
    /// </summary>
    /// <param name="authorRepository">The author repository used for author operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="authorRepository"/> is null.</exception>
    public CreateBookCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }

    /// <summary>
    /// Handles the creation of a book based on the provided command.
    /// </summary>
    /// <param name="request">The command containing book creation details.</param>
    /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{EntityCreatedResponse}"/> indicating success or failure.</returns>
    public async Task<Result<EntityCreatedResponse>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the author by the provided Author ID
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        // Check if the author exists; if not, return a failure result
        if (author is null)
        {
            return Result.Fail<EntityCreatedResponse>($"Author with ID {request.AuthorId} could not be found.");
        }

        // Create value objects for ISBN and Published Date
        var isbn = BookIdentifier.Create(request.ISBN);
        var publishedDate = PublishedDate.Create(request.PublishedDate);

        // Create a new book entity
        var book = new Book(request.AuthorId, request.Title, publishedDate, isbn);

        // Add the new book to the author's collection
        author.AddBook(book);

        // Save changes to the database
        await _authorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        // Return a success result with the created book's ID
        return Result.Ok<EntityCreatedResponse>(book.Id);
    }
}

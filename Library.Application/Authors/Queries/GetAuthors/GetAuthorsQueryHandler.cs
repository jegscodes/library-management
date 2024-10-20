namespace Library.Application.Authors.Queries.GetAuthors;

/// <summary>
/// Handles the retrieval of a paginated list of authors.
/// </summary>
public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IPaginated<Author>>
{
    private readonly IAuthorRepository _authorRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAuthorsQueryHandler"/> class.
    /// </summary>
    /// <param name="authorRepository">The repository used to access author data.</param>
    public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    /// <summary>
    /// Handles the given <see cref="GetAuthorsQuery"/> and retrieves a paginated list of authors.
    /// </summary>
    /// <param name="request">The query containing pagination parameters.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A task that represents the asynchronous operation, with a <see cref="IPaginated{Author}"/> as its result.</returns>
    public async Task<IPaginated<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetPaginatedListAsync(request.PageNumber, request.PageSize);

        if (authors is null)
        {
            return new PaginatedList<Author>(new List<Author>(), 0, request.PageNumber, request.PageSize);
        }

        return authors;
    }
}

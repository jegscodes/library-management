namespace Library.Application.Authors.Queries.GetAuthor;

/// <summary>
/// Handles the retrieval of an author by their ID.
/// </summary>
public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, Result<Author>>
{
    private readonly IAuthorRepository _authorRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAuthorQueryHandler"/> class.
    /// </summary>
    /// <param name="authorRepository">The repository used to access author data.</param>
    public GetAuthorQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    /// <summary>
    /// Handles the given <see cref="GetAuthorQuery"/> and retrieves the corresponding author.
    /// </summary>
    /// <param name="request">The query containing the author's ID.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Result{Author}"/> indicating the success or failure of the operation.</returns>
    public async Task<Result<Author>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.Id);

        if (author == null)
        {
            return Result.Fail<Author>($"Author with ID {request.Id} couldn't be found");
        }

        return Result.Ok(author);
    }
}

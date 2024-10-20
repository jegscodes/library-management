namespace Library.Application.Authors.Commands.CreateAuthor;

/// <summary>
/// Handles the creation of a new author by processing <see cref="CreateAuthorCommand"/> requests.
/// </summary>
public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<EntityCreatedResponse>>
{
    private readonly IAuthorRepository _authorRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAuthorCommandHandler"/> class.
    /// </summary>
    /// <param name="authorRepository">The author repository used for author operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="authorRepository"/> is null.</exception>
    public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }

    /// <summary>
    /// Handles the creation of an author based on the provided command.
    /// </summary>
    /// <param name="request">The command containing author creation details.</param>
    /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{EntityCreatedResponse}"/> indicating success or failure.</returns>
    public async Task<Result<EntityCreatedResponse>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        // Create an Email value object from the request email
        var email = Email.Create(request.Email);

        // Check if the author with the specified email already exists
        var authorExist = await _authorRepository.CheckEmailIfExist(email);

        if (authorExist)
        {
            // Return a failure result if the author already exists
            return Result.Fail<EntityCreatedResponse>($"Author with email, {request.Email} already exist.");
        }

        // Create a new author entity
        var author = new Author(request.Name, email);

        // Add the new author to the repository
        _authorRepository.Add(author);

        // Save changes to the database
        await _authorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        // Return a success result with the created author's ID
        return Result.Ok<EntityCreatedResponse>(author.Id);
    }
}

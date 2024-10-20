using Library.Application.Common.Extensions;

namespace Library.Infrastructure.Repositories;

/// <summary>
/// Repository class for managing author entities in the database.
/// </summary>
public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for repository operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context"/> is null.</exception>
    public AuthorRepository(LibraryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Gets the unit of work associated with the repository.
    /// </summary>
    public IUnitOfWork UnitOfWork => _context;

    /// <summary>
    /// Adds a new author to the database.
    /// </summary>
    /// <param name="author">The author entity to add.</param>
    /// <returns>The added <see cref="Author"/> entity.</returns>
    public Author Add(Author author)
    {
        return _context.Add(author).Entity;
    }

    /// <summary>
    /// Checks if an email already exists in the authors' database.
    /// </summary>
    /// <param name="email">The email to check for existence.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the email exists.</returns>
    public async Task<bool> CheckEmailIfExist(Email email)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Email == email);

        return author != null;
    }

    /// <summary>
    /// Retrieves all authors from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of authors.</returns>
    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors.Include(c => c.Books)
                                     .ToListAsync();
    }

    /// <summary>
    /// Retrieves an author by their unique identifier.
    /// </summary>
    /// <param name="authorId">The unique identifier of the author.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the author if found; otherwise, null.</returns>
    public async Task<Author?> GetByIdAsync(int authorId)
    {
        return await _context.Authors.FindAsync(authorId);
    }

    /// <summary>
    /// Retrieves a paginated list of authors.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of authors per page.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of authors.</returns>
    public async Task<IPaginated<Author>> GetPaginatedListAsync(int pageNumber, int pageSize)
    {
        return await _context.Authors.Include(c => c.Books).ToPaginatedListAsync(pageNumber, pageSize);
    }
    /// <summary>
    /// Updates an existing author in the database.
    /// </summary>
    /// <param name="author">The author entity to update.</param>
    public void Update(Author author)
    {
        _context.Entry(author).State = EntityState.Modified;
    }
}

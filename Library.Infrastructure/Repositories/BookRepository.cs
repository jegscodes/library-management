namespace Library.Infrastructure.Repositories;

/// <summary>
/// Repository class for managing book entities in the database.
/// </summary>
public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookRepository"/> class.
    /// </summary>
    /// <param name="context">The database context to be used for repository operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context"/> is null.</exception>
    public BookRepository(LibraryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Gets the unit of work associated with the repository.
    /// </summary>
    public IUnitOfWork UnitOfWork => _context;

    /// <summary>
    /// Adds a new book to the database.
    /// </summary>
    /// <param name="book">The book entity to add.</param>
    /// <returns>The added <see cref="Book"/> entity.</returns>
    public Book Add(Book book)
    {
        return _context.Add(book).Entity;
    }

    /// <summary>
    /// Retrieves all books from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of books.</returns>
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.Include(c => c.Author)
                                   .ToListAsync();
    }

    /// <summary>
    /// Retrieves a book by its unique identifier.
    /// </summary>
    /// <param name="bookId">The unique identifier of the book.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the book if found; otherwise, null.</returns>
    public async Task<Book?> GetByIdAsync(int bookId)
    {
        return await _context.Books.Include(b => b.Author)
                                   .Where(b => b.Id == bookId)
                                   .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves a paginated list of books from the database.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of books per page.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of books.</returns>
    public async Task<IPaginated<Book>> GetPaginatedListAsync(int pageNumber, int pageSize)
    {
        return await _context.Books.Include(c => c.Author)
                                    .OrderByDescending(c => c.PublishedDate)
                                    .ToPaginatedListAsync(pageNumber, pageSize);
    }
    /// <summary>
    /// Updates an existing book in the database.
    /// </summary>
    /// <param name="book">The book entity to update.</param>
    public void Update(Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
    }
}

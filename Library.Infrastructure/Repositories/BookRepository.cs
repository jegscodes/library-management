namespace Library.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;
    public Book Add(Book book)
    {
        return _context.Add(book).Entity;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.Include(c => c.Author)
                                   .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int bookId)
    {
        return await _context.Books.FindAsync(bookId);
    }
    public void Update(Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
    }
}

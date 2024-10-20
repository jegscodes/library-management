using Library.Application.Common.Extensions;

namespace Library.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;
    public AuthorRepository(LibraryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;
    public Author Add(Author book)
    {
        return _context.Add(book).Entity;
    }

    public async Task<bool> CheckEmailIfExist(Email email)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Email == email);

        return author != null;
    }

    public async Task<IPaginated<Author>> GetPaginatedListAsync(int pageNumber, int pageSize)
    {
        return await _context.Authors.Include(c => c.Books).ToPaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors.Include(c => c.Books)
                                     .ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(int bookId)
    {
        return await _context.Authors.FindAsync(bookId);
    }
    public void Update(Author book)
    {
        _context.Entry(book).State = EntityState.Modified;
    }
}

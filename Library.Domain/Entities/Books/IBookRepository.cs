namespace Library.Domain.Entities.Books;

public interface IBookRepository : IRepository<Book>
{
    Book Add(Book book);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<IPaginated<Book>> GetPaginatedListAsync(int pageNumber, int pageSize);
    Task<Book> GetByIdAsync(int id);
    void Update(Book book);
}

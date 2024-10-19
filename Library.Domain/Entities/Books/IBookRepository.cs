namespace Library.Domain.Entities.Books;

public interface IBookRepository : IRepository<Book>
{
    Book Add(Book book);

    Task<IEnumerable<Book>> GetAllAsync();

    Task<Book> GetByIdAsync(int id);
    void Update(Book book);
}

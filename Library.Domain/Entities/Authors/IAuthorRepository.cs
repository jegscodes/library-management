namespace Library.Domain.Entities.Authors;

public interface IAuthorRepository : IRepository<Author>
{
    Author Add(Author author);

    Task<IPaginated<Author>> GetPaginatedListAsync(int pageNumber, int pageSize);

    Task<IEnumerable<Author>> GetAllAsync();

    Task<bool> CheckEmailIfExist(Email email);

    Task<Author> GetByIdAsync(int id);

    void Update(Author author);
}

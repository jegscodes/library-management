namespace Library.Domain.Entities.Authors;

public interface IAuthorRepository : IRepository<Author>
{
    Author Add(Author author);

    Task<IEnumerable<Author>> GetAllAsync();

    Task<Author> GetByIdAsync(int id);
    void Update(Author author);
}

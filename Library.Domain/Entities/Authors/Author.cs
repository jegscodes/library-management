namespace Library.Domain.Entities.Authors;

/// <summary>
/// Represents an author who wrote the book.
/// </summary>
public class Author : Entity
{
    private readonly List<Book> _books;

    /// <summary>
    /// Initializes a new instance of the <see cref="Author"/> class with the specified name and email.
    /// </summary>
    /// <param name="name">The name of the author.</param>
    /// <param name="email">The email address of the author.</param>
    public Author(string name, string email)
    {
        Name = name;
        Email = email;
        _books = new List<Book>();
    }

    /// <summary>
    /// Protected constructor for ORM or serialization purposes.
    /// </summary>
    protected Author()
    {
        _books = new List<Book>();
    }

    /// <summary>
    /// Gets a read-only collection of books written by the author.
    /// </summary>
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    /// <summary>
    /// Gets the email address of the author.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the name of the author.
    /// </summary>
    public string Name { get; private set; }
}

namespace Library.Domain.Entities.Authors;

/// <summary>
/// Represents an author who wrote the book.
/// </summary>
public class Author : Entity, IAggregateRoot
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
    }

    /// <summary>
    /// Protected constructor for ORM or serialization purposes.
    /// </summary>
    protected Author()
    {
        _books = [];
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

    /// <summary>
    /// Adds a book to the collection if it does not already exist based on its ISBN.
    /// </summary>
    /// <param name="book">The book to add. Each book must have a unique ISBN.</param>
    /// <remarks>
    /// This method ensures that no two books with the same ISBN can exist in the collection. 
    /// Each title corresponds to a unique ISBN. 
    /// If the ISBN of the provided book matches an existing book, the book will not be added.
    /// </remarks>
    public void AddBook(Book book)
    {
        var bookExist = _books.Any(e => e.ISBN == book.ISBN);

        if (!bookExist)
        {
            _books.Add(book);
        }
    }
}

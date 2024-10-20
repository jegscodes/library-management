namespace Library.Domain.Entities.Books;

/// <summary>
/// Represents a book in the library.
/// </summary>
public class Book : Entity, IAggregateRoot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Book"/> class.
    /// </summary>
    /// <param name="authorId">The identifier of the author of the book.</param>
    /// <param name="title">The title of the book. Must not be empty or null.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="title"/> is empty or null.</exception>
    public Book(int authorId, string title, PublishedDate publishedDate, BookIdentifier isbn)
    {
        AuthorId = authorId;
        Title = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException("Title shouldn't be empty", nameof(title));
        PublishedDate = publishedDate;
        ISBN = isbn;    
    }

    /// <summary>
    /// Protected constructor for ORM or serialization purposes.
    /// </summary>
    protected Book() { }

    /// <summary>
    /// Gets the author of the book.
    /// </summary>
    public Author Author { get; private set; }

    /// <summary>
    /// Gets the identifier for the author of the book.
    /// </summary>
    public int AuthorId { get; private set; }

    /// <summary>
    /// Gets the International Standard Book Number (ISBN) for the book.
    /// ISBN has minimum length of 10, maximum length of 13 and either starts with 978 or 979.
    /// </summary>
    public BookIdentifier ISBN { get; private set; }

    /// <summary>
    /// Gets the publication date of the book.
    /// </summary>
    public PublishedDate PublishedDate { get; private set; }

    /// <summary>
    /// Gets the title of the book.
    /// </summary>
    public string Title { get; private set; }


    public void Update(string title, BookIdentifier isbn, PublishedDate publishedDate)
    {
        Title = title;
        ISBN = isbn;
        PublishedDate = publishedDate;
    }

    public void UpdateAuthor(int authorId)
    {
        if(authorId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(authorId), "Invalid Author Id");
        }

        AuthorId = authorId;
    }

    public bool AuthorChanged(int authorId)
    {
        return AuthorId != authorId ? true : false;
    }
}

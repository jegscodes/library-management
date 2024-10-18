namespace Library.Domain.Entities.Books;

/// <summary>
/// Represents a book in the library.
/// </summary>
public class Book : Entity, IAggregateRoot
{
    public Book(int authorId, string isbn, DateTime publishedDate, string title)
    {
        Title = title;
        ISBN = new BookIdentifier(isbn);
        AuthorId = authorId;
        PublishedDate = publishedDate;
    }
    /// <summary>
    /// Gets the author of the book.
    /// </summary>
    public Author Author { get; }

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
    public DateTime PublishedDate { get; private set; }

    /// <summary>
    /// Gets the title of the book.
    /// </summary>
    public string Title { get; private set; }
}

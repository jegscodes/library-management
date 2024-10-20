namespace Library.Application.Books.Commands;

/// <summary>
/// Represents a command for creating or updating a book.
/// </summary>
public interface IBookCommand
{
    /// <summary>
    /// Gets the identifier of the author associated with the book.
    /// </summary>
    int AuthorId { get; }

    /// <summary>
    /// Gets the International Standard Book Number (ISBN) of the book.
    /// </summary>
    string ISBN { get; }

    /// <summary>
    /// Gets the published date of the book.
    /// </summary>
    DateTime PublishedDate { get; }

    /// <summary>
    /// Gets the title of the book.
    /// </summary>
    string Title { get; }
}

using Library.Domain.Exceptions;

namespace Library.Domain.Entities.Books;

public class BookIdentifier
{
    public string Value { get; }
    public BookIdentifier(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValidISBN(value))
        {
            throw new InvalidISBNException("Invalid ISBN format.");
        }

        Value = value;
    }

    private bool IsValidISBN(string isbn)
    {
        // Validation logic for ISBN here
        // https://www.isbn-international.org/content/what-isbn/10
        return true;
    }
}

using Library.Domain.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Domain.Entities.Books;

/// <summary>
/// Represents a published date for a book.
/// </summary>
public sealed class PublishedDate : ValueObject
{
    public DateTime Value { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PublishedDate"/> class.
    /// </summary>
    /// <param name="value">The date the book was published.</param>
    /// <exception cref="ArgumentException">Thrown when the date is in the future.</exception>
    public static PublishedDate Create(DateTime value)
    {
        if (value.Date > DateTime.Now.Date)
        {
            throw new PublishedDateException("Published date shouldn't exceed today's date.");
        }

        return new PublishedDate { Value = value.Date };
    }

    /// <summary>
    /// Returns the components that make this value object unique for equality comparison.
    /// </summary>
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString("yyyy-MM-dd");
    }
}

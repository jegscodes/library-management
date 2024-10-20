using Library.Domain.Exceptions;

namespace Library.Domain.Entities.Books;

public sealed class BookIdentifier : ValueObject
{
    /// <summary>
    /// Gets the value of the book identifier.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="BookIdentifier"/> class.
    /// </summary>
    /// <param name="value">The ISBN value to initialize the book identifier.</param>
    /// <returns>A new <see cref="BookIdentifier"/> instance.</returns>
    /// <exception cref="InvalidISBNException">Thrown when the ISBN is empty or has an invalid format.</exception>
    public static BookIdentifier Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidISBNException("ISBN cannot be empty.");
        }
        // TODO/REFACTOR: Change Implementation ?
        if (!IsValidISBN(value))
        {
            throw new InvalidISBNException("Invalid ISBN format.");
        }

        return new BookIdentifier { Value = value };
    }

    /// <summary>
    /// Validates the given ISBN value.
    /// </summary>
    /// <param name="isbn">The ISBN string to validate.</param>
    /// <returns><c>true</c> if the ISBN is valid; otherwise, <c>false</c>.</returns>
    /// <exception cref="InvalidISBNException">Thrown when the ISBN does not meet length requirements.</exception>
    private static bool IsValidISBN(string isbn)
    {
        // Convert to span, removing hyphens and whitespace
        var cleanIsbn = isbn.AsSpan().Trim().ToString().Replace("-", "").AsSpan();

        return cleanIsbn.Length switch
        {
            10 => ISBN10CheckDigit(cleanIsbn),
            13 => ISBN13CheckDigit(cleanIsbn),
            _ => throw new InvalidISBNException("ISBN must be 10 or 13 characters long.")
        };
    }

    /// <summary>
    /// Validates the check digit for an ISBN-10 number.
    /// The check digit is calculated using the formula:
    /// <list type="bullet">
    ///     <item><description>Sum = (d1 * 10 + d2 * 9 + d3 * 8 + d4 * 7 + d5 * 6 + d6 * 5 + d7 * 4 + d8 * 3 + d9 * 2 + d10 * 1)</description></item>
    ///     <item><description>Check digit (d10) is valid if Sum % 11 == 0.</description></item>
    /// </list>
    /// For more details, see: https://www.futurelearn.com/info/courses/maths-puzzles/0/steps/14005
    /// </summary>
    /// <param name="cleanIsbn">A span of characters representing the cleaned ISBN-10.</param>
    /// <returns><c>true</c> if the check digit is valid; otherwise, <c>false</c>.</returns>
    private static bool ISBN10CheckDigit(ReadOnlySpan<char> cleanIsbn)
    {
        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            if (!char.IsDigit(cleanIsbn[i])) return false;

            sum += (cleanIsbn[i] - '0') * (10 - i);
        }

        char lastChar = cleanIsbn[9];
        sum += lastChar == 'X' ? 10 : (lastChar - '0');

        return sum % 11 == 0;
    }

    /// <summary>
    /// Validates the check digit for an ISBN-13 number.
    /// The check digit is calculated using the formula:
    /// <list type="bullet">
    ///     <item><description>Sum = (d1 * 1 + d2 * 3 + d3 * 1 + d4 * 3 + d5 * 1 + d6 * 3 + d7 * 1 + d8 * 3 + d9 * 1 + d10 * 3 + d11 * 1 + d12 * 3)</description></item>
    ///     <item><description>Check digit (d13) is valid if d13 == (10 - (Sum % 10)) % 10.</description></item>
    /// </list>
    /// ISBN-13 either starts with 979 or 978.
    /// </summary>
    /// <param name="cleanIsbn">A span of characters representing the cleaned ISBN-13.</param>
    /// <returns><c>true</c> if the check digit is valid; otherwise, <c>false</c>.</returns>
    private static bool ISBN13CheckDigit(ReadOnlySpan<char> cleanIsbn)
    {
        var firstThree = cleanIsbn.Slice(0, 3);

        if (!firstThree.SequenceEqual("978".AsSpan()) && !firstThree.SequenceEqual("979".AsSpan()))
        {
            return false;
        }

        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            if (!char.IsDigit(cleanIsbn[i]))
                return false;

            sum += (cleanIsbn[i] - '0') * (i % 2 == 0 ? 1 : 3);
        }

        int checkDigit = (10 - (sum % 10)) % 10;
        return cleanIsbn[12] - '0' == checkDigit;
    }

    /// <summary>
    /// Returns the components that make this value object unique for equality comparison.
    /// </summary>
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

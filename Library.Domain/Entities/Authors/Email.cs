namespace Library.Domain.Entities.Authors;

/// <summary>
/// Represents an email value object with validation logic.
/// </summary>
public class Email : ValueObject
{
    private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    /// <summary>
    /// Gets the value of the email.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Email"/> class.
    /// Validates the email format and throws exceptions for invalid input.
    /// </summary>
    /// <param name="email">The email address to be validated and created.</param>
    /// <returns>A new instance of the <see cref="Email"/> class.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the email is null or empty.</exception>
    /// <exception cref="InvalidEmailException">Thrown when the email format is invalid.</exception>
    public static Email Create(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(nameof(email), "Email cannot be null.");
        }

        if (!Regex.IsMatch(email, EmailRegexPattern, RegexOptions.IgnoreCase))
        {
            throw new InvalidEmailException("Invalid email format.");
        }

        return new Email { Value = email };
    }

    /// <summary>
    /// Returns the atomic values of the <see cref="Email"/> object for equality comparisons.
    /// </summary>
    /// <returns>An enumerable of atomic values.</returns>
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

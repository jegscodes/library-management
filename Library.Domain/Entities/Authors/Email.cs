
using Library.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Library.Domain.Entities.Authors;

public class Email : ValueObject
{
    private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    public string Value { get; private set; }

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

        return new Email { Value  = email };
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

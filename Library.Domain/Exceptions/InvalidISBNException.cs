namespace Library.Domain.Exceptions;

public class InvalidISBNException : Exception
{
    public InvalidISBNException()
    { }

    public InvalidISBNException(string message)
        : base(message)
    { }

    public InvalidISBNException(string message, Exception innerException)
        : base(message, innerException)
    { }
}

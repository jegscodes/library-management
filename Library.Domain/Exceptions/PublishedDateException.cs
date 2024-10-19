namespace Library.Domain.Exceptions;

public class PublishedDateException : Exception
{
    public PublishedDateException()
    { }

    public PublishedDateException(string message)
        : base(message)
    { }

    public PublishedDateException(string message, Exception innerException)
        : base(message, innerException)
    { }
}

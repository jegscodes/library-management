namespace Library.Domain.Events;

public class BookAddedDomainEvent : INotification
{
    public Book Book { get; }

    public BookAddedDomainEvent(Book book)
    {
        Book = book;
    }
}

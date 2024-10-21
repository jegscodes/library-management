namespace Library.Domain.Events;

public class AuthorCreatedDomainEvent : INotification
{
    public string Name { get; }

    public AuthorCreatedDomainEvent(string name)
    {
        Name = name;
    }
}

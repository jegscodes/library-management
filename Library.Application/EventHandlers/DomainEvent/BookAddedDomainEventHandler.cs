
namespace Library.Application.EventHandlers.DomainEvent;

public class BookAddedDomainEventHandler : INotificationHandler<BookAddedDomainEvent>
{
    private readonly ILogger<BookAddedDomainEventHandler> _logger;

    public BookAddedDomainEventHandler(ILogger<BookAddedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    public async Task Handle(BookAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(1000);

        _logger.LogInformation("Added book: {book}", notification.Book.Title);
    }
}

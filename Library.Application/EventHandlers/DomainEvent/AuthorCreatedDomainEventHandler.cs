namespace Library.Application.EventHandlers.DomainEvent;

public class AuthorCreatedDomainEventHandler : INotificationHandler<AuthorCreatedDomainEvent>
{
    private readonly ILogger<AuthorCreatedDomainEventHandler> _logger;

    public AuthorCreatedDomainEventHandler(ILogger<AuthorCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(AuthorCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(1000); // Simulate
        _logger.LogInformation("Author: {name}", notification.Name);
        return;
    }
}

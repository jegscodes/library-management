namespace Library.Domain.Common;

/// <summary>
/// Represents a base entity with auditing capabilities 
/// and domain event management.
/// </summary>
public abstract class Entity : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int Id { get; set; }

    private List<INotification> _domainEvents;

    /// <summary>
    /// Gets a read-only collection of domain events associated with the entity.
    /// </summary>
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the entity.
    /// </summary>
    /// <param name="eventItem">The domain event to add.</param>
    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? [];
        _domainEvents.Add(eventItem);
    }

    /// <summary>
    /// Removes a domain event from the entity.
    /// </summary>
    /// <param name="eventItem">The domain event to remove.</param>
    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    /// <summary>
    /// Clears all domain events associated with the entity.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}

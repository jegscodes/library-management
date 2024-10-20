using System.ComponentModel.DataAnnotations.Schema;

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
    public int Id { get; protected set; }

    private readonly List<INotification> _domainEvents = [];

    /// <summary>
    /// Gets a read-only collection of domain events associated with the entity.
    /// </summary>
    [NotMapped]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the entity.
    /// </summary>
    /// <param name="eventItem">The domain event to add.</param>
    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    /// <summary>
    /// Removes a domain event from the entity.
    /// </summary>
    /// <param name="eventItem">The domain event to remove.</param>
    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents.Remove(eventItem);
    }

    /// <summary>
    /// Clears all domain events associated with the entity.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (!(obj is Entity other))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (Id == default || other.Id == default)
        {
            return false;
        }

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 31;
    }
}

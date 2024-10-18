namespace Library.Domain.Common;

/// <summary>
/// Represents an aggregate root in the Domain-Driven Design (DDD) context.
/// </summary>
/// <remarks>
/// An aggregate root is an entity that acts as a gateway to a cluster of related objects, 
/// ensuring consistency and encapsulating the rules for modifying the aggregate. 
/// It is the only member of the aggregate that outside objects are allowed to hold references to.
/// </remarks>
public interface IAggregateRoot;

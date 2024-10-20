namespace Library.Application.Authors.Commands;

/// <summary>
/// Represents a command related to author operations.
/// </summary>
public interface IAuthorCommand
{
    /// <summary>
    /// Gets the email of the author.
    /// </summary>
    string Email { get; }

    /// <summary>
    /// Gets the name of the author.
    /// </summary>
    string Name { get; }
}

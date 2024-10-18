namespace Library.Domain.Common;

/// <summary>
/// Represents an entity that has auditing capabilities, 
/// tracking creation and modification details.
/// </summary>
public abstract class AuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the user who created the entity.
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    public DateTime ModifiedOn { get; set; }

    /// <summary>
    /// Gets or sets the user who last modified the entity.
    /// </summary>
    public string ModifiedBy { get; set; }
}

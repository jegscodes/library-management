namespace Library.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the <see cref="Author"/> entity for the database context.
/// </summary>
public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    /// <summary>
    /// Configures the <see cref="Author"/> entity's properties and relationships.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // Ignore the DomainEvents property for database mapping
        builder.Ignore(b => b.DomainEvents);

        // Configure the one-to-many relationship with Books
        builder.HasMany(b => b.Books)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
    }
}

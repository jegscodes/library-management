namespace Library.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the <see cref="Book"/> entity for the database context.
/// </summary>
public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
    /// <summary>
    /// Configures the <see cref="Book"/> entity's properties and relationships.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        // Ignore the DomainEvents property for database mapping
        builder.Ignore(b => b.DomainEvents);

        // Configure the ISBN property with a maximum length of 13 characters.
        // ISBN has minimum length of 10 and either starts with 978 or 979.
        // https://www.isbn-international.org/content/what-isbn/10
        builder.Property(b => b.ISBN)
               .HasMaxLength(13);

        // Configure the many-to-one relationship with Author
        builder.HasOne(b => b.Author)
               .WithMany()
               .HasForeignKey(b => b.AuthorId);
    }
}

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
        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
               .HasMaxLength(255)
               .IsRequired();

        // Ignore the DomainEvents property for database mapping
        builder.Ignore(b => b.DomainEvents);

        // Configure the ISBN property with a maximum length of 13 characters.
        // ISBN has minimum length of 10 and either starts with 978 or 979.
        // https://www.isbn-international.org/content/what-isbn/10
        builder.Property(b => b.ISBN)
               .HasConversion(v => v.Value,
                              v => BookIdentifier.Create(v))
               .HasMaxLength(17)
               .IsRequired();

        builder.Property(b => b.PublishedDate)
               .HasConversion(v => v.Value,
                              v => PublishedDate.Create(v))
               .IsRequired();

        // Configure the many-to-one relationship with Author
        builder.HasOne(b => b.Author)
               .WithMany(b  => b.Books)
               .HasForeignKey(b => b.AuthorId);
    }
}

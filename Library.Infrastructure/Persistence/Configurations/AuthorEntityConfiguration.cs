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
        builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
               .HasMaxLength(128)
               .IsRequired();

        builder.Property(e => e.Email)
               .HasMaxLength(255)
               .HasConversion(v => v.Value, v => Email.Create(v))
               .IsRequired();

        builder.HasIndex(e => e.Email)
               .IsUnique();

        // Ignore the DomainEvents property for database mapping
        builder.Ignore(b => b.DomainEvents);

        // Configure the one-to-many relationship with Books
        builder.HasMany(b => b.Books)
               .WithOne(b => b.Author)
               .HasForeignKey(b => b.AuthorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

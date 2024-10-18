public class LibraryDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LibraryDbContext"/> class 
    /// with the specified options.
    /// </summary>
    /// <param name="options">The options for configuring the database context.</param>
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    /// <summary>
    /// Gets or sets the collection of books in the library.
    /// </summary>
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// Gets or sets the collection of authors in the library.
    /// </summary>
    public DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Configures the model creating process, applying configurations from the current assembly.
    /// </summary>
    /// <param name="builder">The model builder used to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}

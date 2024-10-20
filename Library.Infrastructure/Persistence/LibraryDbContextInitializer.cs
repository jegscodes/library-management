using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Persistence;

public class LibraryDbContextInitializer
{
    private readonly ILogger<LibraryDbContextInitializer> _logger;
    private readonly LibraryDbContext _context;

    public LibraryDbContextInitializer(ILogger<LibraryDbContextInitializer> logger, LibraryDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
    public async Task SeedAsync()
    {
        _logger.LogInformation("Seed Started.");

        if(!_context.Authors.Any())
        {
            var email = Email.Create("testauthor@mailinator.com");
            var author = new Author("Test Author", email);

            _context.Authors.Add(author);
        }

        if (_context.ChangeTracker.HasChanges())
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation("Seed Completed.");
        }
    }
}

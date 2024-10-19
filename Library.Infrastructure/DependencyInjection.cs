using Library.Infrastructure.Persistence;
using Library.Infrastructure.Persistence.Interceptors;
using Library.Infrastructure.Repositories;

namespace Library.Infrastructure;

/// <summary>
/// Provides extension methods for configuring the infrastructure layer 
/// of the application, including database context and dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the specified <see cref="IServiceCollection"/> 
    /// </summary>
    /// <param name="services">The service collection to add the infrastructure services to.</param>
    /// <param name="configuration">The configuration containing application settings.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the connection string is not found.</exception>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "Connection string not found.");
        }

        services.AddScoped<ISaveChangesInterceptor, EntityAuditAndEventInterceptor>();

        services.AddDbContext<LibraryDbContext>((provider, options) =>
        {
            options.AddInterceptors(provider.GetRequiredService<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<LibraryDbContextInitializer>();

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}

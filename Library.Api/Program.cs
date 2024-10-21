using Library.Api.Middlewares;
using Library.Application;
using Library.Infrastructure;
using Library.Infrastructure.Persistence;

namespace Library.Api;

/// <summary>
/// The main entry point for the Library API application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main method that runs the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
               .AddApplicationServices()
               .AddInfrastructure(builder.Configuration);

        builder.Services.AddControllers();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<LibraryDbContextInitializer>();

            await initializer.InitializeAsync();
            await initializer.SeedAsync();
        }
        else
        { 
            // Enforce HTTPS by using HSTS (HTTP Strict Transport Security) in production
            app.UseHsts();
        }

        app.UseMiddleware<CustomExceptionHandler>();
        app.UseMiddleware<RateLimitHandler>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

namespace Library.Api.Middlewares;

/// <summary>
/// Middleware to handle exceptions globally in the application.
/// </summary>
public class CustomExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> _logger;
    private readonly RequestDelegate _next;
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomExceptionHandler"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>
    /// <param name="logger">The logger instance used for logging errors.</param>
    public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware and handles any exceptions that occur during the request processing.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Call the next middleware
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex); // Handle the exception
        }
    }

    /// <summary>
    /// Handles exceptions and sets the appropriate response status code and content.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="exception">The exception that occurred.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var httpStatusCode = HttpStatusCode.InternalServerError;
        string result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                httpStatusCode = HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Failures);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;

        if (result.Length == 0)
        {
            result = JsonSerializer.Serialize(new
            {
                error = exception.Message
            });
        }

        return context.Response.WriteAsync(result); 
    }
}

using System.Collections.Concurrent;

namespace Library.Api.Middlewares
{
    /// <summary>
    /// Middleware for rate limiting requests from clients based on their IP address.
    /// </summary>
    public class RateLimitHandler
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, RateLimitInfo> _clients = new();
        private readonly TimeSpan _timeWindow = TimeSpan.FromMinutes(1);
        private readonly int _maxRequests = 25;

        /// <summary>
        /// Initializes a new instance of the <see cref="RateLimitHandler"/> class.
        /// </summary>
        /// <param name="next">The next delegate in the middleware pipeline.</param>
        public RateLimitHandler(RequestDelegate next)
        {
            _next = next;
            _semaphore = new SemaphoreSlim(1, 1);
        }

        /// <summary>
        /// Invokes the middleware to limit the number of requests from the same client.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();

            if (clientIp != null)
            {
                await _semaphore.WaitAsync();
                try
                {
                    if (!_clients.TryGetValue(clientIp, out var rateLimitInfo))
                    {
                        rateLimitInfo = new RateLimitInfo();
                        _clients[clientIp] = rateLimitInfo;
                    }

                    // Check if the time window has elapsed
                    if (DateTime.UtcNow - rateLimitInfo.Timestamp > _timeWindow)
                    {
                        rateLimitInfo.Timestamp = DateTime.UtcNow;
                        rateLimitInfo.RequestCount = 1;
                    }
                    else
                    {
                        rateLimitInfo.RequestCount++;
                        // If the request count exceeds the limit, return a 429 status code
                        if (rateLimitInfo.RequestCount > _maxRequests)
                        {
                            context.Response.StatusCode = StatusCodes.Status429TooManyRequests; 
                            await context.Response.WriteAsync("Please try again later.");
                            return;
                        }
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            }

            await _next(context);
        }

        /// <summary>
        /// Contains information about the rate limit for a client.
        /// </summary>
        private class RateLimitInfo
        {
            /// <summary>
            /// Gets or sets the timestamp of the first request in the current time window.
            /// </summary>
            public DateTime Timestamp { get; set; } = DateTime.UtcNow;

            /// <summary>
            /// Gets or sets the count of requests made in the current time window.
            /// </summary>
            public int RequestCount { get; set; }
        }
    }
}

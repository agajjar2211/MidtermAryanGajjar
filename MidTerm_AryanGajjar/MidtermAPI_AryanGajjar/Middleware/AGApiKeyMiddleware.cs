namespace MidtermAPI_AryanGajjar.Middleware
{
    public class AGApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string? _apiKey;

        public AGApiKeyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _apiKey = config.GetValue<string>("ApiKey");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var providedKey))
            {
                await Reject(context);
                return;
            }

            if (string.IsNullOrWhiteSpace(_apiKey) || providedKey != _apiKey)
            {
                await Reject(context);
                return;
            }

            await _next(context);
        }

        private static async Task Reject(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                error = "Unauthorized",
                message = "Invalid or missing API key."
            });
        }
    }
}

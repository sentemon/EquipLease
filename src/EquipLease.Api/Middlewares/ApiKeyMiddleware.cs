using EquipLease.Core.Constants;

namespace EquipLease.Api.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    
    private const string ApiKeyHeaderName = "X-API-KEY";

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API Key is missing.");
            
            return;
        }

        var configuredApiKey = _configuration[AppSettingsConstants.ApiKey] ??
                               throw new ArgumentNullException(nameof(AppSettingsConstants.ApiKey), 
                                   "API Key not configured.");

        if (!string.Equals(extractedApiKey, configuredApiKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Unauthorized client.");
            
            return;
        }

        await _next(context);
    }
}
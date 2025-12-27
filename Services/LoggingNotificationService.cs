
using Microsoft.Extensions.Logging;

namespace EyeOfJanthir.Services;

public sealed class LoggingNotificationService(
    ILogger<LoggingNotificationService> logger)
    : INotificationService
{
    private readonly ILogger<LoggingNotificationService> logger = logger;

    public Task SendNotificationAsync(string title, string message, CancellationToken cancellationToken)
    {
        this.logger.LogError("{Title}\n{Message}", title, message);
        return Task.CompletedTask;
    }
}

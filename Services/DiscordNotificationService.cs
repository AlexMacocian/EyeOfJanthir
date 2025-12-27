using EyeOfJanthir.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace EyeOfJanthir.Services;

public sealed class DiscordNotificationService(
    IHttpClientFactory httpClientFactory,
    ILogger<DiscordNotificationService> logger)
    : INotificationService, IDisposable
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient(nameof(DiscordNotificationService));
    private readonly ILogger<DiscordNotificationService> logger = logger;

    public void Dispose()
    {
        this.httpClient.Dispose();
    }

    public async Task SendNotificationAsync(string title, string message, CancellationToken cancellationToken)
    {
        this.logger.LogError("{Title}\n{Message}", title, message);

        var payload = new DiscordWebhookPayload(
            Content: null,
            Embeds:
            [
                new DiscordEmbed(
                    Title: title,
                    Description: message,
                    Color: 16711680) // Red color
            ]);

        try
        {
            var response = await this.httpClient.PostAsJsonAsync(string.Empty, payload, AppJsonContext.Default.DiscordWebhookPayload, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                this.logger.LogError("Failed to send Discord notification. Status: {StatusCode}", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Encountered exception while sending Discord notification");
        }
    }
}

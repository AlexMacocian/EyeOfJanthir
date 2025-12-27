using EyeOfJanthir.Models;

namespace EyeOfJanthir.Services;

public interface INotificationService
{
    Task SendNotificationAsync(string title, string message, DiscordColor color, CancellationToken cancellationToken);
}
